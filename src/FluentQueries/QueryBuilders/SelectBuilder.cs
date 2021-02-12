using FluentQueries.Abstract;
using FluentQueries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries.QueryBuilders
{
    public class SelectBuilder
    {
        private string _fromExpression;
        private List<string> _selectExpressions = new List<string>();

        public SelectBuilder FromExpression(string fromExpression)
        {
            _fromExpression = fromExpression;

            return this;
        }

        public SelectBuilder From(string tableName, string alias = null)
        {
            _fromExpression = $"[{tableName}]";

            if (string.IsNullOrEmpty(alias))
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
            var selectExpression = "";
            if (string.IsNullOrEmpty(tableName))
            {
                selectExpression = $"[{fieldName}]";
            }
            else
            {
                selectExpression = $"[{tableName}].[{fieldName}]";
            }

            if (!string.IsNullOrEmpty(alias))
            {
                selectExpression = selectExpression + " AS " + alias;
            }
            _selectExpressions.Add(selectExpression);

            return this;
        }

        public IQuery ToQuery()
        {
            var query = new Query();

            return query;
        }

        public static SelectBuilder Make()
        {
            return new SelectBuilder();
        }
    }
}
