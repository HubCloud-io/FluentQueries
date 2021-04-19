using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries.Abstract
{
    public interface IDataField
    {
        string Name { get; set; }
        object Value { get; set; }
        Type ValueType { get; set; }
    }
}
