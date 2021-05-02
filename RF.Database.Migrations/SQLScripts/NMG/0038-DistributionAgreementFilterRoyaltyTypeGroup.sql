ALTER TABLE public."RoyaltyTypeGroup" ADD "Name" text NULL;

ALTER TABLE public."DistributionAgreementFilter" ADD "RoyaltyTypeGroup" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE public."DistributionAgreementDetail" ADD "RoyaltyTypeGroupId" integer NULL;

CREATE INDEX "IX_DistributionAgreementDetail_RoyaltyTypeGroupId" ON public."DistributionAgreementDetail" ("RoyaltyTypeGroupId");

ALTER TABLE public."DistributionAgreementDetail" ADD CONSTRAINT "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~" FOREIGN KEY ("RoyaltyTypeGroupId") REFERENCES public."RoyaltyTypeGroup" ("Id") ON DELETE RESTRICT;

INSERT INTO public."_RFMigrationHistory" ("MigrationId", "ProductVersion")
VALUES ('20200801024631_DistributionAgreementFilterRoyaltyTypeGroup', '2.2.6-servicing-10079');

