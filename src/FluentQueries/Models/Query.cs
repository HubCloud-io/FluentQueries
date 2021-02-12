using FluentQueries.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries.Models
{
    public class Query : IQuery
    {
        private List<IQueryParameter> _parameters = new List<IQueryParameter>();

        public string Text { get; set; }
        public IEnumerable<IQueryParameter> Parameters => _parameters;

        public void AddParameters(IEnumerable<IQueryParameter> parameters)
        {
            _parameters.AddRange(parameters);
        }
    }
}
