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
        public void Query_InsertRow()
        {
            var row1 = new Dictionary<string, object>();
            row1.Add("Uid", Guid.Parse("{14A791DC-8C9E-4F23-A542-B12F848F497A}"));
            row1.Add("Number", 1);
            row1.Add("Date", new DateTime(2021, 02, 16));
            row1.Add("fld_abc123", 1);

            var query = InsertBuilder.Make().Into("Table", row1.Keys.ToList()).Values(row1.Values.ToList()).Query();

            var ethalon = @"INSERT INTO [Table]
([Uid], [Number], [Date], [fld_abc123])
VALUES
(@parameter1, @parameter2, @parameter3, @parameter4)
";

            Console.WriteLine(ethalon);
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(4, query.Parameters.Count());

        }

        [Test]
        public void Query_InsertTwoRows()
        {
            var row1 = new Dictionary<string, object>();
            row1.Add("Uid", Guid.Parse("{14A791DC-8C9E-4F23-A542-B12F848F497A}"));
            row1.Add("Number", 1);
            row1.Add("Date", new DateTime(2021, 02, 16));
            row1.Add("fld_abc123", 1);

            var row2 = new Dictionary<string, object>();
            row2.Add("Uid", Guid.Parse("{B61A15D5-1B99-4DBE-9D40-989B28A18FEA}"));
            row2.Add("Number", 2);
            row2.Add("Date", new DateTime(2021, 02, 19));
            row2.Add("fld_abc123", 22);


            var query = InsertBuilder.Make()
                .Into("Table", row1.Keys.ToList())
                .Values(row1.Values.ToList())
                .Values(row2.Values.ToList()).Query();

            var ethalon = @"INSERT INTO [Table]
([Uid], [Number], [Date], [fld_abc123])
VALUES
(@parameter1, @parameter2, @parameter3, @parameter4)
,(@parameter5, @parameter6, @parameter7, @parameter8)
";

            Console.WriteLine(ethalon);
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(8, query.Parameters.Count());

        }

    }
}
