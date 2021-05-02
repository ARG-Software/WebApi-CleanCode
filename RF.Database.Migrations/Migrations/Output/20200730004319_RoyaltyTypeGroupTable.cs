using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RF.Database.Migrations.Migrations.Output
{
    public partial class RoyaltyTypeGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "RoyaltyType",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoyaltyTypeGroup",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedByUserLogin = table.Column<string>(nullable: true),
                    ModifiedByUserLogin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoyaltyTypeGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoyaltyType_RoyaltyTypeGroupId",
                schema: "public",
                table: "RoyaltyType",
                column: "RoyaltyTypeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoyaltyType_RoyaltyTypeGroup_RoyaltyTypeGroupId",
                schema: "public",
                table: "RoyaltyType",
                column: "RoyaltyTypeGroupId",
                principalSchema: "public",
                principalTable: "RoyaltyTypeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoyaltyType_RoyaltyTypeGroup_RoyaltyTypeGroupId",
                schema: "public",
                table: "RoyaltyType");

            migrationBuilder.DropTable(
                name: "RoyaltyTypeGroup",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_RoyaltyType_RoyaltyTypeGroupId",
                schema: "public",
                table: "RoyaltyType");

            migrationBuilder.DropColumn(
                name: "RoyaltyTypeGroupId",
                schema: "public",
                table: "RoyaltyType");
        }
    }
}
