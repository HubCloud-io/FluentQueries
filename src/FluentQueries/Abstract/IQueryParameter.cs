using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FluentQueries.Abstract
{
   public  interface IQueryParameter
    {
         string Name { get; set; }
         object Value { get; set; }
         DbType DbType { get; set; }
    }
}
