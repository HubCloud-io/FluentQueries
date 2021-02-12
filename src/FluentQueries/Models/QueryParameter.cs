using FluentQueries.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FluentQueries.Models
{
    public class QueryParameter : IQueryParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType DbType { get; set; }

        public QueryParameter()
        {

        }

        public QueryParameter(string name, object value, DbType dbType)
        {
            Name = name;
            Value = value;
            DbType = dbType;
        }
    }
}
