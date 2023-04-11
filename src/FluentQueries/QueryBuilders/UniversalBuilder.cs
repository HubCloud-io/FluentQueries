using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentQueries.Abstract;
using FluentQueries.Models;

namespace FluentQueries.QueryBuilders
{
    /// <summary>
    /// The UniversalBuilder class provides a fluent interface for building and managing
    /// query strings and parameters in a structured and readable manner.
    /// </summary>
    public class UniversalBuilder
    {
        private readonly List<IQueryParameter> _parameters = new List<IQueryParameter>();
        private readonly StringBuilder _sb = new StringBuilder();
        
        public static UniversalBuilder Make()
        {
            return new UniversalBuilder();
        }

        public UniversalBuilder Append(string value)
        {
            _sb.Append(value);

            return this;
        }
        
        public UniversalBuilder AppendIf(string value, bool condition)
        {
            if (condition)
            {
                _sb.Append(value);
            }

            return this;
        }
        
        public UniversalBuilder AppendLine(string value)
        {
            _sb.AppendLine(value);

            return this;
        }

        public UniversalBuilder AppendLineIf(string value, bool condition)
        {
            if (condition)
            {
                _sb.AppendLine(value);
            }

            return this;
        }
        
        public UniversalBuilder Parameter(string name, object value, DbType dbType)
        {
            var parameter = new QueryParameter(name, value, dbType);

            _parameters.Add(parameter);

            return this;
        }
        
        public UniversalBuilder Parameter(string name, object value)
        {
            var parameter = new QueryParameter(name, value);

            _parameters.Add(parameter);

            return this;
        }

        public IQuery ToQuery()
        {
            var query = new Query();
            query.AddParameters(_parameters);
            query.Text = _sb.ToString();

            return query;
        }
        
    }
    
   
}