using FluentQueries.Abstract;
using FluentQueries.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FluentQueries.QueryBuilders
{
    public class UpdateBuilder
    {
        private string _tableName = "";
        private Dictionary<string, string> _fields = new Dictionary<string, string>();
        private string _whereExpression = "";
        private Dictionary<string, IQueryParameter> _parameters = new Dictionary<string, IQueryParameter>();

        public int ParametersCount => _parameters.Count();

        public UpdateBuilder Table(string tableName)
        {
            _tableName = tableName;

            return this;
        }

        public UpdateBuilder Set(string name, object value)
        {
            SetExpressionAndParameter(name, value);
            return this;
        }

        public UpdateBuilder Set(Dictionary<string, object> values)
        {
            foreach (var kvp in values)
            {
                SetExpressionAndParameter(kvp.Key, kvp.Value);
            }

            return this;
        }

        public UpdateBuilder Set(IEnumerable<IDataField> dataFields)
        {
            foreach (var dataField in dataFields)
            {
                var dbType = QueryParameter.DbTypeByValueType(dataField.ValueType);
                SetExpressionAndParameter(dataField.Name, dataField.Value, dbType);
            }

            return this;
        }

        public UpdateBuilder Where(string whereExpression, IEnumerable<IQueryParameter> parameters)
        {
            _whereExpression += whereExpression;
            foreach (var parameter in parameters)
            {
                SetParameter(parameter);
            }

            return this;
        }

        public IQuery Query()
        {
            var query = new Query();
            query.AddParameters(_parameters.Select(x => x.Value).ToList());

            var sb = new StringBuilder();
            sb.AppendLine($"UPDATE [{_tableName}]");
            sb.AppendLine("SET");

            var n = 1;
            foreach (var kvp in _fields)
            {
                if (n > 1)
                {
                    sb.Append(",");
                }
                sb.AppendLine($"[{kvp.Key}] = {kvp.Value}");

                n++;
            }

            if (!string.IsNullOrEmpty(_whereExpression))
            {
                sb.AppendLine($"WHERE {_whereExpression}");
            }

            query.Text = sb.ToString();

            return query;

        }


        private void SetExpressionAndParameter(string name, object value)
        {
            var parameterName = name.ToLower();
            var parameterExpression = $"@{parameterName}";
            if (_fields.ContainsKey(name))
            {
                _fields[name] = parameterExpression;
            }
            else
            {
                _fields.Add(name, parameterExpression);
            }

            var parameter = new QueryParameter(parameterName, value);
            SetParameter(parameter);

        }

        private void SetExpressionAndParameter(string name, object value, DbType dbType)
        {
            var parameterName = name.ToLower();
            var parameterExpression = $"@{parameterName}";
            if (_fields.ContainsKey(name))
            {
                _fields[name] = parameterExpression;
            }
            else
            {
                _fields.Add(name, parameterExpression);
            }

            var parameter = new QueryParameter(parameterName, value, dbType);
            SetParameter(parameter);

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

        public static UpdateBuilder Make()
        {
            return new UpdateBuilder();
        }

    }
}
