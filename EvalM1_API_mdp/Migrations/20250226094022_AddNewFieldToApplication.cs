using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvalM1_API_mdp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Applications",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Applications");
        }
    }
}
