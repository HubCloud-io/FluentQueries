using FluentQueries.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries.UnitTests
{
    [TestFixture]
    public class QueryFieldTests
    {
        [Test]
        public void Expression_Field()
        {
            var queryField = new QueryField("Id");

            Assert.AreEqual("[Id]", queryField.Expression);
        }

        [Test]
        public void Expression_FieldAndAlias()
        {
            var queryField = new QueryField("Id","Identifier");

            Assert.AreEqual("[Id] AS Identifier", queryField.Expression);

        }

        [Test]
        public void Expression_FielAliasTable()
        {
            var queryField = new QueryField("Id", "Identifier", "Table");

            Assert.AreEqual("[Table].[Id] AS Identifier", queryField.Expression);

        }

        [Test]
        public void Expression_FieldAndTable()
        {
            var queryField = new QueryField("Id", null, "Table");

            Assert.AreEqual("[Table].[Id]", queryField.Expression);

        }

    }
}
