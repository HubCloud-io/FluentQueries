using FluentQueries.Abstract;
using FluentQueries.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FluentQueries.QueryBuilders
{
    public class SelectBuilder
    {
        private string _fromExpression;
        private List<string> _selectExpressions = new List<string>();
        private List<string> _orderByExpressions = new List<string>();
        private List<string> _leftJoinExpressions = new List<string>();
        private string _whereExpression = "";
        private List<IQueryParameter> _parameters = new List<IQueryParameter>();

        public static SelectBuilder Make()
        {
            return new SelectBuilder();
        }

        public SelectBuilder FromExpression(string fromExpression)
        {
            _fromExpression = fromExpression;

            return this;
        }

        public SelectBuilder From(string tableName, string alias = null)
        {
            _fromExpression = $"[{tableName}]";

            if (!string.IsNullOrEmpty(alias))
            {
                _fromExpression += $" AS {alias}";
            }

            return this;
        }

        public SelectBuilder SelectExpression(string selectExpression)
        {
            _selectExpressions.Add(selectExpression);
            return this;
        }

        public SelectBuilder Select(string fieldName, string alias = null, string tableName = null)
        {
            var queryField = new QueryField(fieldName, alias, tableName);
            var selectExpression = queryField.Expression;
            _selectExpressions.Add(selectExpression);

            return this;
        }

        public SelectBuilder SelectIf(bool condition, string fieldName, string alias = null, string tableName = null)
        {
            if (!condition)
            {
                return this;
            }

            Select(fieldName, alias, tableName);

            return this;
        }

        public SelectBuilder Select(IEnumerable<IQueryField> fields)
        {

            foreach (var fld in fields)
            {
                var selectExpression = fld.Expression;
                _selectExpressions.Add(selectExpression);
            }

            return this;
        }

        public SelectBuilder OrderBy(string expression)
        {
            _orderByExpressions.Add(expression);

            return this;
        }

        public SelectBuilder LeftJoin(string expression)
        {

            _leftJoinExpressions.Add(expression);

            return this;
        }

        public SelectBuilder Where(string whereExpression)
        {
            if (string.IsNullOrEmpty(_whereExpression))
            {
                _whereExpression = whereExpression;
            }
            else
            {
                _whereExpression += " AND " + whereExpression;
            }

            return this;
        }

        public SelectBuilder Parameter(string name, object value)
        {
            var parameter = new QueryParameter(name, value);

            _parameters.Add(parameter);

            return this;
        }

        public SelectBuilder Parameter(string name, object value, DbType dbType)
        {
            var parameter = new QueryParameter(name, value, dbType);

            _parameters.Add(parameter);

            return this;
        }


        public IQuery Query()
        {
            var query = new Query();
            query.AddParameters(_parameters);

            var sb = new StringBuilder();

            sb.Append("SELECT");
            if (!_selectExpressions.Any())
            {
                sb.AppendLine(" * ");
            }
            else
            {
                var n = 1;
                foreach (var selectExpression in _selectExpressions)
                {
                    if (n == 1)
                    {
                        sb.AppendLine($" {selectExpression}");
                    }
                    else
                    {
                        sb.AppendLine($",{selectExpression}");
                    }
                    n++;
                }
            }

            // FROM
            sb.AppendLine($"FROM {_fromExpression}");

            // JOIN
            AddLeftJoinExpressions(sb);

            // Where
            if (!string.IsNullOrEmpty(_whereExpression))
            {
                sb.AppendLine($"WHERE {_whereExpression}");
            }

            // ORDER BY
            AddOrderByExpressions(sb);

            query.Text = sb.ToString();

            return query;
        }


        private void AddLeftJoinExpressions(StringBuilder sb)
        {
            if (_leftJoinExpressions.Any())
            {
                foreach (var currentExpression in _leftJoinExpressions)
                {
                    sb.AppendLine($"LEFT JOIN {currentExpression}");

                }
            }
        }

        private void AddOrderByExpressions(StringBuilder sb)
        {
            if (_orderByExpressions.Any())
            {
                sb.Append("ORDER BY ");
                var n = 1;
                foreach (var currentExpression in _orderByExpressions)
                {
                    if (n > 1)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(currentExpression);
                    n++;
                }
            }
        }
    }
}
