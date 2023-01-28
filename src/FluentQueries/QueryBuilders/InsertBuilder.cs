using FluentQueries.Abstract;
using FluentQueries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQueries.QueryBuilders
{
    public enum IdentityType
    {
        Int,
        Long,
        Guid
    }
    public class InsertBuilder
    {
        private string _into = "";
        private List<string> _fields = new List<string>();
        private List<List<string>> _data = new List<List<string>>();
        private Dictionary<string, IQueryParameter> _parameters = new Dictionary<string, IQueryParameter>();
        private string _scopeIdentityQuery;
        
        public int ParametersCount => _parameters.Count();

        public InsertBuilder Into(string tableName, IEnumerable<string> fields)
        {
            _into = $"[{tableName}]";

            foreach (var fldName in fields)
            {
                _fields.Add(fldName);
            }

            return this;
        }

        public InsertBuilder Into(string tableName, IEnumerable<IDataField> fields)
        {
            _into = $"[{tableName}]";

            foreach (var dataField in fields)
            {
                _fields.Add(dataField.Name);
            }

            return this;
        }

        public InsertBuilder Values(IEnumerable<object> values)
        {
            var row = new List<string>();

            foreach (var value in values)
            {
                var parameterName = $"parameter{(_parameters.Count() + 1)}";

                var parameter = new QueryParameter(parameterName, value);
                SetParameter(parameter);

                row.Add("@" + parameterName);

            }

            _data.Add(row);

            return this;
        }

        public InsertBuilder Values(IEnumerable<IDataField> dataFields)
        {
            var row = new List<string>();

            foreach (var dataField in dataFields)
            {
                var parameterName = $"parameter{(_parameters.Count() + 1)}";

                var valueType = QueryParameter.DbTypeByValueType(dataField.ValueType);
                var parameter = new QueryParameter(parameterName, dataField.Value, valueType);
                SetParameter(parameter);

                row.Add("@" + parameterName);

            }

            _data.Add(row);

            return this;
        }
        
        public InsertBuilder ScopeIdentity(IdentityType identityType = IdentityType.Int)
        {
            var type = "INT";
            switch (identityType)
            {
                case IdentityType.Long:
                    type = "BIGINT";
                    break;
                case IdentityType.Guid:
                    type = "UNIQUEIDENTIFIER";
                    break;
            }
            _scopeIdentityQuery = $"SELECT CAST(SCOPE_IDENTITY() as {type})";
            return this;
        }

        public IQuery Query()
        {
            var query = new Query();
            query.AddParameters(_parameters.Select(x => x.Value).ToList());

            var sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.AppendLine(_into);

            // Keys
            sb.Append("(");
            var n = 1;
            foreach (var fld in _fields)
            {
                if (n > 1)
                {
                    sb.Append(", ");
                }
                sb.Append($"[{fld}]");
                n++;
            }
            sb.AppendLine(")");

            // Values
            sb.AppendLine("VALUES");

            var r = 1;
            foreach (var row in _data)
            {
                n = 1;
                if (r > 1)
                {
                    sb.Append(",");
                }
                sb.Append("(");
                foreach (var expression in row)
                {
                    if (n > 1)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(expression);
                    n++;
                }
                sb.AppendLine(")");
                r++;
            }

            if (!string.IsNullOrEmpty(_scopeIdentityQuery))
                sb.Append(";")
                    .Append(_scopeIdentityQuery);

            query.Text = sb.ToString();

            return query;

        }

        private void SetParameter(IQueryParameter parameter)
        {
            if (_parameters.ContainsKey(parameter.Name))
            {
                _parameters[parameter.Name] = parameter;
            }
            else
            {
                _parameters.Add(parameter.Name, parameter);
            }
        }

        public static InsertBuilder Make()
        {
            return new InsertBuilder();
        }

    }
}
