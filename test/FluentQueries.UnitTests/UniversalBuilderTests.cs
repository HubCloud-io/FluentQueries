using NUnit.Framework;
using System.Data;
using System.Linq;
using FluentQueries.QueryBuilders;

namespace FluentQueries.UnitTests
{
    [TestFixture]
    public class UniversalBuilderTests
    {
        [Test]
        public void TestMake()
        {
            var builder = UniversalBuilder.Make();
            Assert.IsNotNull(builder);
        }

        [Test]
        public void TestAppend()
        {
            var builder = UniversalBuilder.Make().Append("SELECT * FROM Users");
            var query = builder.ToQuery();

            Assert.AreEqual("SELECT * FROM Users", query.Text);
        }

        [Test]
        public void TestAppendIf()
        {
            var builder = UniversalBuilder.Make().AppendIf("SELECT * FROM Users", true);
            var query = builder.ToQuery();

            Assert.AreEqual("SELECT * FROM Users", query.Text);
        }

        [Test]
        public void TestAppendIf_NotAppended()
        {
            var builder = UniversalBuilder.Make().AppendIf("SELECT * FROM Users", false);
            var query = builder.ToQuery();

            Assert.AreEqual("", query.Text);
        }

        [Test]
        public void TestAppendLine()
        {
            var builder = UniversalBuilder.Make().AppendLine("SELECT * FROM Users");
            var query = builder.ToQuery();

            Assert.AreEqual("SELECT * FROM Users" + System.Environment.NewLine, query.Text);
        }

        [Test]
        public void TestAppendLineIf()
        {
            var builder = UniversalBuilder.Make().AppendLineIf("SELECT * FROM Users", true);
            var query = builder.ToQuery();

            Assert.AreEqual("SELECT * FROM Users" + System.Environment.NewLine, query.Text);
        }

        [Test]
        public void TestAppendLineIf_NotAppended()
        {
            var builder = UniversalBuilder.Make().AppendLineIf("SELECT * FROM Users", false);
            var query = builder.ToQuery();

            Assert.AreEqual("", query.Text);
        }

        [Test]
        public void TestParameter_DbType()
        {
            var builder = UniversalBuilder.Make().Parameter("Name", "John Doe", DbType.String);
            var query = builder.ToQuery();

            Assert.AreEqual(1, query.Parameters.Count());
            Assert.AreEqual("Name", query.Parameters.First().Name);
            Assert.AreEqual("John Doe", query.Parameters.First().Value);
            Assert.AreEqual(DbType.String, query.Parameters.First().DbType);
        }

        [Test]
        public void TestParameter()
        {
            var builder = UniversalBuilder.Make().Parameter("Name", "John Doe");
            var query = builder.ToQuery();

            Assert.AreEqual(1, query.Parameters.Count());
            Assert.AreEqual("Name", query.Parameters.First().Name);
            Assert.AreEqual("John Doe", query.Parameters.First().Value);
        }
    }
}