ALTER TABLE public."DistributionAgreementDetail" ADD "PlatformTypeId" integer NULL;

ALTER TABLE public."DistributionAgreementDetail" ADD "ProductionTypeId" integer NULL;

ALTER TABLE public."DistributionAgreementDetail" ADD "RegionId" integer NULL;

ALTER TABLE public."DistributionAgreementDetail" ADD "RoyaltyTypeGroupId" integer NULL;

ALTER TABLE public."DistributionAgreementDetail" ADD "SourceId" integer NULL;

CREATE INDEX "IX_DistributionAgreementDetail_PlatformTypeId" ON public."DistributionAgreementDetail" ("PlatformTypeId");

CREATE INDEX "IX_DistributionAgreementDetail_ProductionTypeId" ON public."DistributionAgreementDetail" ("ProductionTypeId");

CREATE INDEX "IX_DistributionAgreementDetail_RegionId" ON public."DistributionAgreementDetail" ("RegionId");

CREATE INDEX "IX_DistributionAgreementDetail_RoyaltyTypeGroupId" ON public."DistributionAgreementDetail" ("RoyaltyTypeGroupId");

CREATE INDEX "IX_DistributionAgreementDetail_SourceId" ON public."DistributionAgreementDetail" ("SourceId");

ALTER TABLE public."DistributionAgreementDetail" ADD CONSTRAINT "FK_DistributionAgreementDetail_PlatformType_PlatformTypeId" FOREIGN KEY ("PlatformTypeId") REFERENCES public."PlatformType" ("Id") ON DELETE RESTRICT;

ALTER TABLE public."DistributionAgreementDetail" ADD CONSTRAINT "FK_DistributionAgreementDetail_ProductionType_ProductionTypeId" FOREIGN KEY ("ProductionTypeId") REFERENCES public."ProductionType" ("Id") ON DELETE RESTRICT;

ALTER TABLE public."DistributionAgreementDetail" ADD CONSTRAINT "FK_DistributionAgreementDetail_Region_RegionId" FOREIGN KEY ("RegionId") REFERENCES public."Region" ("Id") ON DELETE RESTRICT;

ALTER TABLE public."DistributionAgreementDetail" ADD CONSTRAINT "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~" FOREIGN KEY ("RoyaltyTypeGroupId") REFERENCES public."RoyaltyTypeGroup" ("Id") ON DELETE RESTRICT;

ALTER TABLE public."DistributionAgreementDetail" ADD CONSTRAINT "FK_DistributionAgreementDetail_Source_SourceId" FOREIGN KEY ("SourceId") REFERENCES public."Source" ("Id") ON DELETE RESTRICT;

INSERT INTO public."_RFMigrationHistory" ("MigrationId", "ProductVersion")
VALUES ('20201105210708_updateDistributionAgreementDetailFields', '2.2.6-servicing-10079');

