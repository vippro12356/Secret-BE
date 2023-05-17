using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secrets_Sharing_BE.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutoDelete = table.Column<bool>(type: "bit", nullable: false),
                    UploadBy = table.Column<int>(type: "int", nullable: false),
                    Protect = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileData", x => x.Id);
                    table.UniqueConstraint("AK_FileData_Protect", x => x.Protect);
                });

            migrationBuilder.CreateTable(
                name: "TextData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutoDelete = table.Column<bool>(type: "bit", nullable: false),
                    UploadBy = table.Column<int>(type: "int", nullable: false),
                    Protect = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextData", x => x.Id);
                    table.UniqueConstraint("AK_TextData_Protect", x => x.Protect);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileData");

            migrationBuilder.DropTable(
                name: "TextData");
        }
    }
}
