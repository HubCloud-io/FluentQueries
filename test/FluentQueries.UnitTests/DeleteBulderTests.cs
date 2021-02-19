using FluentQueries.QueryBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQueries.UnitTests
{
    [TestFixture]
    public class DeleteBulderTests
    {
        [Test]
        public void Query_DeleteItemById()
        {
            var query = DeleteBuilder.Make()
                .Table("Table")
                .Where("Id = @id")
                .Parameter("id", 1)
                .Query();

            var ethalon = @"DELETE [Table]
WHERE Id = @id
";
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(1, query.Parameters.Count());

        }
    }
}
