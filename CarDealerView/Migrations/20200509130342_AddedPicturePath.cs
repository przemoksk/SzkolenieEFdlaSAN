using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealerView.Migrations
{
    public partial class AddedPicturePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Cars",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "CarModels",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
