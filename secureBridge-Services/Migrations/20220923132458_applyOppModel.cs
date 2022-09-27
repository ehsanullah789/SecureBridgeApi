using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secureBridge_Services.Migrations
{
    public partial class applyOppModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyOpportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyOpportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyOpportunities_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyOpportunities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyOpportunities_OpportunityId",
                table: "ApplyOpportunities",
                column: "OpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyOpportunities_UserId",
                table: "ApplyOpportunities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyOpportunities");
        }
    }
}
