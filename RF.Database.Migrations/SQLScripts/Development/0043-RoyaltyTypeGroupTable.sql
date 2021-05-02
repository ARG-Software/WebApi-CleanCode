ALTER TABLE public."RoyaltyType" ADD "RoyaltyTypeGroupId" integer NULL;

CREATE TABLE public."RoyaltyTypeGroup" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    CONSTRAINT "PK_RoyaltyTypeGroup" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_RoyaltyType_RoyaltyTypeGroupId" ON public."RoyaltyType" ("RoyaltyTypeGroupId");

ALTER TABLE public."RoyaltyType" ADD CONSTRAINT "FK_RoyaltyType_RoyaltyTypeGroup_RoyaltyTypeGroupId" FOREIGN KEY ("RoyaltyTypeGroupId") REFERENCES public."RoyaltyTypeGroup" ("Id") ON DELETE RESTRICT;

INSERT INTO public."_RFMigrationHistory" ("MigrationId", "ProductVersion")
VALUES ('20200730004319_RoyaltyTypeGroupTable', '2.2.6-servicing-10079');

