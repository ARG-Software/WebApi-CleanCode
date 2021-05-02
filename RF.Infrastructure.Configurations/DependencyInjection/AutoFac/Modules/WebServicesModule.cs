using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using RF.Application.Core.Common.Behaviours;
using RF.Domain.Builders;
using RF.Domain.Entities;
using RF.Domain.Filters;
using RF.Domain.Interfaces.Builders;
using RF.Domain.Interfaces.Bus;
using RF.Domain.Interfaces.Entities;
using RF.Domain.Interfaces.Filters;
using RF.Domain.Interfaces.Logger;
using RF.Domain.Interfaces.Parser;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.Strategies;
using RF.Domain.Interfaces.UnitOfWork;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.Strategies;
using RF.Domain.ValueObjects;
using RF.Infrastructure.Bus;
using RF.Infrastructure.Configurations.Models;
using RF.Infrastructure.Data.Adapters.EntityFrameworkCore;
using RF.Infrastructure.Data.Repositories.EfPgSQL;
using RF.Infrastructure.Data.UnitOfWork.EfPgSql;
using RF.Infrastructure.Logger;
using RF.Infrastructure.Parser;
using RF.Services.ETL;
using RF.Services.Interfaces.ETL;
using Module = Autofac.Module;

namespace RF.Infrastructure.Configurations.DependencyInjection.AutoFac.Modules
{
    public class WebServicesModule : Module
    {
        public WebServicesConfigurations Configurations { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            //Entities, ValueObjects
            builder.RegisterType<ParsedStatement>().As<IParsedStatement>();
            builder.RegisterType<TemplateDefinition>().As<ITemplateDefinition>().InstancePerDependency();
            builder.RegisterType<Source>().As<ISource>().InstancePerDependency();
            builder.RegisterType<Template>().As<ITemplate>().InstancePerDependency();

            builder.RegisterType<StatementErrors>().As<IStatementErrors>().InstancePerLifetimeScope();

            builder.RegisterType<DistributionAgreementFilterScoreStrategy>().As<IDistributionAgreementFilterScoreStrategy>().InstancePerDependency();

            builder.RegisterGeneric(typeof(DistributionAgreementRankScore<,>))
                .As(typeof(IDistributionAgreementRankScore<,>)).InstancePerDependency();

            builder.RegisterType<DistributionAgreementFilterScoreStrategy>().As<IDistributionAgreementFilterScoreStrategy>()
                .InstancePerDependency();

            //Builders
            builder.RegisterType<DistributionAgreementFilterBuilder<DistributionAgreementDetail>>()
                .As<IDistributionAgreementFilterBuilder<DistributionAgreementDetail>>().InstancePerDependency();

            builder.RegisterGeneric(typeof(NpoiParser<>)).As(typeof(IParser<>)).InstancePerLifetimeScope();

            //Database
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<EFCoreContext>()
                .UseNpgsql(Configurations.ConnectionString);

            builder.RegisterType<EFCoreContext>().As<DbContext>().WithParameter("options", dbContextOptionsBuilder.Options)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorkEfPgSqL>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            //Logger
            builder.RegisterType<NetCoreLogger>()
                .As<IRoyaltyManagerLogger>().InstancePerLifetimeScope();

            //Repositories
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IReadWriteRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IReadRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IWriteRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Criteria<>)).As(typeof(ICreteria<>)).InstancePerDependency();

            // Mediatr
            builder.RegisterType<MediatrBus>()
                  .As<IMemoryBus>().InstancePerLifetimeScope();
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterAssemblyTypes(Assembly.Load("RF.Application.Core")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Services
            builder.RegisterType<ETLService>().As<IETLService>().PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<ETLContextService>().As<IETLContextService>().PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<StatementService>().As<IStatementService>().PropertiesAutowired()
                .InstancePerDependency();
            builder.RegisterType<RoyaltiesService>().As<IRoyaltiesService>().PropertiesAutowired()
                .InstancePerDependency();
        }
    }
}