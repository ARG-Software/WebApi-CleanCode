using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using Hangfire.Server;
using RF.Application.Core.UseCases.Isrc.Queries.GetISRCBySongId;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdByIsrcOrIswcId;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdBySourceSongCode;
using RF.Application.Core.UseCases.Template.Queries.GetTemplateDefinitionObjectById;
using RF.Domain.Dto;
using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Bus;
using RF.Domain.Interfaces.Parser;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.ValueObjects;
using RF.Services.ETL;
using RF.Services.ETL.Common.Exceptions;
using RF.Services.Interfaces.ETL;
using RF.UnitTests.Services.Fixtures;
using Xunit;

namespace RF.UnitTests.Services.ETL
{
    public class ETLServiceTest : CommonConfigurationAndMethodsForServicesTest
    {
        private readonly ETLService _service;
        private readonly IRoyaltiesService _fakeRoyaltyService;
        private readonly IParser<ParsedStatement> _fakeParser;
        private readonly IETLContextService _fakeContextService;
        private readonly IStatementService _fakeStatementService;
        private readonly IStatementErrors _fakeStatementErrors;

        private readonly IMemoryBus _fakeBus;

        private readonly IEnumerable<ParsedStatement> _fakeParsedStatements;

        private const int FakeInputTemplateId = 1;
        private const int FakePaymentId = 1;
        private const int NumberOfElementsInList = 1;

        private const string FakeJobName = "fakejob";

        private readonly RoyaltyManagerProcessmentDto _fakeRoyaltyManagerProcessmentDto;

        public ETLServiceTest()
        {
            _service = A.Fake<ETLService>(options => options.CallsBaseMethods());

            _fakeContextService = A.Fake<IETLContextService>();
            _fakeParser = A.Fake<IParser<ParsedStatement>>();
            _fakeRoyaltyService = A.Fake<IRoyaltiesService>();
            _fakeStatementService = A.Fake<IStatementService>();
            _fakeStatementErrors = A.Fake<IStatementErrors>();

            _fakeBus = A.Fake<IMemoryBus>();

            _service.ContextService = _fakeContextService;
            _service.Parser = _fakeParser;
            _service.RoyaltiesService = _fakeRoyaltyService;
            _service.StatementService = _fakeStatementService;
            _service.StatementProcessingErrors = _fakeStatementErrors;
            _service.Bus = _fakeBus;

            _fakeParsedStatements = A.CollectionOfFake<ParsedStatement>(NumberOfElementsInList);

            _fakeRoyaltyManagerProcessmentDto = new RoyaltyManagerProcessmentDto()
            {
                PaymentId = FakePaymentId,
                TemplateId = FakeInputTemplateId,
                StatementList = _fakeParsedStatements,
                Tags = new[] { "fakeTag" }
            };

            A.CallTo(_fakeRoyaltyService)
                .WithReturnType<IEnumerable<RoyaltyDistribution>>()
                .Returns(Fixture.CreateMany<RoyaltyDistribution>(2));

            A.CallTo(_service)
                .WithReturnType<bool>()
                .Returns(true);
        }

        [Fact]
        public async void ParseStatementFile_IfStreamIsNull_ThrowException()
        {
            //Arrange
            const int mockInputSourceId = 1;

            //Act
            var exception =
                await Assert.ThrowsAsync<ETLException>(() => _service.ParseStatementFile(mockInputSourceId, null));

            // Assert
            Assert.Equal("File is null, please correct it and try again", exception.Message);
        }

