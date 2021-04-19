using FluentQueries.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries.Models
{
    public class DataField : IDataField
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type ValueType { get; set; }

        public DataField()
        {

        }

        public DataField(string name, object value, Type valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }
    }
}
