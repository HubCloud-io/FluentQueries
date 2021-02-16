using FluentQueries.QueryBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentQueries.UnitTests
{
    [TestFixture]
    public class InsertBuilderTests
    {
        [Test]
        public void Query_InsertValues()
        {
            var fields = new Dictionary<string, object>();
            fields.Add("Uid", Guid.Parse("{14A791DC-8C9E-4F23-A542-B12F848F497A}"));
            fields.Add("Number", 1);
            fields.Add("Date", new DateTime(2021, 02, 16));
            fields.Add("fld_abc123", 1);

            var query = InsertBuilder.Make().Into("Table").Set(fields).Query();

            var ethalon = @"INSERT INTO [Table]
([Uid], [Number], [Date], [fld_abc123])
VALUES(@uid, @number, @date, @fld_abc123)";

            Console.WriteLine(ethalon);
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(4, query.Parameters.Count());

        }
    }
}
