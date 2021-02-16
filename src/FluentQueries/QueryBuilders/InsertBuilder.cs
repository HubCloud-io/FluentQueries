using FluentQueries.Abstract;
using FluentQueries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQueries.QueryBuilders
{
    public class InsertBuilder
    {
        private string _into = "";
        private Dictionary<string, string> _fields = new Dictionary<string, string>();
        //  private string _whereExpression = "";
        private Dictionary<string, IQueryParameter> _parameters = new Dictionary<string, IQueryParameter>();

        public InsertBuilder Into(string tableName)
        {
            _into = $"[{tableName}]";

            return this;
        }

        public InsertBuilder Set(string name, object value)
        {
            SetExpressionAndParameter(name, value);
            return this;
        }

        public InsertBuilder Set(Dictionary<string, object> values)
        {
            foreach (var kvp in values)
            {
                SetExpressionAndParameter(kvp.Key, kvp.Value);
            }

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
            foreach (var kvp in _fields)
            {
                if (n > 1)
                {
                    sb.Append(", ");
                }
                sb.Append($"[{kvp.Key}]");
                n++;
            }
            sb.AppendLine(")");

            // Values
            sb.Append("VALUES(");
            n = 1;
            foreach(var kvp in _fields)
            {
                if(n > 1)
                {
                    sb.Append(", ");
                }
                sb.Append(kvp.Value);
                n++;
            }
            sb.Append(")");

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
