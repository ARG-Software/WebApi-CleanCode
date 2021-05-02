using System;
using System.IO;
using System.Text;
using AutoFixture;
using FakeItEasy;
using Hangfire;
using Hangfire.Server;
using Hangfire.Storage;
using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.ValueObjects;

namespace RF.UnitTests.Services.Fixtures
{
    public abstract class CommonConfigurationAndMethodsForServicesTest
    {
        // TODO: Change this to fixtures,  https://xunit.net/docs/shared-context.html#class-fixture

        #region Mocked Repositories

        protected readonly PerformContext FakePerformContext;
        protected readonly IReadWriteRepository<DistributionAgreement> FakeDistributionAgreementRepository;
        protected readonly IReadWriteRepository<DistributionAgreementDetail> FakeDistributionAgreementDetailRepository;
        protected readonly IReadWriteRepository<EpisodeAlias> FakeEpisodeAliasRepository;
        protected readonly IReadWriteRepository<ISRC> FakeIsrcRepository;
        protected readonly IReadWriteRepository<ISWC> FakeIswcRepository;
        protected readonly IReadWriteRepository<LabelAlias> FakeLabelAliasRepository;
        protected readonly IReadWriteRepository<PaymentReceived> FakePaymentReceivedRepository;
        protected readonly IReadWriteRepository<PlatformTierAlias> FakePlatformAliasRepository;
        protected readonly IReadWriteRepository<ProductionTitleAlias> FakeProductionTitleAliasRepository;
        protected readonly IReadWriteRepository<PublisherAlias> FakePublisherAliasRepository;
        protected readonly IReadWriteRepository<RoyaltyDistribution> FakeRoyaltyDistributionRepository;
        protected readonly IReadWriteRepository<RoyaltyTypeAlias> FakeRoyaltyTypeAliasRepository;
        protected readonly IReadWriteRepository<SocietyAlias> FakeSocietyAliasRepository;
        protected readonly IReadWriteRepository<SongAlias> FakeSongAliasRepository;
        protected readonly IReadWriteRepository<Source> FakeSourceRepository;
        protected readonly IReadWriteRepository<SourceSongCode> FakeSongSourceCode;
        protected readonly IReadWriteRepository<StatementDetail> FakeStatementDetailRepository;
        protected readonly IReadWriteRepository<StatementHeader> FakeStatementHeaderRepository;
        protected readonly IReadWriteRepository<Template> FakeTemplateRepository;
        protected readonly IReadWriteRepository<TerritoryAlias> FakeTerritoryAliasRepository;
        protected readonly IReadWriteRepository<WriterShare> FakeWriterShareRepository;

        #endregion Mocked Repositories

        protected readonly IUnitOfWork MockUnitOfWork;

        protected readonly Fixture Fixture;

        protected CommonConfigurationAndMethodsForServicesTest()
        {
            FakePerformContext = GeneratePerformContext();
            FakeDistributionAgreementDetailRepository = A.Fake<IReadWriteRepository<DistributionAgreementDetail>>();
            FakeDistributionAgreementRepository = A.Fake<IReadWriteRepository<DistributionAgreement>>();
            FakeEpisodeAliasRepository = A.Fake<IReadWriteRepository<EpisodeAlias>>();
            FakeIsrcRepository = A.Fake<IReadWriteRepository<ISRC>>();
            FakeIswcRepository = A.Fake<IReadWriteRepository<ISWC>>();
            FakeLabelAliasRepository = A.Fake<IReadWriteRepository<LabelAlias>>();
            FakePaymentReceivedRepository = A.Fake<IReadWriteRepository<PaymentReceived>>();
            FakePlatformAliasRepository = A.Fake<IReadWriteRepository<PlatformTierAlias>>();
            FakeProductionTitleAliasRepository = A.Fake<IReadWriteRepository<ProductionTitleAlias>>();
            FakePublisherAliasRepository = A.Fake<IReadWriteRepository<PublisherAlias>>();
            FakeRoyaltyDistributionRepository = A.Fake<IReadWriteRepository<RoyaltyDistribution>>();
            FakeRoyaltyTypeAliasRepository = A.Fake<IReadWriteRepository<RoyaltyTypeAlias>>();
            FakeSocietyAliasRepository = A.Fake<IReadWriteRepository<SocietyAlias>>();
            FakeSongAliasRepository = A.Fake<IReadWriteRepository<SongAlias>>();
            FakeSongSourceCode = A.Fake<IReadWriteRepository<SourceSongCode>>();
            FakeSourceRepository = A.Fake<IReadWriteRepository<Source>>();
            FakeStatementDetailRepository = A.Fake<IReadWriteRepository<StatementDetail>>();
            FakeStatementHeaderRepository = A.Fake<IReadWriteRepository<StatementHeader>>();
            FakeTemplateRepository = A.Fake<IReadWriteRepository<Template>>();
            FakeTerritoryAliasRepository = A.Fake<IReadWriteRepository<TerritoryAlias>>();
            FakeWriterShareRepository = A.Fake<IReadWriteRepository<WriterShare>>();

            MockUnitOfWork = A.Fake<IUnitOfWork>();

            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            Fixture.Register<ITemplateDefinition>(() => Fixture.Create<TemplateDefinition>());
            Fixture.Register<ITemplateFieldDefinition>(() => Fixture.Create<TemplateFieldDefinition>());
        }

        #region Statement Service Mocked Methods

        protected Stream MockFileStream()
        {
            var fakeStream = (Stream)new MemoryStream(Encoding.UTF8.GetBytes("any stream here"));
            return fakeStream;
        }

        #endregion Statement Service Mocked Methods

        private PerformContext GeneratePerformContext()
        {
            var connection = A.Fake<IStorageConnection>();
            var storage = A.Fake<JobStorage>();

            var backgroundJob = new BackgroundJob("fakeJob", null, DateTime.Now);
            var performContext = new PerformContext(storage, connection, backgroundJob, A.Fake<IJobCancellationToken>());

            return performContext;
        }
    }
}