using RF.Domain.Entities;
using Moq;
using Xunit;
using System.Linq;
using AutoFixture;
using Moq.Protected;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using RF.Infrastructure.Data.Repositories.EfPgSQL;

namespace RF.UnitTests.Infrastructure.Data.Repositories
{
    public class RepositoryEFPgSqlAsyncTest : RepositoryEFPgSqlTestBase
    {
        [Fact]
        public async void InsertRFEntityAsync_CallAdd_VerifyIfAddWasCalledOnce()
        {
            //Arrange
            var entityToInsert = new Fixture().Create<Artist>();

            //Act
            await Repository.InsertRFEntityAsync(entityToInsert);

            //Assert

            FakeArtistDbSet.Verify(m => m.AddAsync(It.IsAny<Artist>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void InsertRFEntityAsync_ArrayOfEntitiesPassedAsParameter_VerifyIfAddRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToInsert = new Artist[] { new Fixture().Create<Artist>() };

            //Act
            await Repository.InsertRFEntityAsync(entitiesToInsert);

            //Assert
            FakeArtistDbSet.Verify(m => m.AddRangeAsync(It.IsAny<Artist[]>()), Times.Once);
        }

        [Fact]
        public async void InsertRFEntityAsync_ListOfEntitiesPassedAsParameter_VerifyIfAddRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToInsert = new Mock<IEnumerable<Artist>>();

            //Act
            await Repository.InsertRFEntityAsync(entitiesToInsert.Object);

            //Assert
            FakeArtistDbSet.Verify(m => m.AddRangeAsync(It.IsAny<IEnumerable<Artist>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void SingleAsync_FilterExpressionPassedAsParameter_ReturnsEntityThatMatchesTheFilter()
        {
            //Act
            var result = await Repository.SingleAsync(x => x.Id == 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.IsType<Artist>(result);
        }

        [Fact]
        public async void SingleAsync_OrderExpressionPassedAsParameter_ReturnsTheFirstEntityThatMatchesTheCriteria()
        {
            //Arrange
            IOrderedQueryable<Artist> orderingFunc(IQueryable<Artist> query) => query.OrderByDescending(art => art.Id);

            //Act
            var result = await Repository.SingleAsync(null, orderingFunc, true, null);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(20, result.Id);
            Assert.IsType<Artist>(result);
        }

        [Fact]
        public async void SingleAsync_PassIncludedProperties_ReturnsEntityWithRelatedPropertiesNotNull()
        {
            //Act
            var result = await Repository.SingleAsync(null, null, true, x => x.Label);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Label.Name);
            Assert.IsType<Artist>(result);
        }

        [Fact]
        public async void GetAllAsync_FilterExpressionPassedAsParameter_ReturnsListWithFixedSizeAndFiltered()
        {
            //Act
            // Note: The Id's only go from 1 to 20
            var result = await Repository.GetAllAsync(x => x.Id > 15, null);

            //Assert
            Assert.Equal(5, result.Count());
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void GetAllAsync_OrderByIdAsParameter_ReturnsListOrderedByIdDescending()
        {
            //Arrange
            IOrderedQueryable<Artist> orderingFunc(IQueryable<Artist> query) => query.OrderByDescending(art => art.Id);

            //Act
            var result = await Repository.GetAllAsync(null, orderingFunc);

            //Assert
            //Note: The starting id should be 20 now, since its ordered in a descendent way
            Assert.Equal(20, result.First().Id);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void GetAllAsync_PassIncludedProperties_ReturnsListWithRelatedPropertiesNotNull()
        {
            //Act
            var result = await Repository.GetAllAsync(null, null, true, x => x.Label);

            //Assert
            Assert.NotNull(result.First().Label.Name);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void GetListAsync_IndexAndSizePassedAsParameter_ReturnsListWithFixedSize()
        {
            //Act
            var result = await Repository.GetListAsync(null, null, 0, 10);

            //Assert
            Assert.Equal(10, result.Count());
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void GetListAsync_PassSelectorAsParameter_ReturnsNewListWithSelectorTypePassed()
        {
            //Act
            var result = await Repository.GetListAsync<TestDto>(x => new TestDto
            {
                Id = x.Id,
                Name = x.Name
            }, null, null, 0, 10);

            //Assert
            Assert.Equal(10, result.Count());
            Assert.IsType<TestDto>(result.First());
            Assert.IsAssignableFrom<IEnumerable<TestDto>>(result);
        }

        [Fact]
        public async void GetListAsync_FilterExpressionPassedAsParameter_ReturnsListWithFixedSizeAndFiltered()
        {
            //Act
            // Note: The Id's only go from 1 to 20
            var result = await Repository.GetListAsync(x => x.Id > 15, null, 0, 20);

            //Assert
            Assert.Equal(5, result.Count());
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void GetListAsync_OrderByIdAsParameter_ReturnsListOrderedByIdDescending()
        {
            //Arrange
            IOrderedQueryable<Artist> orderingFunc(IQueryable<Artist> query) => query.OrderByDescending(art => art.Id);

            //Act
            var result = await Repository.GetListAsync(null, orderingFunc);

            //Assert
            //Note: The starting id should be 20 now, since its ordered in a descendent way
            Assert.Equal(20, result.First().Id);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void GetListAsync_PassIncludedProperties_ReturnsListWithRelatedPropertiesNotNull()
        {
            //Act
            var result = await Repository.GetListAsync(null, null, 0, 20, true, x => x.Label);

            //Assert
            Assert.NotNull(result.First().Label.Name);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public async void CountTableSizeAsync_PassingNoPredicate_ShouldReturnTheDataSetSize()
        {
            //Act
            var result = await Repository.CountTableSizeAsync();

            //Assert
            Assert.Equal(TableSize, result);
        }

        [Fact]
        public async void CountTableSizeAsync_PassingAPredicate_ShouldReturnTheDataSetSizeThatMatchesThePredicatePassed()
        {
            //Act
            var result = await Repository.CountTableSizeAsync(x => x.Id > 10);

            //Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public async void CountTableSizeAsync_PassingAPredicateAndIncludedProperties_ShouldReturnTheDataSetSizeThatMatchesThePredicated()
        {
            //Act
            var result = await Repository.CountTableSizeAsync(x => x.Label.Id == 1, x => x.Label);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task SearchAsync_PassingKeyParameterToSearch_ShouldReturnEntityWithSameKeyAsync()
        {
            //Arrange
            var entityFound = new Artist()
            {
                Id = 1,
                Name = "john doe"
            };
            FakeArtistDbSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(entityFound);

            //Act
            var result = await Repository.SearchAsync();

            //Assert

            FakeArtistDbSet.Verify(m => m.FindAsync(It.IsAny<object[]>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(result.Name, entityFound.Name);
        }
    }

    public class RepositoryEFPgSqlTest : RepositoryEFPgSqlTestBase
    {
        [Fact]
        public void InsertRFEntity_CallAdd_VerifyIfAddWasCalledOnce()
        {
            //Arrange
            var entityToInsert = new Mock<Artist>();

            //Act
            Repository.InsertRFEntity(entityToInsert.Object);

            //Assert

            FakeArtistDbSet.Verify(m => m.Add(It.IsAny<Artist>()), Times.Once);
        }

        [Fact]
        public void InsertRFEntity_ArrayOfEntitiesPassedAsParameter_VerifyIfAddRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToInsert = new Artist[] { new Fixture().Create<Artist>() };

            //Act
            Repository.InsertRFEntity(entitiesToInsert);

            //Assert
            FakeArtistDbSet.Verify(m => m.AddRange(It.IsAny<Artist[]>()), Times.Once);
        }

        [Fact]
        public void InsertRFEntity_ListOfEntitiesPassedAsParameter_VerifyIfAddRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToInsert = new Mock<IEnumerable<Artist>>();

            //Act
            Repository.InsertRFEntity(entitiesToInsert.Object);

            //Assert
            FakeArtistDbSet.Verify(m => m.AddRange(It.IsAny<IEnumerable<Artist>>()), Times.Once);
        }

        [Fact]
        public void UpdateRFEntity_CallUpdate_VerifyIfUpdateWasCalledOnce()
        {
            //Arrange
            var entityToUpdate = new Mock<Artist>();
            FakeArtistDbSet.Setup(x => x.Find(It.IsAny<Artist>())).Returns(entityToUpdate.Object);

            //Act
            Repository.UpdateRFEntity(entityToUpdate.Object);

            //Assert

            FakeArtistDbSet.Verify(m => m.Update(It.IsAny<Artist>()), Times.Once);
        }

        [Fact]
        public void UpdateRFEntity_ArrayOfEntitiesPassedAsParameter_VerifyIfUpdateRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToUpdate = new Artist[] { new Fixture().Create<Artist>() };

            //Act
            Repository.UpdateRFEntity(entitiesToUpdate);

            //Assert
            FakeArtistDbSet.Verify(m => m.UpdateRange(It.IsAny<Artist[]>()), Times.Once);
        }

        [Fact]
        public void UpdateRFEntity_ListOfEntitiesPassedAsParameter_VerifyIfUpdateRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToUpdate = new Mock<IEnumerable<Artist>>();

            //Act
            Repository.UpdateRFEntity(entitiesToUpdate.Object);

            //Assert
            FakeArtistDbSet.Verify(m => m.UpdateRange(It.IsAny<IEnumerable<Artist>>()), Times.Once);
        }

        [Fact]
        public void DeleteRFEntity_CallRemove_VerifyIfRemoveWasCalledOnce()
        {
            //Arrange
            var entityToDelete = new Mock<Artist>();
            FakeArtistDbSet.Setup(x => x.Find(It.IsAny<Artist>())).Returns(entityToDelete.Object);

            //Act
            Repository.DeleteRFEntity(entityToDelete.Object);

            //Assert

            FakeArtistDbSet.Verify(m => m.Remove(It.IsAny<Artist>()), Times.Once);
        }

        [Fact]
        public void DeleteRFEntity_ArrayOfEntitiesPassedAsParameter_VerifyIfRemoveRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToDelete = new Artist[] { new Fixture().Create<Artist>() };

            //Act
            Repository.DeleteRFEntity(entitiesToDelete);

            //Assert
            FakeArtistDbSet.Verify(m => m.RemoveRange(It.IsAny<Artist[]>()), Times.Once);
        }

        [Fact]
        public void DeleteRFEntity_ListOfEntitiesPassedAsParameter_VerifyIfRemoveRangeWasCalledOnce()
        {
            //Arrange
            var entitiesToDelete = new Mock<IEnumerable<Artist>>();

            //Act
            Repository.DeleteRFEntity(entitiesToDelete.Object);

            //Assert
            FakeArtistDbSet.Verify(m => m.RemoveRange(It.IsAny<IEnumerable<Artist>>()), Times.Once);
        }

        [Fact]
        public void Single_FilterExpressionPassedAsParameter_ReturnsEntityThatMatchesTheFilter()
        {
            //Act
            var result = Repository.Single(x => x.Id == 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.IsType<Artist>(result);
        }

        [Fact]
        public void Single_OrderExpressionPassedAsParameter_ReturnsTheFirstEntityThatMatchesTheCriteria()
        {
            //Arrange
            IOrderedQueryable<Artist> orderingFunc(IQueryable<Artist> query) => query.OrderByDescending(art => art.Id);

            //Act
            var result = Repository.Single(null, orderingFunc, true, null);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(20, result.Id);
            Assert.IsType<Artist>(result);
        }

        [Fact]
        public void Single_PassIncludedProperties_ReturnsEntityWithRelatedPropertiesNotNull()
        {
            //Act
            var result = Repository.Single(null, null, true, x => x.Label);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Label.Name);
            Assert.IsType<Artist>(result);
        }

        [Fact]
        public void GetList_IndexAndSizePassedAsParameter_ReturnsListWithFixedSize()
        {
            //Act
            var result = Repository.GetList(null, null, 0, 10);

            //Assert
            Assert.Equal(10, result.Count());
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void GetList_PassSelectorAsParameter_ReturnsNewListWithSelectorTypePassed()
        {
            //Act
            var result = Repository.GetList<TestDto>(x => new TestDto
            {
                Id = x.Id,
                Name = x.Name
            }, null, null, 0, 10);

            //Assert
            Assert.Equal(10, result.Count());
            Assert.IsType<TestDto>(result.First());
            Assert.IsAssignableFrom<IEnumerable<TestDto>>(result);
        }

        [Fact]
        public void GetList_FilterExpressionPassedAsParameter_ReturnsListWithFixedSizeAndFiltered()
        {
            //Act
            // Note: The Id's only go from 1 to 20
            var result = Repository.GetList(x => x.Id > 15, null, 0, 20);

            //Assert
            Assert.Equal(5, result.Count());
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void GetList_OrderByIdAsParameter_ReturnsListOrderedByIdDescending()
        {
            //Arrange
            IOrderedQueryable<Artist> orderingFunc(IQueryable<Artist> query) => query.OrderByDescending(art => art.Id);

            //Act
            var result = Repository.GetList(null, orderingFunc);

            //Assert
            //Note: The starting id should be 20 now, since its ordered in a descendent way
            Assert.Equal(20, result.First().Id);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void GetList_PassIncludedProperties_ReturnsListWithRelatedPropertiesNotNull()
        {
            //Act
            var result = Repository.GetList(null, null, 0, 20, true, x => x.Label);

            //Assert
            Assert.NotNull(result.First().Label.Name);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void GetAll_FilterExpressionPassedAsParameter_ReturnsListWithFixedSizeAndFiltered()
        {
            //Act
            // Note: The Id's only go from 1 to 20
            var result = Repository.GetAll(x => x.Id > 15, null);

            //Assert
            Assert.Equal(5, result.Count());
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void GetAll_OrderByIdAsParameter_ReturnsListOrderedByIdDescending()
        {
            //Arrange
            IOrderedQueryable<Artist> orderingFunc(IQueryable<Artist> query) => query.OrderByDescending(art => art.Id);

            //Act
            var result = Repository.GetAll(null, orderingFunc);

            //Assert
            //Note: The starting id should be 20 now, since its ordered in a descendent way
            Assert.Equal(20, result.First().Id);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void GetAll_PassIncludedProperties_ReturnsListWithRelatedPropertiesNotNull()
        {
            //Act
            var result = Repository.GetAll(null, null, true, x => x.Label);

            //Assert
            Assert.NotNull(result.First().Label.Name);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }

        [Fact]
        public void CountTableSize_PassingNoPredicate_ShouldReturnTheDataSetSize()
        {
            //Act
            var result = Repository.CountTableSize();

            //Assert
            Assert.Equal(TableSize, result);
        }

        [Fact]
        public void CountTableSize_PassingAPredicate_ShouldReturnTheDataSetSizeThatMatchesThePredicatePassed()
        {
            //Act
            var result = Repository.CountTableSize(x => x.Id > 10);

            //Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void CountTableSize_PassingAPredicateAndIncludedProperties_ShouldReturnTheDataSetSizeThatMatchesThePredicated()
        {
            //Act
            var result = Repository.CountTableSize(x => x.Label.Id == 1, x => x.Label);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Search_PassingKeyParameterToSearch_ShouldReturnEntityWithSameKey()
        {
            //Arrange
            var entityFound = new Artist()
            {
                Id = 1,
                Name = "john doe"
            };
            FakeArtistDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns(entityFound);

            //Act
            var result = Repository.Search();

            //Assert

            FakeArtistDbSet.Verify(m => m.Find(It.IsAny<object[]>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(result.Name, entityFound.Name);
        }

        [Fact]
        public void Query_PassSqlAndParameters_ShouldReturnTheSetThatMatchesTheQuery()
        {
            //Arrange
            var mockedRepository = new Mock<Repository<Artist>>(FakeContext.Object);
            var fakeArtistsList = GenerateListOfFakeArtists().AsQueryable();

            // This is white box testing, it should be avoided, but in order to test a static method in the repository
            // this is for now the best solution i've got. Basically here i'm mocking a virtual method that is used to make the query to the
            // database and after it's used in the repository by the method evoked below called Query
            mockedRepository
                .Protected()
                .Setup<IQueryable<Artist>>("MakeSqlQuery", ItExpr.IsAny<string>(), ItExpr.IsAny<object[]>())
                .Returns(fakeArtistsList);

            ////Act
            var result = mockedRepository.Object.Query("Select fakes where id={0}", 1);

            ////Assert
            Assert.Equal(result.Count(), TableSize);
            Assert.IsAssignableFrom<IEnumerable<Artist>>(result);
        }
    }
}