using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademiaNet.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Addprogramacdemicentitity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicPrograms",
                columns: table => new
                {
                    AcademicProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InstitutionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPrograms", x => x.AcademicProgramID);
                    table.ForeignKey(
                        name: "FK_AcademicPrograms_Institutions_InstitutionID",
                        column: x => x.InstitutionID,
                        principalTable: "Institutions",
                        principalColumn: "InstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPrograms_AcademicProgramID_Name",
                table: "AcademicPrograms",
                columns: new[] { "AcademicProgramID", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPrograms_InstitutionID",
                table: "AcademicPrograms",
                column: "InstitutionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicPrograms");
        }
    }
}
