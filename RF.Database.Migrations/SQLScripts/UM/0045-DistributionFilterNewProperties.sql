ALTER TABLE public."DistributionAgreementDetail" DROP CONSTRAINT "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~";

ALTER TABLE public."DistributionAgreementDetail" DROP CONSTRAINT "FK_DistributionAgreementDetail_Source_SourceId";

DROP INDEX public."IX_DistributionAgreementDetail_RoyaltyTypeGroupId";

DROP INDEX public."IX_DistributionAgreementDetail_SourceId";

ALTER TABLE public."DistributionAgreementDetail" DROP COLUMN "RoyaltyTypeGroupId";

ALTER TABLE public."DistributionAgreementDetail" DROP COLUMN "SourceId";

ALTER TABLE public."StatementDetail" ALTER COLUMN "TerritoryId" TYPE integer;
ALTER TABLE public."StatementDetail" ALTER COLUMN "TerritoryId" DROP NOT NULL;
ALTER TABLE public."StatementDetail" ALTER COLUMN "TerritoryId" DROP DEFAULT;

ALTER TABLE public."StatementDetail" ALTER COLUMN "RoyaltyTypeId" TYPE integer;
ALTER TABLE public."StatementDetail" ALTER COLUMN "RoyaltyTypeId" DROP NOT NULL;
ALTER TABLE public."StatementDetail" ALTER COLUMN "RoyaltyTypeId" DROP DEFAULT;

ALTER TABLE public."DistributionAgreementFilter" ADD "PlatformType" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE public."DistributionAgreementFilter" ADD "ProductionType" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE public."DistributionAgreementFilter" ADD "Region" boolean NOT NULL DEFAULT FALSE;

INSERT INTO public."_RFMigrationHistory" ("MigrationId", "ProductVersion")
VALUES ('20200906224556_DistributionFilterNewProperties', '2.2.6-servicing-10079');

