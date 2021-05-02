using RF.Library.LinqExtensions;
using Xunit;

namespace RF.UnitTests.Libraries.LinqExtensions
{
    public class PredicateBuilderTest
    {
        private readonly string _predicateCompareId = "x => (x.Id == Convert(1, Nullable`1))";

        private readonly string _predicateCompareIdAnd =
            "x => ((x.Id == Convert(1, Nullable`1)) AndAlso (x.Id == Convert(1, Nullable`1)))";

        private readonly string _predicateCompareIdOr =
            "x => ((x.Id == Convert(1, Nullable`1)) OrElse (x.Id == Convert(1, Nullable`1)))";

        private readonly string _predicateCompareIdNot =
            "x => Not((x.Id == Convert(1, Nullable`1)))";

        [Fact]
        public void CreatePredicateBuilderNew_WithNoParameter_ShouldReturnFalse()
        {
            //Arrange
            var predicate = PredicateBuilder.New<TestClass>();

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal("param => False", predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderNew_WithTrueParameter_ShouldReturnTrue()
        {
            //Arrange
            var predicate = PredicateBuilder.New<TestClass>(true);

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal("param => True", predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderNew_WithFalseParameter_ShouldReturnFalse()
        {
            //Arrange
            var predicate = PredicateBuilder.New<TestClass>(false);

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal("param => False", predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderTrue_WithNoParameters_ShouldReturnTrue()
        {
            //Arrange
            var predicate = PredicateBuilder.True<TestClass>();

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal("param => True", predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderFalse_WithNoParameters_ShouldReturnFalse()
        {
            //Arrange
            var predicate = PredicateBuilder.False<TestClass>();

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal("param => False", predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderCreate_WithPredicate_ShouldReturnComparison()
        {
            //Arrange
            var predicate = PredicateBuilder.Create<TestClass>(x => x.Id == 1);

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal(_predicateCompareId, predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderAnd_WithPredicate_ShouldReturnComparison()
        {
            //Arrange
            var predicate = PredicateBuilder.And<TestClass>(x => x.Id == 1, x => x.Id == 1);

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal(_predicateCompareIdAnd, predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderOr_WithPredicate_ShouldReturnComparison()
        {
            //Arrange
            var predicate = PredicateBuilder.Or<TestClass>(x => x.Id == 1, x => x.Id == 1);

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal(_predicateCompareIdOr, predicate.ToString());
        }

        [Fact]
        public void CreatePredicateBuilderNot_WithPredicate_ShouldReturnComparison()
        {
            //Arrange
            var predicate = PredicateBuilder.Not<TestClass>(x => x.Id == 1);

            //Assert
            Assert.Single(predicate.Parameters);
            Assert.Equal(_predicateCompareIdNot, predicate.ToString());
        }

        public class TestClass
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
        }
    }
}