        [Fact]
        public async void ParseStatementFile_TemplateCantBeConvertedToTemplateStructure_ThrowException()
        {
            //Arrange
            const int mockInputSourceId = 1;
            var mockStream = MockFileStream();

            A.CallTo(() => _fakeBus.Send(A<GetTemplateDefinitionObjectByIdQuery>.Ignored, A<CancellationToken>.Ignored))
                .Throws<Exception>();

            //Act && Assert

            var exception =
                await Assert.ThrowsAsync<Exception>(() => _service.ParseStatementFile(mockInputSourceId, mockStream));
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public async void ParseStatementFile_CouldntParseStatementFile_ThrowException()
        {
            //Arrange
            const int mockInputSourceId = 1;

            var mockStream = MockFileStream();

            A.CallTo(() => _fakeParser.ConvertStreamFileToObjectList(A<Stream>.Ignored, A<ITemplateDefinition>.Ignored))
                .Returns((IEnumerable<ParsedStatement>)null);

            A.CallTo(() => _fakeBus.Send(A<GetTemplateDefinitionObjectByIdQuery>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Fixture.Create<ITemplateDefinition>());

            //Act && Assert
            var exception =
                await Assert.ThrowsAsync<ETLException>(() =>
                    _service.ParseStatementFile(mockInputSourceId, mockStream));
            Assert.Equal("Couldn't parse statement file, please correct it and try again", exception.Message);

            mockStream.Close();
        }

        [Fact]
        public async void ParseStatementFile_FileIsCorrectlyParsed_ReturnListWithParsedStatements()
        {
            //Arrange

            var fakeStream = MockFileStream();
            var fakeTemplateId = 1;

            A.CallTo(() => _fakeParser.ConvertStreamFileToObjectList(A<Stream>.Ignored, A<ITemplateDefinition>.Ignored))
                .Returns(_fakeParsedStatements);

            A.CallTo(() => _fakeBus.Send(A<GetTemplateDefinitionObjectByIdQuery>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Fixture.Create<ITemplateDefinition>());

            //Act
            var result = await _service.ParseStatementFile(fakeTemplateId, fakeStream);
            fakeStream.Close();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<IParsedStatement>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async void ProcessFileInBackground_WhenStartingToProcessFile_SetVariablesForETLProcessment()
        {
            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() => _fakeContextService.SetPaymentForETLProcessment(A<int>.Ignored))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeContextService.SetBackgroundTaskContext(A<PerformContext>.Ignored))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeContextService.SetBackgroundTaskContextTags(_fakeRoyaltyManagerProcessmentDto.Tags))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void ProcessFileInBackground_IfNoErrorsDuringParsing_MakeAllNecessaryCallsToProcessStatements()
        {
            //Arrange
            A.CallTo(() => _fakeStatementErrors.HasAny())
                .Returns(false);

            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() => _fakeStatementService.CreateStatementHeader(A<int>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeStatementService.InsertStatementHeader(A<StatementHeader>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeStatementService.InsertStatementDetails(A<StatementHeader>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void ProcessFileInBackground_IfNoErrorsDuringParsingAndWhenProcessingStatements_MakeAllNecessaryCallsToProcessRoyalties()
        {
            //Arrange
            var fakeStatementDetails = A.CollectionOfFake<StatementDetail>(2).ToList();

            A.CallTo(() => _fakeContextService.GetStatementDetailsToBeInserted())
                .Returns(fakeStatementDetails);

            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert

            A.CallTo(() => _fakeRoyaltyService.RoyaltiesDistributionCalculation(fakeStatementDetails))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeRoyaltyService.InsertRoyaltiesDistributions(A<IEnumerable<RoyaltyDistribution>>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ProcessFileInBackground_IfNoErrorsDuringParsingAndWhenProcessingStatementsAndNoRoyaltiesCouldBeCalculated_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeRoyaltyService.RoyaltiesDistributionCalculation(A<IEnumerable<StatementDetail>>.Ignored))
                .Returns(new List<RoyaltyDistribution>());

            //Act && Assert

            var exception = Assert.ThrowsAsync<ETLException>(() => _service
                .ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext));
            Assert.Equal("Couldn't calculate any royalties for current file", exception.Result.Message);
        }

        [Fact]
        public async void ProcessFileInBackground_IfPaymentCanBeReconciled_MarkPaymentAsReconciled()
        {
            //Arrange
            A.CallTo(() => _fakeContextService.CheckIfPaymentCanBeReconciled(A.Dummy<StatementHeader>()))
                .WithAnyArguments()
                .Returns(true);

            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() => _fakeContextService.MarkPaymentAsReconciled())
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ProcessFileInBackground_IfAfterProcessingRoyaltiesAndStatementsCantCommitDataToDatabase_ThrowException()
        {
            //Arrange
            A.CallTo(() => _service.CommitDatabasePendingChanges())
                .Returns(false);

            //Act && Assert
            var exception = Assert.ThrowsAsync<ETLException>(() => _service
                .ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext));
            Assert.Equal("Couldn't commit statements and royalties to database.", exception.Result.Message);
        }

        [Fact]
        public void ProcessFileInBackground_IfHasErrorsDuringProcessment_ThrowException()
        {
            //Arrange
            A.CallTo(() => _fakeStatementErrors.HasAny())
                .Returns(true);

            //Act && Assert
            var exception = Assert.ThrowsAsync<ETLException>(() => _service
                .ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext));
            Assert.StartsWith("File not processed successfully.", exception.Result.Message);
        }

        [Fact]
        public async void ProcessFileInBackground_IfHasNoErrorsProcessing_ReturnsSuccessMessage()
        {
            //Act
            var result = await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("File processed with success", result);
        }

        [Fact]
        public async void ProcessFileInBackground_AfterBuildStatementDetailSuccessfully_CallAddStatementDetailToInsert()
        {
            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() =>
                    _fakeContextService.AddStatementDetailToInsert(A<StatementDetail>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void ProcessFileInBackground_WhenFillingValuesForIsrcAndIswc_CheckThatFindCodeIsCalledForBoth()
        {
            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() => _fakeStatementService.FindCode<ISRC>(A<string>.Ignored, A<int>.Ignored))
                .MustHaveHappened(NumberOfElementsInList, Times.Exactly);
            A.CallTo(() => _fakeStatementService.FindCode<ISWC>(A<string>.Ignored, A<int>.Ignored))
                .MustHaveHappened(NumberOfElementsInList, Times.Exactly);
        }

        [Fact]
        public async void ProcessFile_IfCouldntFindSongIdByAliasOrSourceSongCode_RetrySongIdFetch()
        {
            //Arrange
            A.CallTo(() => _fakeBus.Send(A<GetSongIdBySourceSongCodeOrAliasQuery>.Ignored, A<CancellationToken>.Ignored))
                .Returns(0);

            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() => _fakeBus.Send(A<GetSongIdByIsrcOrIswcIdQuery>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappened(NumberOfElementsInList, Times.Exactly);
        }

        [Fact]
        public async void ProcessFile_IfSongIdHasValueButIsrcCouldntBeFound_RetryIsrcIdFetch()
        {
            //Arrange
            A.CallTo(() => _fakeBus.Send(A<GetSongIdBySourceSongCodeOrAliasQuery>.Ignored, A<CancellationToken>.Ignored))
                .Returns(2);
            A.CallTo(() => _fakeStatementService.FindCode<ISRC>(A<string>.Ignored, A<int>.Ignored))
                .Returns(null);

            //Act
            await _service.ProcessFileInBackground(FakeJobName, _fakeRoyaltyManagerProcessmentDto, FakePerformContext);

            //Assert
            A.CallTo(() => _fakeBus.Send(A<GetIsrcBySongIdQuery>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappened(NumberOfElementsInList, Times.Exactly);
        }
    }
}