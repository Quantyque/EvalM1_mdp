using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvalM1_API_mdp.Migrations
{
    /// <inheritdoc />
    public partial class InitialInitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passwords",
                columns: table => new
                {
                    IdPassword = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdApplication = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passwords", x => x.IdPassword);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    TypeCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    IdApplication = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.IdApplication);
                    table.ForeignKey(
                        name: "FK_Applications_Passwords_IdApplication",
                        column: x => x.IdApplication,
                        principalTable: "Passwords",
                        principalColumn: "IdPassword",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "TypeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Type",
                column: "TypeCode",
                values: new object[]
                {
                    "CLI",
                    "PRO"
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_TypeId",
                table: "Applications",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Passwords");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
