using FluentQueries.Models;
using FluentQueries.QueryBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FluentQueries.UnitTests
{
    [TestFixture]
    public class SelectBuilderTests
    {
        [Test]
        public void Query_SelectAll()
        {
            var query = SelectBuilder.Make().From("Table").Query();

            Console.WriteLine(query.Text);

            var ethalon = @"SELECT * 
FROM [Table]
";

            Assert.AreEqual(ethalon, query.Text);
        }

        [Test]
        public void Query_SelectOneField()
        {
            var query = SelectBuilder.Make().From("Table").Select("Id").Query();

            Console.WriteLine(query.Text);

            var ethalon = @"SELECT [Id]
FROM [Table]
";

            Assert.AreEqual(ethalon, query.Text);

        }

        [Test]
        public void Query_SelectManyFields()
        {
            var query = SelectBuilder.Make()
                .From("Table")
                .Select("Id")
                .Select("Name", "ItemName")
                .Select("Title")
                .Query();

            Console.WriteLine(query.Text);

            var ethalon = @"SELECT [Id]
,[Name] AS ItemName
,[Title]
FROM [Table]
";

            Assert.AreEqual(ethalon, query.Text);

        }

        [Test]
        public void Query_SelectWithWhere()
        {

            var query = SelectBuilder.Make()
                .Select("Title")
                .From("Table")
                .Where("Id = @id").Parameter("id", 1).Query();

            var ethalon = @"SELECT [Title]
FROM [Table]
WHERE Id = @id
";
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(1, query.Parameters.Count());

        }

        [Test]
        public void Query_SelectWithTwoConditions()
        {

            var query = SelectBuilder.Make()
                .Select("Title")
                .From("Table")
                .Where("DateStart >= @dateStart")
                .Parameter("dateStart", new DateTime(2021,1,1))
                .Where("DateFinish <= @dateFinish")
                .Parameter("dateFinish",new DateTime(2021,12,31))
                .Query();

            var ethalon = @"SELECT [Title]
FROM [Table]
WHERE DateStart >= @dateStart AND DateFinish <= @dateFinish
";
            Assert.AreEqual(ethalon, query.Text);
            Assert.AreEqual(2, query.Parameters.Count());

        }


    }
}
