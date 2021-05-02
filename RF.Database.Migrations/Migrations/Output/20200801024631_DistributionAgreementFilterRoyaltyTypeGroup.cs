using Microsoft.EntityFrameworkCore.Migrations;

namespace RF.Database.Migrations.Migrations.Output
{
    public partial class DistributionAgreementFilterRoyaltyTypeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "public",
                table: "RoyaltyTypeGroup",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RoyaltyTypeGroup",
                schema: "public",
                table: "DistributionAgreementFilter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionAgreementDetail_RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RoyaltyTypeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~",
                schema: "public",
                table: "DistributionAgreementDetail",
                column: "RoyaltyTypeGroupId",
                principalSchema: "public",
                principalTable: "RoyaltyTypeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionAgreementDetail_RoyaltyTypeGroup_RoyaltyTypeGro~",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropIndex(
                name: "IX_DistributionAgreementDetail_RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "public",
                table: "RoyaltyTypeGroup");

            migrationBuilder.DropColumn(
                name: "RoyaltyTypeGroup",
                schema: "public",
                table: "DistributionAgreementFilter");

            migrationBuilder.DropColumn(
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "DistributionAgreementDetail");
        }
    }
}
