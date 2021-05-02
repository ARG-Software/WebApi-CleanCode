using Microsoft.EntityFrameworkCore.Migrations;

namespace RF.Database.Migrations.Migrations.Output
{
    public partial class DistributionFilterNewProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_Source_SourceId",
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
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "SourceId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.AlterColumn<int>(
                name: "TerritoryId",
                schema: "public",
                table: "StatementDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RoyaltyTypeId",
                schema: "public",
                table: "StatementDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "PlatformType",
                schema: "public",
                table: "DistributionAgreementFilter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProductionType",
                schema: "public",
                table: "DistributionAgreementFilter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Region",
                schema: "public",
                table: "DistributionAgreementFilter",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformType",
                schema: "public",
                table: "DistributionAgreementFilter");

            migrationBuilder.DropColumn(
                name: "ProductionType",
                schema: "public",
                table: "DistributionAgreementFilter");

            migrationBuilder.DropColumn(
                name: "Region",
                schema: "public",
                table: "DistributionAgreementFilter");

            migrationBuilder.AlterColumn<int>(
                name: "TerritoryId",
                schema: "public",
                table: "StatementDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoyaltyTypeId",
                schema: "public",
                table: "StatementDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
