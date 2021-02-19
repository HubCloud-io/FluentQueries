using FluentQueries.Abstract;
using FluentQueries.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FluentQueries.QueryBuilders
{
    public class DeleteBuilder
    {
        private string _tableName = "";
        private string _whereExpression = "";
        private List<IQueryParameter> _parameters = new List<IQueryParameter>();

        public DeleteBuilder Table(string tableName)
        {
            _tableName = tableName;

            return this;
        }

        public DeleteBuilder Where(string whereExpression)
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

        public DeleteBuilder Parameter(string name, object value)
        {
            var parameter = new QueryParameter(name, value);

            _parameters.Add(parameter);

            return this;
        }

        public DeleteBuilder Parameter(string name, object value, DbType dbType)
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
            sb.AppendLine($"DELETE [{_tableName}]");

            if (!string.IsNullOrEmpty(_whereExpression))
            {
                sb.AppendLine($"WHERE {_whereExpression}");
            }

            query.Text = sb.ToString();

            return query;

        }

        public static DeleteBuilder Make()
        {
            return new DeleteBuilder();
        }

    }
}
