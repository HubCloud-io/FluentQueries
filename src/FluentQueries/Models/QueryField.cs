using FluentQueries.Abstract;

namespace FluentQueries.Models
{
    public class QueryField : IQueryField
    {
        public string Table { get; set; }
        public string Field { get; set; }
        public string Alias { get; set; }

        public string Expression
        {
            get
            {
                var expression = "";
                if (string.IsNullOrEmpty(Table))
                {
                    expression = $"[{Field}]";
                }
                else
                {
                    expression = $"[{Table}].[{Field}]";
                }

                if (!string.IsNullOrEmpty(Alias))
                {
                    expression = expression + " AS " + Alias;
                }

                return expression;
            }
        }

        public QueryField()
        {

        }

        public QueryField(string field)
        {
            Field = field;
        }

        public QueryField(string field, string alias):this(field)
        {
            Alias = alias;
        }

        public QueryField(string field, string alias, string table):this(field, alias)
        {
            Table = table;
        }

        public override string ToString()
        {
            return Expression;
        }
    }
}
