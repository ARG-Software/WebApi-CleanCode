using Microsoft.EntityFrameworkCore.Migrations;

namespace RF.Database.Migrations.Migrations.Output
{
    public partial class updateDistributionAgreementDetailFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlatformTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductionTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                schema: "public",
                table: "DistributionAgreementDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                schema: "public",
                table: "DistributionAgreementDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_PlatformTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "PlatformTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_ProductionTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "ProductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_RegionId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RoyaltyTypeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_SourceId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionAgreementDetail_PlatformType_PlatformTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "PlatformTypeId",
                principalSchema: "public",
                principalTable: "PlatformType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionAgreementDetail_ProductionType_ProductionTypeId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "ProductionTypeId",
                principalSchema: "public",
                principalTable: "ProductionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionAgreementDetail_Region_RegionId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RegionId",
                principalSchema: "public",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RoyaltyTypeGroupId",
                principalSchema: "public",
                principalTable: "RoyaltyTypeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionAgreementDetail_Source_SourceId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "SourceId",
                principalSchema: "public",
                principalTable: "Source",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_PlatformType_PlatformTypeId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_ProductionType_ProductionTypeId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_Region_RegionId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_Source_SourceId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropIndex(
                name: "IX_DistributionAgreementDetail_PlatformTypeId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropIndex(
                name: "IX_DistributionAgreementDetail_ProductionTypeId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropIndex(
                name: "IX_DistributionAgreementDetail_RegionId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropIndex(
                name: "IX_DistributionAgreementDetail_RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropIndex(
                name: "IX_DistributionAgreementDetail_SourceId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "PlatformTypeId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "ProductionTypeId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "RegionId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "SourceId",
                schema: "public",
                table: "DistributionAgreementDetail");
        }
    }
}
