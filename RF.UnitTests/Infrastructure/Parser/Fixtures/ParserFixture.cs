using RF.Domain.Interfaces.ValueObjects;

namespace RF.UnitTests.Infrastructure.Parser.Fixtures
{
    using Moq;
    using System;
    using System.IO;
    using RF.Infrastructure.Parser;

    public class ParserFixture : IDisposable
    {
        private Mock<ITemplateDefinition> Source { get; }

        private const string PathFile = @"Infrastructure/Parser/Resources/ParserTestFile.xlsx";

        public ParserFixture()
        {
            Source = new Mock<ITemplateDefinition>();
        }

        public NpoiParser<T> GetParser<T>() where T : class, new()
        {
            return new NpoiParser<T>();
        }

        public MemoryStream LoadFile()
        {
            using (var fileStream = new FileStream(PathFile, FileMode.Open, FileAccess.Read))
            {
                var memoryStream = new MemoryStream();
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);

                return memoryStream;
            }
        }

        public void MockSourceParameters(int startingLine, int? worksheetNumber)
        {
            Mock.Get(Source.Object)
                .Setup(x => x.StartingLine)
                .Returns(startingLine);
            Mock.Get(Source.Object)
                .Setup(x => x.WorkSheetNumber)
                .Returns(worksheetNumber);
        }

        public ITemplateDefinition GetSourceMockObject()
        {
            return Source.Object;
        }

        public void Dispose()
        {
        }
    }
}