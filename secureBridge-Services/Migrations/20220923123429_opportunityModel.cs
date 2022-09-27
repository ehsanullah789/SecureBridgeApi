using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secureBridge_Services.Migrations
{
    public partial class opportunityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHousing = table.Column<bool>(type: "bit", nullable: false),
                    IsJob = table.Column<bool>(type: "bit", nullable: false),
                    IsEvent = table.Column<bool>(type: "bit", nullable: false),
                    IsFood = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunities_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OrganizationId",
                table: "Opportunities",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opportunities");
        }
    }
}
