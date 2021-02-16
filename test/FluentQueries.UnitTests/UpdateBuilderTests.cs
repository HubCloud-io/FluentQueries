﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentQueries.QueryBuilders;
using FluentQueries.Models;
using System.Linq;

namespace FluentQueries.UnitTests
{
    [TestFixture]
    public class UpdateBuilderTests
    {
        [Test]
        public void Query_UpdateValues()
        {
            var fields = new Dictionary<string, object>();
            fields.Add("Uid", Guid.Parse("{14A791DC-8C9E-4F23-A542-B12F848F497A}"));
            fields.Add("Number", 1);
            fields.Add("Date", new DateTime(2021, 02, 16));
            fields.Add("fld_abc123", 1);

            var parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("id", 1));

            var query = UpdateBuilder.Make()
                .Table("Table")
                .Set(fields)
                .Where("Id = @id", parameters)
                .Query();

            var ethalon = @"UPDATE [Table]
SET
[Uid] = @uid
,[Number] = @number
,[Date] = @date
,[fld_abc123] = @fld_abc123
WHERE Id = @id
";

            Console.WriteLine(ethalon);
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(5, query.Parameters.Count());


        }
    }
}
