using MockQueryable.Moq;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Infrastructure.Data.Repositories.EfPgSQL;
using RF.Infrastructure.Data.Adapters.EntityFrameworkCore;

namespace RF.UnitTests.Infrastructure.Data.Repositories
{
    public abstract class RepositoryEFPgSqlTestBase : IDisposable
    {
        protected readonly IReadWriteRepository<Artist> Repository;
        protected readonly int TableSize;
        protected Mock<DbSet<Artist>> FakeArtistDbSet { get; }
        protected Mock<EFCoreContext> FakeContext { get; }

        protected RepositoryEFPgSqlTestBase()
        {
            TableSize = 20;
            FakeArtistDbSet = GenerateListOfFakeArtists().AsQueryable().BuildMockDbSet();

            FakeContext = new Mock<EFCoreContext>();

            //FakeContext.Setup(c => c.Artist).Returns(FakeArtistDbSet.Object);
            FakeContext.Setup(c => c.Set<Artist>()).Returns(FakeArtistDbSet.Object);

            Repository = new Repository<Artist>(FakeContext.Object);
        }

        public void Dispose()
        {
        }

        protected IEnumerable<Artist> GenerateListOfFakeArtists()
        {
            var fakeArtists = Enumerable.Range(1, TableSize).Select(i => new Artist
            {
                Id = i,
                Name = $"Name{i}",
                Email = $"musician{i}@gmail.com",
                Label = new Label
                {
                    Id = i,
                    Name = $"Label{i}"
                }
            }).AsEnumerable();

            return fakeArtists;
        }
    }

    internal class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}