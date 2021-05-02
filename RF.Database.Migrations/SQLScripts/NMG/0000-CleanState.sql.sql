CREATE TABLE IF NOT EXISTS public."_RFMigrationHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK__RFMigrationHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE public."Company" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    CONSTRAINT "PK_Company" PRIMARY KEY ("Id")
);

CREATE TABLE public."Currency" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "Code" text NULL,
    CONSTRAINT "PK_Currency" PRIMARY KEY ("Id")
);

CREATE TABLE public."DistributionAgreementFilter" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "RoyaltyType" boolean NOT NULL,
    "Territory" boolean NOT NULL,
    "Publisher" boolean NOT NULL,
    "Episode" boolean NOT NULL,
    "PlatformTier" boolean NOT NULL,
    "ProductionTitle" boolean NOT NULL,
    "Label" boolean NOT NULL,
    "Album" boolean NOT NULL,
    "Society" boolean NOT NULL,
    "Source" boolean NOT NULL,
    CONSTRAINT "PK_DistributionAgreementFilter" PRIMARY KEY ("Id")
);

CREATE TABLE public."Label" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "Email" text NULL,
    "Address" text NULL,
    "PayeeName" text NULL,
    "TaxId" text NULL,
    "Isni" text NULL,
    "Controlled" boolean NOT NULL,
    CONSTRAINT "PK_Label" PRIMARY KEY ("Id")
);

CREATE TABLE public."MechanicalLicense" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Key" text NULL,
    CONSTRAINT "PK_MechanicalLicense" PRIMARY KEY ("Id")
);

CREATE TABLE public."Payee" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "Email" text NULL,
    "SwiftCode" text NULL,
    "IBAN" text NULL,
    "RoutingNumber" text NULL,
    "AccountNumber" text NULL,
    "Address1" text NULL,
    "Address2" text NULL,
    "City" text NULL,
    "State" text NULL,
    "PostalCode" text NULL,
    "MailingCountry" text NULL,
    "PaypalAddress" text NULL,
    "TaxId" text NULL,
    CONSTRAINT "PK_Payee" PRIMARY KEY ("Id")
);

CREATE TABLE public."PlatformType" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    CONSTRAINT "PK_PlatformType" PRIMARY KEY ("Id")
);

CREATE TABLE public."ProductionType" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Title" text NULL,
    CONSTRAINT "PK_ProductionType" PRIMARY KEY ("Id")
);

CREATE TABLE public."PublisherType" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    CONSTRAINT "PK_PublisherType" PRIMARY KEY ("Id")
);

CREATE TABLE public."Region" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    CONSTRAINT "PK_Region" PRIMARY KEY ("Id")
);

CREATE TABLE public."RoyaltyType" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Title" text NULL,
    CONSTRAINT "PK_RoyaltyType" PRIMARY KEY ("Id")
);

CREATE TABLE public."Source" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_Source" PRIMARY KEY ("Id")
);

CREATE TABLE public."Writer" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "WriterLN" text NULL,
    "WriterFN" text NULL,
    "WriterPRAff" text NULL,
    "WriterMRAff" text NULL,
    "WriterIPINameNumber" integer NOT NULL,
    "WriterIPIBaseNumber" integer NULL,
    "Controlled" boolean NOT NULL,
    CONSTRAINT "PK_Writer" PRIMARY KEY ("Id")
);

CREATE TABLE public."WriterRoleCode" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Role" text NULL,
    "Code" text NULL,
    CONSTRAINT "PK_WriterRoleCode" PRIMARY KEY ("Id")
);

