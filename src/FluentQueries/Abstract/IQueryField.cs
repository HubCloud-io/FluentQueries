using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries.Abstract
{
    public interface IQueryField
    {
        string Table { get; set; }
        string Field { get; set; }
        string Alias { get; set; }

        string Expression { get; }
    }
}
