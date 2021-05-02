using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RF.Database.Migrations.Migrations.Output
{
    public partial class CleanState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributionAgreementFilter",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RoyaltyType = table.Column<bool>(nullable: false),
                    Territory = table.Column<bool>(nullable: false),
                    Publisher = table.Column<bool>(nullable: false),
                    Episode = table.Column<bool>(nullable: false),
                    PlatformTier = table.Column<bool>(nullable: false),
                    ProductionTitle = table.Column<bool>(nullable: false),
                    Label = table.Column<bool>(nullable: false),
                    Album = table.Column<bool>(nullable: false),
                    Society = table.Column<bool>(nullable: false),
                    Source = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionAgreementFilter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PayeeName = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(nullable: true),
                    Isni = table.Column<string>(nullable: true),
                    Controlled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MechanicalLicense",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicalLicense", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payee",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SwiftCode = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    RoutingNumber = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    MailingCountry = table.Column<string>(nullable: true),
                    PaypalAddress = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformType",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionType",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublisherType",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoyaltyType",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoyaltyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Writer",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    WriterLN = table.Column<string>(nullable: true),
                    WriterFN = table.Column<string>(nullable: true),
                    WriterPRAff = table.Column<string>(nullable: true),
                    WriterMRAff = table.Column<string>(nullable: true),
                    WriterIPINameNumber = table.Column<int>(nullable: false),
                    WriterIPIBaseNumber = table.Column<int>(nullable: true),
                    Controlled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WriterRoleCode",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterRoleCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CWRFile",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CWRFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CWRFile_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "public",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    SongTitle = table.Column<string>(nullable: true),
                    FirstReleaseCatalogNumber = table.Column<int>(nullable: true),
                    IntendedPurpose = table.Column<string>(nullable: true),
                    ProductionTitle = table.Column<string>(nullable: true),
                    Library = table.Column<string>(nullable: true),
                    CdIdentifier = table.Column<string>(nullable: true),
                    WorkTitleCDCut = table.Column<string>(nullable: true),
                    PublicDomain = table.Column<bool>(nullable: true),
                    PDTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Song_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "public",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributionAgreement",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DistributionAgreementFilterId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionAgreement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionAgreement_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "public",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreement_DistributionAgreementFilter_Distribut~",
                        column: x => x.DistributionAgreementFilterId,
                        principalSchema: "public",
                        principalTable: "DistributionAgreementFilter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LabelId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PayeeName = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(nullable: true),
                    Isni = table.Column<string>(nullable: true),
                    IPI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artist_Label_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EAN",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LabelId = table.Column<int>(nullable: false),
                    DistributingLabelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAN_Label_DistributingLabelId",
                        column: x => x.DistributingLabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EAN_Label_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabelAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LabelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabelAlias_Label_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UPC",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AlbumId = table.Column<int>(nullable: false),
                    LabelId = table.Column<int>(nullable: false),
                    DistributingLabelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UPC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UPC_Label_DistributingLabelId",
                        column: x => x.DistributingLabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UPC_Label_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PayeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipient_Payee_PayeeId",
                        column: x => x.PayeeId,
                        principalSchema: "public",
                        principalTable: "Payee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlatformTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Platform_PlatformType_PlatformTypeId",
                        column: x => x.PlatformTypeId,
                        principalSchema: "public",
                        principalTable: "PlatformType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionTitle",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ProductionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionTitle_ProductionType_ProductionTypeId",
                        column: x => x.ProductionTypeId,
                        principalSchema: "public",
                        principalTable: "ProductionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Territory",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Territory_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "public",
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceived",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    ExchangeRate = table.Column<double>(nullable: false),
                    GrossAmountLocal = table.Column<double>(nullable: false),
                    NetAmountLocal = table.Column<double>(nullable: false),
                    GrossAmountForeign = table.Column<double>(nullable: false),
                    NetAmountForeign = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Reconciled = table.Column<bool>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceived", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentReceived_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "public",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentReceived_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoyaltyTypeAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RoyaltyTypeId = table.Column<int>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoyaltyTypeAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoyaltyTypeAlias_RoyaltyType_RoyaltyTypeId",
                        column: x => x.RoyaltyTypeId,
                        principalSchema: "public",
                        principalTable: "RoyaltyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoyaltyTypeAlias_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Definition = table.Column<string>(nullable: true),
                    SourceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ISWC",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISWC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ISWC_Song_SongId",
                        column: x => x.SongId,
                        principalSchema: "public",
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SongAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongAlias_Song_SongId",
                        column: x => x.SongId,
                        principalSchema: "public",
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SourceSongCode",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceSongCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceSongCode_Song_SongId",
                        column: x => x.SongId,
                        principalSchema: "public",
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SourceSongCode_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WriterShare",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: false),
                    WriterId = table.Column<int>(nullable: false),
                    WriterRoleCodeId = table.Column<int>(nullable: false),
                    DistributionAgreementId = table.Column<int>(nullable: false),
                    Share = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WriterShare_DistributionAgreement_DistributionAgreementId",
                        column: x => x.DistributionAgreementId,
                        principalSchema: "public",
                        principalTable: "DistributionAgreement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WriterShare_Song_SongId",
                        column: x => x.SongId,
                        principalSchema: "public",
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WriterShare_Writer_WriterId",
                        column: x => x.WriterId,
                        principalSchema: "public",
                        principalTable: "Writer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WriterShare_WriterRoleCode_WriterRoleCodeId",
                        column: x => x.WriterRoleCodeId,
                        principalSchema: "public",
                        principalTable: "WriterRoleCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ISRC",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false),
                    RecordingName = table.Column<string>(nullable: true),
                    RecordingDuration = table.Column<TimeSpan>(nullable: true),
                    FirstReleaseDate = table.Column<DateTime>(nullable: true),
                    ArtistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISRC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ISRC_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalSchema: "public",
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ISRC_Song_SongId",
                        column: x => x.SongId,
                        principalSchema: "public",
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlatformTier",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlatformId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformTier_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalSchema: "public",
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    ProductionTitleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episode_ProductionTitle_ProductionTitleId",
                        column: x => x.ProductionTitleId,
                        principalSchema: "public",
                        principalTable: "ProductionTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionTitleAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProductionTitleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionTitleAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionTitleAlias_ProductionTitle_ProductionTitleId",
                        column: x => x.ProductionTitleId,
                        principalSchema: "public",
                        principalTable: "ProductionTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Society",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Acronym = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Affiliates = table.Column<int>(nullable: false),
                    TerritoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Society", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Society_Territory_TerritoryId",
                        column: x => x.TerritoryId,
                        principalSchema: "public",
                        principalTable: "Territory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TerritoryAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TerritoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerritoryAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerritoryAlias_Territory_TerritoryId",
                        column: x => x.TerritoryId,
                        principalSchema: "public",
                        principalTable: "Territory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatementHeader",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TotalLocal = table.Column<double>(nullable: false),
                    TotalForeign = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: false),
                    PaymentReceivedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatementHeader_PaymentReceived_PaymentReceivedId",
                        column: x => x.PaymentReceivedId,
                        principalSchema: "public",
                        principalTable: "PaymentReceived",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementHeader_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "public",
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlatformTierAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlatformTierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTierAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformTierAlias_PlatformTier_PlatformTierId",
                        column: x => x.PlatformTierId,
                        principalSchema: "public",
                        principalTable: "PlatformTier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    EpisodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpisodeAlias_Episode_EpisodeId",
                        column: x => x.EpisodeId,
                        principalSchema: "public",
                        principalTable: "Episode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(nullable: true),
                    IPINameNumber = table.Column<string>(nullable: true),
                    IPIBaseNumber = table.Column<string>(nullable: true),
                    PublisherTypeId = table.Column<int>(nullable: false),
                    Controlled = table.Column<bool>(nullable: false),
                    PRAffiliationId = table.Column<int>(nullable: false),
                    MRAffiliationId = table.Column<int>(nullable: false),
                    SRAffiliationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publisher_Society_MRAffiliationId",
                        column: x => x.MRAffiliationId,
                        principalSchema: "public",
                        principalTable: "Society",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publisher_Society_PRAffiliationId",
                        column: x => x.PRAffiliationId,
                        principalSchema: "public",
                        principalTable: "Society",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publisher_PublisherType_PublisherTypeId",
                        column: x => x.PublisherTypeId,
                        principalSchema: "public",
                        principalTable: "PublisherType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publisher_Society_SRAffiliationId",
                        column: x => x.SRAffiliationId,
                        principalSchema: "public",
                        principalTable: "Society",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocietyAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SocietyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocietyAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocietyAlias_Society_SocietyId",
                        column: x => x.SocietyId,
                        principalSchema: "public",
                        principalTable: "Society",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributionAgreementDetail",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IswcId = table.Column<int>(nullable: true),
                    IsrcId = table.Column<int>(nullable: true),
                    RoyaltyTypeId = table.Column<int>(nullable: true),
                    TerritoryId = table.Column<int>(nullable: true),
                    PublisherId = table.Column<int>(nullable: true),
                    RecipientId = table.Column<int>(nullable: false),
                    EpisodeId = table.Column<int>(nullable: true),
                    PlatformTierId = table.Column<int>(nullable: true),
                    ProductionTitleId = table.Column<int>(nullable: true),
                    LabelId = table.Column<int>(nullable: true),
                    SocietyId = table.Column<int>(nullable: true),
                    SourceId = table.Column<int>(nullable: true),
                    DistributionAgreementId = table.Column<int>(nullable: true),
                    Share = table.Column<double>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    AgreementGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionAgreementDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_DistributionAgreement_Distribut~",
                        column: x => x.DistributionAgreementId,
                        principalSchema: "public",
                        principalTable: "DistributionAgreement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Episode_EpisodeId",
                        column: x => x.EpisodeId,
                        principalSchema: "public",
                        principalTable: "Episode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_ISRC_IsrcId",
                        column: x => x.IsrcId,
                        principalSchema: "public",
                        principalTable: "ISRC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_ISWC_IswcId",
                        column: x => x.IswcId,
                        principalSchema: "public",
                        principalTable: "ISWC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Label_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_PlatformTier_PlatformTierId",
                        column: x => x.PlatformTierId,
                        principalSchema: "public",
                        principalTable: "PlatformTier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_ProductionTitle_ProductionTitle~",
                        column: x => x.ProductionTitleId,
                        principalSchema: "public",
                        principalTable: "ProductionTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "public",
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalSchema: "public",
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_RoyaltyType_RoyaltyTypeId",
                        column: x => x.RoyaltyTypeId,
                        principalSchema: "public",
                        principalTable: "RoyaltyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Society_SocietyId",
                        column: x => x.SocietyId,
                        principalSchema: "public",
                        principalTable: "Society",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionAgreementDetail_Territory_TerritoryId",
                        column: x => x.TerritoryId,
                        principalSchema: "public",
                        principalTable: "Territory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublisherAlias",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PublisherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublisherAlias_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "public",
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatementDetail",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RoyaltyNetUsd = table.Column<double>(nullable: false),
                    RoyaltyNetForeign = table.Column<double>(nullable: false),
                    UnitRate = table.Column<double>(nullable: true),
                    BonusAmount = table.Column<double>(nullable: true),
                    StatementHeaderId = table.Column<int>(nullable: false),
                    RoyaltyTypeId = table.Column<int>(nullable: false),
                    TerritoryId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: true),
                    EpisodeId = table.Column<int>(nullable: true),
                    PlatformTierId = table.Column<int>(nullable: true),
                    ProductionTitleId = table.Column<int>(nullable: true),
                    LabelId = table.Column<int>(nullable: true),
                    AlbumId = table.Column<int>(nullable: true),
                    SocietyId = table.Column<int>(nullable: true),
                    ISRCId = table.Column<int>(nullable: true),
                    ISWCId = table.Column<int>(nullable: true),
                    SongId = table.Column<int>(nullable: false),
                    PerformanceDate = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatementDetail_Episode_EpisodeId",
                        column: x => x.EpisodeId,
                        principalSchema: "public",
                        principalTable: "Episode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_ISRC_ISRCId",
                        column: x => x.ISRCId,
                        principalSchema: "public",
                        principalTable: "ISRC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_ISWC_ISWCId",
                        column: x => x.ISWCId,
                        principalSchema: "public",
                        principalTable: "ISWC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_Label_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "public",
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_PlatformTier_PlatformTierId",
                        column: x => x.PlatformTierId,
                        principalSchema: "public",
                        principalTable: "PlatformTier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_ProductionTitle_ProductionTitleId",
                        column: x => x.ProductionTitleId,
                        principalSchema: "public",
                        principalTable: "ProductionTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "public",
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_RoyaltyType_RoyaltyTypeId",
                        column: x => x.RoyaltyTypeId,
                        principalSchema: "public",
                        principalTable: "RoyaltyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_Society_SocietyId",
                        column: x => x.SocietyId,
                        principalSchema: "public",
                        principalTable: "Society",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_Song_SongId",
                        column: x => x.SongId,
                        principalSchema: "public",
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_StatementHeader_StatementHeaderId",
                        column: x => x.StatementHeaderId,
                        principalSchema: "public",
                        principalTable: "StatementHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementDetail_Territory_TerritoryId",
                        column: x => x.TerritoryId,
                        principalSchema: "public",
                        principalTable: "Territory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoyaltyDistribution",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    StatementDetailId = table.Column<int>(nullable: false),
                    WriterId = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoyaltyDistribution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoyaltyDistribution_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalSchema: "public",
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoyaltyDistribution_StatementDetail_StatementDetailId",
                        column: x => x.StatementDetailId,
                        principalSchema: "public",
                        principalTable: "StatementDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoyaltyDistribution_Writer_WriterId",
                        column: x => x.WriterId,
                        principalSchema: "public",
                        principalTable: "Writer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artist_LabelId",
                schema: "public",
                table: "Artist",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_CWRFile_CompanyId",
                schema: "public",
                table: "CWRFile",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreement_CompanyId",
                schema: "public",
                table: "DistributionAgreement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreement_DistributionAgreementFilterId",
                schema: "public",
                table: "DistributionAgreement",
                column: "DistributionAgreementFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_DistributionAgreementId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "DistributionAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_EpisodeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_IsrcId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "IsrcId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_IswcId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "IswcId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_LabelId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_PlatformTierId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "PlatformTierId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_ProductionTitleId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "ProductionTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_PublisherId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_RecipientId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_RoyaltyTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RoyaltyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_SocietyId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "SocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_SourceId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_TerritoryId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EAN_DistributingLabelId",
                schema: "public",
                table: "EAN",
                column: "DistributingLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_EAN_LabelId",
                schema: "public",
                table: "EAN",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_ProductionTitleId",
                schema: "public",
                table: "Episode",
                column: "ProductionTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeAlias_EpisodeId",
                schema: "public",
                table: "EpisodeAlias",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ISRC_ArtistId",
                schema: "public",
                table: "ISRC",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ISRC_SongId",
                schema: "public",
                table: "ISRC",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_ISWC_SongId",
                schema: "public",
                table: "ISWC",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelAlias_LabelId",
                schema: "public",
                table: "LabelAlias",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceived_CurrencyId",
                schema: "public",
                table: "PaymentReceived",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceived_SourceId",
                schema: "public",
                table: "PaymentReceived",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Platform_PlatformTypeId",
                schema: "public",
                table: "Platform",
                column: "PlatformTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformTier_PlatformId",
                schema: "public",
                table: "PlatformTier",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformTierAlias_PlatformTierId",
                schema: "public",
                table: "PlatformTierAlias",
                column: "PlatformTierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionTitle_ProductionTypeId",
                schema: "public",
                table: "ProductionTitle",
                column: "ProductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionTitleAlias_ProductionTitleId",
                schema: "public",
                table: "ProductionTitleAlias",
                column: "ProductionTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_MRAffiliationId",
                schema: "public",
                table: "Publisher",
                column: "MRAffiliationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_PRAffiliationId",
                schema: "public",
                table: "Publisher",
                column: "PRAffiliationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_PublisherTypeId",
                schema: "public",
                table: "Publisher",
                column: "PublisherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_SRAffiliationId",
                schema: "public",
                table: "Publisher",
                column: "SRAffiliationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherAlias_PublisherId",
                schema: "public",
                table: "PublisherAlias",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_PayeeId",
                schema: "public",
                table: "Recipient",
                column: "PayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoyaltyDistribution_RecipientId",
                schema: "public",
                table: "RoyaltyDistribution",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_RoyaltyDistribution_StatementDetailId",
                schema: "public",
                table: "RoyaltyDistribution",
                column: "StatementDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RoyaltyDistribution_WriterId",
                schema: "public",
                table: "RoyaltyDistribution",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_RoyaltyTypeAlias_RoyaltyTypeId",
                schema: "public",
                table: "RoyaltyTypeAlias",
                column: "RoyaltyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoyaltyTypeAlias_SourceId",
                schema: "public",
                table: "RoyaltyTypeAlias",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Society_TerritoryId",
                schema: "public",
                table: "Society",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SocietyAlias_SocietyId",
                schema: "public",
                table: "SocietyAlias",
                column: "SocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_CompanyId",
                schema: "public",
                table: "Song",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SongAlias_SongId",
                schema: "public",
                table: "SongAlias",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceSongCode_SongId",
                schema: "public",
                table: "SourceSongCode",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceSongCode_SourceId",
                schema: "public",
                table: "SourceSongCode",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_EpisodeId",
                schema: "public",
                table: "StatementDetail",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_ISRCId",
                schema: "public",
                table: "StatementDetail",
                column: "ISRCId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_ISWCId",
                schema: "public",
                table: "StatementDetail",
                column: "ISWCId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_LabelId",
                schema: "public",
                table: "StatementDetail",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_PlatformTierId",
                schema: "public",
                table: "StatementDetail",
                column: "PlatformTierId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_ProductionTitleId",
                schema: "public",
                table: "StatementDetail",
                column: "ProductionTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_PublisherId",
                schema: "public",
                table: "StatementDetail",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_RoyaltyTypeId",
                schema: "public",
                table: "StatementDetail",
                column: "RoyaltyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_SocietyId",
                schema: "public",
                table: "StatementDetail",
                column: "SocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_SongId",
                schema: "public",
                table: "StatementDetail",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_StatementHeaderId",
                schema: "public",
                table: "StatementDetail",
                column: "StatementHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetail_TerritoryId",
                schema: "public",
                table: "StatementDetail",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementHeader_PaymentReceivedId",
                schema: "public",
                table: "StatementHeader",
                column: "PaymentReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementHeader_TemplateId",
                schema: "public",
                table: "StatementHeader",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_SourceId",
                schema: "public",
                table: "Template",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Territory_RegionId",
                schema: "public",
                table: "Territory",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryAlias_TerritoryId",
                schema: "public",
                table: "TerritoryAlias",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UPC_DistributingLabelId",
                schema: "public",
                table: "UPC",
                column: "DistributingLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_UPC_LabelId",
                schema: "public",
                table: "UPC",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_WriterShare_DistributionAgreementId",
                schema: "public",
                table: "WriterShare",
                column: "DistributionAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_WriterShare_SongId",
                schema: "public",
                table: "WriterShare",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_WriterShare_WriterId",
                schema: "public",
                table: "WriterShare",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_WriterShare_WriterRoleCodeId",
                schema: "public",
                table: "WriterShare",
                column: "WriterRoleCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CWRFile",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DistributionAgreementDetail",
                schema: "public");

            migrationBuilder.DropTable(
                name: "EAN",
                schema: "public");

            migrationBuilder.DropTable(
                name: "EpisodeAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "LabelAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MechanicalLicense",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PlatformTierAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductionTitleAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PublisherAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RoyaltyDistribution",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RoyaltyTypeAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SocietyAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SongAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SourceSongCode",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TerritoryAlias",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UPC",
                schema: "public");

            migrationBuilder.DropTable(
                name: "WriterShare",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Recipient",
                schema: "public");

            migrationBuilder.DropTable(
                name: "StatementDetail",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DistributionAgreement",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Writer",
                schema: "public");

            migrationBuilder.DropTable(
                name: "WriterRoleCode",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Payee",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Episode",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ISRC",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ISWC",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PlatformTier",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Publisher",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RoyaltyType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "StatementHeader",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DistributionAgreementFilter",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductionTitle",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Artist",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Song",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Platform",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Society",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PublisherType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PaymentReceived",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Template",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductionType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Label",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PlatformType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Territory",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Source",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "public");
        }
    }
}