CREATE TABLE public."CWRFile" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "File" bytea NULL,
    "CompanyId" integer NOT NULL,
    CONSTRAINT "PK_CWRFile" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CWRFile_Company_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES public."Company" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Song" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "CompanyId" integer NOT NULL,
    "SongTitle" text NULL,
    "FirstReleaseCatalogNumber" integer NULL,
    "IntendedPurpose" text NULL,
    "ProductionTitle" text NULL,
    "Library" text NULL,
    "CdIdentifier" text NULL,
    "WorkTitleCDCut" text NULL,
    "PublicDomain" boolean NULL,
    "PDTitle" text NULL,
    CONSTRAINT "PK_Song" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Song_Company_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES public."Company" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."DistributionAgreement" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "DistributionAgreementFilterId" integer NOT NULL,
    "CompanyId" integer NOT NULL,
    CONSTRAINT "PK_DistributionAgreement" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DistributionAgreement_Company_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES public."Company" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreement_DistributionAgreementFilter_Distribut~" FOREIGN KEY ("DistributionAgreementFilterId") REFERENCES public."DistributionAgreementFilter" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Artist" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "LabelId" integer NULL,
    "Email" text NULL,
    "Address" text NULL,
    "PayeeName" text NULL,
    "TaxId" text NULL,
    "Isni" text NULL,
    "IPI" text NULL,
    CONSTRAINT "PK_Artist" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Artist_Label_LabelId" FOREIGN KEY ("LabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."EAN" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "LabelId" integer NOT NULL,
    "DistributingLabelId" integer NOT NULL,
    "Name" text NULL,
    "Configuration" text NULL,
    "Default" boolean NOT NULL,
    CONSTRAINT "PK_EAN" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_EAN_Label_DistributingLabelId" FOREIGN KEY ("DistributingLabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_EAN_Label_LabelId" FOREIGN KEY ("LabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."LabelAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "LabelId" integer NOT NULL,
    CONSTRAINT "PK_LabelAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_LabelAlias_Label_LabelId" FOREIGN KEY ("LabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."UPC" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "AlbumId" integer NOT NULL,
    "LabelId" integer NOT NULL,
    "DistributingLabelId" integer NOT NULL,
    "Name" text NULL,
    "Configuration" text NULL,
    "Default" boolean NOT NULL,
    CONSTRAINT "PK_UPC" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UPC_Label_DistributingLabelId" FOREIGN KEY ("DistributingLabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_UPC_Label_LabelId" FOREIGN KEY ("LabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Recipient" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "PayeeId" integer NOT NULL,
    CONSTRAINT "PK_Recipient" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Recipient_Payee_PayeeId" FOREIGN KEY ("PayeeId") REFERENCES public."Payee" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Platform" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "PlatformTypeId" integer NOT NULL,
    CONSTRAINT "PK_Platform" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Platform_PlatformType_PlatformTypeId" FOREIGN KEY ("PlatformTypeId") REFERENCES public."PlatformType" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."ProductionTitle" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Title" text NULL,
    "ProductionTypeId" integer NOT NULL,
    CONSTRAINT "PK_ProductionTitle" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ProductionTitle_ProductionType_ProductionTypeId" FOREIGN KEY ("ProductionTypeId") REFERENCES public."ProductionType" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Territory" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "RegionId" integer NOT NULL,
    CONSTRAINT "PK_Territory" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Territory_Region_RegionId" FOREIGN KEY ("RegionId") REFERENCES public."Region" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."PaymentReceived" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "StartDate" timestamp without time zone NOT NULL,
    "EndDate" timestamp without time zone NOT NULL,
    "PaymentDate" timestamp without time zone NOT NULL,
    "ExchangeRate" double precision NOT NULL,
    "GrossAmountLocal" double precision NOT NULL,
    "NetAmountLocal" double precision NOT NULL,
    "GrossAmountForeign" double precision NOT NULL,
    "NetAmountForeign" double precision NOT NULL,
    "CurrencyId" integer NOT NULL,
    "Reconciled" boolean NOT NULL,
    "SourceId" integer NOT NULL,
    CONSTRAINT "PK_PaymentReceived" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PaymentReceived_Currency_CurrencyId" FOREIGN KEY ("CurrencyId") REFERENCES public."Currency" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_PaymentReceived_Source_SourceId" FOREIGN KEY ("SourceId") REFERENCES public."Source" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."RoyaltyTypeAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "RoyaltyTypeId" integer NOT NULL,
    "SourceId" integer NOT NULL,
    CONSTRAINT "PK_RoyaltyTypeAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_RoyaltyTypeAlias_RoyaltyType_RoyaltyTypeId" FOREIGN KEY ("RoyaltyTypeId") REFERENCES public."RoyaltyType" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_RoyaltyTypeAlias_Source_SourceId" FOREIGN KEY ("SourceId") REFERENCES public."Source" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Template" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NOT NULL,
    "Definition" text NULL,
    "SourceId" integer NULL,
    CONSTRAINT "PK_Template" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Template_Source_SourceId" FOREIGN KEY ("SourceId") REFERENCES public."Source" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."ISWC" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "SongId" integer NOT NULL,
    "Code" text NULL,
    "Default" boolean NOT NULL,
    CONSTRAINT "PK_ISWC" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ISWC_Song_SongId" FOREIGN KEY ("SongId") REFERENCES public."Song" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."SongAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "SongId" integer NOT NULL,
    CONSTRAINT "PK_SongAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SongAlias_Song_SongId" FOREIGN KEY ("SongId") REFERENCES public."Song" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."SourceSongCode" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Code" text NULL,
    "SongId" integer NOT NULL,
    "SourceId" integer NOT NULL,
    CONSTRAINT "PK_SourceSongCode" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SourceSongCode_Song_SongId" FOREIGN KEY ("SongId") REFERENCES public."Song" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_SourceSongCode_Source_SourceId" FOREIGN KEY ("SourceId") REFERENCES public."Source" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."WriterShare" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "SongId" integer NOT NULL,
    "WriterId" integer NOT NULL,
    "WriterRoleCodeId" integer NOT NULL,
    "DistributionAgreementId" integer NOT NULL,
    "Share" double precision NOT NULL,
    CONSTRAINT "PK_WriterShare" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_WriterShare_DistributionAgreement_DistributionAgreementId" FOREIGN KEY ("DistributionAgreementId") REFERENCES public."DistributionAgreement" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_WriterShare_Song_SongId" FOREIGN KEY ("SongId") REFERENCES public."Song" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_WriterShare_Writer_WriterId" FOREIGN KEY ("WriterId") REFERENCES public."Writer" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_WriterShare_WriterRoleCode_WriterRoleCodeId" FOREIGN KEY ("WriterRoleCodeId") REFERENCES public."WriterRoleCode" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."ISRC" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "SongId" integer NOT NULL,
    "Code" text NULL,
    "Default" boolean NOT NULL,
    "RecordingName" text NULL,
    "RecordingDuration" interval NULL,
    "FirstReleaseDate" timestamp without time zone NULL,
    "ArtistId" integer NULL,
    CONSTRAINT "PK_ISRC" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ISRC_Artist_ArtistId" FOREIGN KEY ("ArtistId") REFERENCES public."Artist" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_ISRC_Song_SongId" FOREIGN KEY ("SongId") REFERENCES public."Song" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."PlatformTier" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "PlatformId" integer NOT NULL,
    CONSTRAINT "PK_PlatformTier" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PlatformTier_Platform_PlatformId" FOREIGN KEY ("PlatformId") REFERENCES public."Platform" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Episode" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "Number" text NULL,
    "ProductionTitleId" integer NULL,
    CONSTRAINT "PK_Episode" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Episode_ProductionTitle_ProductionTitleId" FOREIGN KEY ("ProductionTitleId") REFERENCES public."ProductionTitle" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."ProductionTitleAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "ProductionTitleId" integer NOT NULL,
    CONSTRAINT "PK_ProductionTitleAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ProductionTitleAlias_ProductionTitle_ProductionTitleId" FOREIGN KEY ("ProductionTitleId") REFERENCES public."ProductionTitle" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Society" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "Acronym" text NULL,
    "Email" text NULL,
    "Website" text NULL,
    "Affiliates" integer NOT NULL,
    "TerritoryId" integer NOT NULL,
    CONSTRAINT "PK_Society" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Society_Territory_TerritoryId" FOREIGN KEY ("TerritoryId") REFERENCES public."Territory" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."TerritoryAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "TerritoryId" integer NOT NULL,
    CONSTRAINT "PK_TerritoryAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TerritoryAlias_Territory_TerritoryId" FOREIGN KEY ("TerritoryId") REFERENCES public."Territory" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."StatementHeader" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "TotalLocal" double precision NOT NULL,
    "TotalForeign" double precision NOT NULL,
    "Date" timestamp without time zone NOT NULL,
    "Comments" text NULL,
    "TemplateId" integer NOT NULL,
    "PaymentReceivedId" integer NULL,
    CONSTRAINT "PK_StatementHeader" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_StatementHeader_PaymentReceived_PaymentReceivedId" FOREIGN KEY ("PaymentReceivedId") REFERENCES public."PaymentReceived" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementHeader_Template_TemplateId" FOREIGN KEY ("TemplateId") REFERENCES public."Template" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."PlatformTierAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "PlatformTierId" integer NOT NULL,
    CONSTRAINT "PK_PlatformTierAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PlatformTierAlias_PlatformTier_PlatformTierId" FOREIGN KEY ("PlatformTierId") REFERENCES public."PlatformTier" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."EpisodeAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "EpisodeId" integer NOT NULL,
    CONSTRAINT "PK_EpisodeAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_EpisodeAlias_Episode_EpisodeId" FOREIGN KEY ("EpisodeId") REFERENCES public."Episode" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."Publisher" (
    "Id" integer NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "MailAddress" text NULL,
    "Email" text NULL,
    "ContactName" text NULL,
    "PhoneNumber" text NULL,
    "TaxId" text NULL,
    "IPINameNumber" text NULL,
    "IPIBaseNumber" text NULL,
    "PublisherTypeId" integer NOT NULL,
    "Controlled" boolean NOT NULL,
    "PRAffiliationId" integer NOT NULL,
    "MRAffiliationId" integer NOT NULL,
    "SRAffiliationId" integer NOT NULL,
    CONSTRAINT "PK_Publisher" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Publisher_Society_MRAffiliationId" FOREIGN KEY ("MRAffiliationId") REFERENCES public."Society" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Publisher_Society_PRAffiliationId" FOREIGN KEY ("PRAffiliationId") REFERENCES public."Society" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Publisher_PublisherType_PublisherTypeId" FOREIGN KEY ("PublisherTypeId") REFERENCES public."PublisherType" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Publisher_Society_SRAffiliationId" FOREIGN KEY ("SRAffiliationId") REFERENCES public."Society" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."SocietyAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "SocietyId" integer NOT NULL,
    CONSTRAINT "PK_SocietyAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SocietyAlias_Society_SocietyId" FOREIGN KEY ("SocietyId") REFERENCES public."Society" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."DistributionAgreementDetail" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "IswcId" integer NULL,
    "IsrcId" integer NULL,
    "RoyaltyTypeId" integer NULL,
    "TerritoryId" integer NULL,
    "PublisherId" integer NULL,
    "RecipientId" integer NOT NULL,
    "EpisodeId" integer NULL,
    "PlatformTierId" integer NULL,
    "ProductionTitleId" integer NULL,
    "LabelId" integer NULL,
    "SocietyId" integer NULL,
    "SourceId" integer NULL,
    "DistributionAgreementId" integer NULL,
    "Share" double precision NOT NULL,
    "Rate" double precision NOT NULL,
    "AgreementGroup" integer NOT NULL,
    CONSTRAINT "PK_DistributionAgreementDetail" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DistributionAgreementDetail_DistributionAgreement_Distribut~" FOREIGN KEY ("DistributionAgreementId") REFERENCES public."DistributionAgreement" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Episode_EpisodeId" FOREIGN KEY ("EpisodeId") REFERENCES public."Episode" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_ISRC_IsrcId" FOREIGN KEY ("IsrcId") REFERENCES public."ISRC" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_ISWC_IswcId" FOREIGN KEY ("IswcId") REFERENCES public."ISWC" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Label_LabelId" FOREIGN KEY ("LabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_PlatformTier_PlatformTierId" FOREIGN KEY ("PlatformTierId") REFERENCES public."PlatformTier" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_ProductionTitle_ProductionTitle~" FOREIGN KEY ("ProductionTitleId") REFERENCES public."ProductionTitle" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Publisher_PublisherId" FOREIGN KEY ("PublisherId") REFERENCES public."Publisher" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Recipient_RecipientId" FOREIGN KEY ("RecipientId") REFERENCES public."Recipient" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_RoyaltyType_RoyaltyTypeId" FOREIGN KEY ("RoyaltyTypeId") REFERENCES public."RoyaltyType" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Society_SocietyId" FOREIGN KEY ("SocietyId") REFERENCES public."Society" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Source_SourceId" FOREIGN KEY ("SourceId") REFERENCES public."Source" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_DistributionAgreementDetail_Territory_TerritoryId" FOREIGN KEY ("TerritoryId") REFERENCES public."Territory" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."PublisherAlias" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Name" text NULL,
    "PublisherId" integer NOT NULL,
    CONSTRAINT "PK_PublisherAlias" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PublisherAlias_Publisher_PublisherId" FOREIGN KEY ("PublisherId") REFERENCES public."Publisher" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."StatementDetail" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "RoyaltyNetUsd" double precision NOT NULL,
    "RoyaltyNetForeign" double precision NOT NULL,
    "UnitRate" double precision NULL,
    "BonusAmount" double precision NULL,
    "StatementHeaderId" integer NOT NULL,
    "RoyaltyTypeId" integer NOT NULL,
    "TerritoryId" integer NOT NULL,
    "PublisherId" integer NULL,
    "EpisodeId" integer NULL,
    "PlatformTierId" integer NULL,
    "ProductionTitleId" integer NULL,
    "LabelId" integer NULL,
    "AlbumId" integer NULL,
    "SocietyId" integer NULL,
    "ISRCId" integer NULL,
    "ISWCId" integer NULL,
    "SongId" integer NOT NULL,
    "PerformanceDate" timestamp without time zone NULL,
    "Quantity" integer NOT NULL,
    CONSTRAINT "PK_StatementDetail" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_StatementDetail_Episode_EpisodeId" FOREIGN KEY ("EpisodeId") REFERENCES public."Episode" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_ISRC_ISRCId" FOREIGN KEY ("ISRCId") REFERENCES public."ISRC" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_ISWC_ISWCId" FOREIGN KEY ("ISWCId") REFERENCES public."ISWC" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_Label_LabelId" FOREIGN KEY ("LabelId") REFERENCES public."Label" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_PlatformTier_PlatformTierId" FOREIGN KEY ("PlatformTierId") REFERENCES public."PlatformTier" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_ProductionTitle_ProductionTitleId" FOREIGN KEY ("ProductionTitleId") REFERENCES public."ProductionTitle" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_Publisher_PublisherId" FOREIGN KEY ("PublisherId") REFERENCES public."Publisher" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_RoyaltyType_RoyaltyTypeId" FOREIGN KEY ("RoyaltyTypeId") REFERENCES public."RoyaltyType" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_Society_SocietyId" FOREIGN KEY ("SocietyId") REFERENCES public."Society" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_Song_SongId" FOREIGN KEY ("SongId") REFERENCES public."Song" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_StatementHeader_StatementHeaderId" FOREIGN KEY ("StatementHeaderId") REFERENCES public."StatementHeader" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_StatementDetail_Territory_TerritoryId" FOREIGN KEY ("TerritoryId") REFERENCES public."Territory" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."RoyaltyDistribution" (
    "Id" serial NOT NULL,
    "CreatedOn" timestamp without time zone NOT NULL,
    "ModifiedOn" timestamp without time zone NULL,
    "CreatedByUserLogin" text NULL,
    "ModifiedByUserLogin" text NULL,
    "Description" text NULL,
    "Amount" double precision NOT NULL,
    "RecipientId" integer NOT NULL,
    "StatementDetailId" integer NOT NULL,
    "WriterId" integer NOT NULL,
    "Rate" double precision NOT NULL,
    CONSTRAINT "PK_RoyaltyDistribution" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_RoyaltyDistribution_Recipient_RecipientId" FOREIGN KEY ("RecipientId") REFERENCES public."Recipient" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_RoyaltyDistribution_StatementDetail_StatementDetailId" FOREIGN KEY ("StatementDetailId") REFERENCES public."StatementDetail" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_RoyaltyDistribution_Writer_WriterId" FOREIGN KEY ("WriterId") REFERENCES public."Writer" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Artist_LabelId" ON public."Artist" ("LabelId");

CREATE INDEX "IX_CWRFile_CompanyId" ON public."CWRFile" ("CompanyId");

CREATE INDEX "IX_DistributionAgreement_CompanyId" ON public."DistributionAgreement" ("CompanyId");

CREATE INDEX "IX_DistributionAgreement_DistributionAgreementFilterId" ON public."DistributionAgreement" ("DistributionAgreementFilterId");

CREATE INDEX "IX_DistributionAgreementDetail_DistributionAgreementId" ON public."DistributionAgreementDetail" ("DistributionAgreementId");

CREATE INDEX "IX_DistributionAgreementDetail_EpisodeId" ON public."DistributionAgreementDetail" ("EpisodeId");

CREATE INDEX "IX_DistributionAgreementDetail_IsrcId" ON public."DistributionAgreementDetail" ("IsrcId");

CREATE INDEX "IX_DistributionAgreementDetail_IswcId" ON public."DistributionAgreementDetail" ("IswcId");

CREATE INDEX "IX_DistributionAgreementDetail_LabelId" ON public."DistributionAgreementDetail" ("LabelId");

CREATE INDEX "IX_DistributionAgreementDetail_PlatformTierId" ON public."DistributionAgreementDetail" ("PlatformTierId");

CREATE INDEX "IX_DistributionAgreementDetail_ProductionTitleId" ON public."DistributionAgreementDetail" ("ProductionTitleId");

CREATE INDEX "IX_DistributionAgreementDetail_PublisherId" ON public."DistributionAgreementDetail" ("PublisherId");

CREATE INDEX "IX_DistributionAgreementDetail_RecipientId" ON public."DistributionAgreementDetail" ("RecipientId");

CREATE INDEX "IX_DistributionAgreementDetail_RoyaltyTypeId" ON public."DistributionAgreementDetail" ("RoyaltyTypeId");

CREATE INDEX "IX_DistributionAgreementDetail_SocietyId" ON public."DistributionAgreementDetail" ("SocietyId");

CREATE INDEX "IX_DistributionAgreementDetail_SourceId" ON public."DistributionAgreementDetail" ("SourceId");

CREATE INDEX "IX_DistributionAgreementDetail_TerritoryId" ON public."DistributionAgreementDetail" ("TerritoryId");

CREATE INDEX "IX_EAN_DistributingLabelId" ON public."EAN" ("DistributingLabelId");

CREATE INDEX "IX_EAN_LabelId" ON public."EAN" ("LabelId");

CREATE INDEX "IX_Episode_ProductionTitleId" ON public."Episode" ("ProductionTitleId");

CREATE INDEX "IX_EpisodeAlias_EpisodeId" ON public."EpisodeAlias" ("EpisodeId");

CREATE INDEX "IX_ISRC_ArtistId" ON public."ISRC" ("ArtistId");

CREATE INDEX "IX_ISRC_SongId" ON public."ISRC" ("SongId");

CREATE INDEX "IX_ISWC_SongId" ON public."ISWC" ("SongId");

CREATE INDEX "IX_LabelAlias_LabelId" ON public."LabelAlias" ("LabelId");

CREATE INDEX "IX_PaymentReceived_CurrencyId" ON public."PaymentReceived" ("CurrencyId");

CREATE INDEX "IX_PaymentReceived_SourceId" ON public."PaymentReceived" ("SourceId");

CREATE INDEX "IX_Platform_PlatformTypeId" ON public."Platform" ("PlatformTypeId");

CREATE INDEX "IX_PlatformTier_PlatformId" ON public."PlatformTier" ("PlatformId");

CREATE INDEX "IX_PlatformTierAlias_PlatformTierId" ON public."PlatformTierAlias" ("PlatformTierId");

CREATE INDEX "IX_ProductionTitle_ProductionTypeId" ON public."ProductionTitle" ("ProductionTypeId");

CREATE INDEX "IX_ProductionTitleAlias_ProductionTitleId" ON public."ProductionTitleAlias" ("ProductionTitleId");

CREATE INDEX "IX_Publisher_MRAffiliationId" ON public."Publisher" ("MRAffiliationId");

CREATE INDEX "IX_Publisher_PRAffiliationId" ON public."Publisher" ("PRAffiliationId");

CREATE INDEX "IX_Publisher_PublisherTypeId" ON public."Publisher" ("PublisherTypeId");

CREATE INDEX "IX_Publisher_SRAffiliationId" ON public."Publisher" ("SRAffiliationId");

CREATE INDEX "IX_PublisherAlias_PublisherId" ON public."PublisherAlias" ("PublisherId");

CREATE INDEX "IX_Recipient_PayeeId" ON public."Recipient" ("PayeeId");

CREATE INDEX "IX_RoyaltyDistribution_RecipientId" ON public."RoyaltyDistribution" ("RecipientId");

CREATE INDEX "IX_RoyaltyDistribution_StatementDetailId" ON public."RoyaltyDistribution" ("StatementDetailId");

CREATE INDEX "IX_RoyaltyDistribution_WriterId" ON public."RoyaltyDistribution" ("WriterId");

CREATE INDEX "IX_RoyaltyTypeAlias_RoyaltyTypeId" ON public."RoyaltyTypeAlias" ("RoyaltyTypeId");

CREATE INDEX "IX_RoyaltyTypeAlias_SourceId" ON public."RoyaltyTypeAlias" ("SourceId");

CREATE INDEX "IX_Society_TerritoryId" ON public."Society" ("TerritoryId");

CREATE INDEX "IX_SocietyAlias_SocietyId" ON public."SocietyAlias" ("SocietyId");

CREATE INDEX "IX_Song_CompanyId" ON public."Song" ("CompanyId");

CREATE INDEX "IX_SongAlias_SongId" ON public."SongAlias" ("SongId");

CREATE INDEX "IX_SourceSongCode_SongId" ON public."SourceSongCode" ("SongId");

CREATE INDEX "IX_SourceSongCode_SourceId" ON public."SourceSongCode" ("SourceId");

CREATE INDEX "IX_StatementDetail_EpisodeId" ON public."StatementDetail" ("EpisodeId");

CREATE INDEX "IX_StatementDetail_ISRCId" ON public."StatementDetail" ("ISRCId");

CREATE INDEX "IX_StatementDetail_ISWCId" ON public."StatementDetail" ("ISWCId");

CREATE INDEX "IX_StatementDetail_LabelId" ON public."StatementDetail" ("LabelId");

CREATE INDEX "IX_StatementDetail_PlatformTierId" ON public."StatementDetail" ("PlatformTierId");

CREATE INDEX "IX_StatementDetail_ProductionTitleId" ON public."StatementDetail" ("ProductionTitleId");

CREATE INDEX "IX_StatementDetail_PublisherId" ON public."StatementDetail" ("PublisherId");

CREATE INDEX "IX_StatementDetail_RoyaltyTypeId" ON public."StatementDetail" ("RoyaltyTypeId");

CREATE INDEX "IX_StatementDetail_SocietyId" ON public."StatementDetail" ("SocietyId");

CREATE INDEX "IX_StatementDetail_SongId" ON public."StatementDetail" ("SongId");

CREATE INDEX "IX_StatementDetail_StatementHeaderId" ON public."StatementDetail" ("StatementHeaderId");

CREATE INDEX "IX_StatementDetail_TerritoryId" ON public."StatementDetail" ("TerritoryId");

CREATE INDEX "IX_StatementHeader_PaymentReceivedId" ON public."StatementHeader" ("PaymentReceivedId");

CREATE INDEX "IX_StatementHeader_TemplateId" ON public."StatementHeader" ("TemplateId");

CREATE INDEX "IX_Template_SourceId" ON public."Template" ("SourceId");

CREATE INDEX "IX_Territory_RegionId" ON public."Territory" ("RegionId");

CREATE INDEX "IX_TerritoryAlias_TerritoryId" ON public."TerritoryAlias" ("TerritoryId");

CREATE INDEX "IX_UPC_DistributingLabelId" ON public."UPC" ("DistributingLabelId");

CREATE INDEX "IX_UPC_LabelId" ON public."UPC" ("LabelId");

CREATE INDEX "IX_WriterShare_DistributionAgreementId" ON public."WriterShare" ("DistributionAgreementId");

CREATE INDEX "IX_WriterShare_SongId" ON public."WriterShare" ("SongId");

CREATE INDEX "IX_WriterShare_WriterId" ON public."WriterShare" ("WriterId");

CREATE INDEX "IX_WriterShare_WriterRoleCodeId" ON public."WriterShare" ("WriterRoleCodeId");

INSERT INTO public."_RFMigrationHistory" ("MigrationId", "ProductVersion")
VALUES ('20200306233427_CleanState', '3.1.2');

