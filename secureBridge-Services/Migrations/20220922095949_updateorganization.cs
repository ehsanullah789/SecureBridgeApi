using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secureBridge_Services.Migrations
{
    public partial class updateorganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Zip",
                table: "Organizations",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Zip",
                table: "Organizations",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
