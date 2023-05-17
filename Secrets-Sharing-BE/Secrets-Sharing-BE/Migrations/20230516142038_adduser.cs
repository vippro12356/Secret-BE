using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secrets_Sharing_BE.Migrations
{
    /// <inheritdoc />
    public partial class adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextData_UploadBy",
                table: "TextData",
                column: "UploadBy");

            migrationBuilder.CreateIndex(
                name: "IX_FileData_UploadBy",
                table: "FileData",
                column: "UploadBy");

            migrationBuilder.AddForeignKey(
                name: "FK_FileData_User_UploadBy",
                table: "FileData",
                column: "UploadBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TextData_User_UploadBy",
                table: "TextData",
                column: "UploadBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileData_User_UploadBy",
                table: "FileData");

            migrationBuilder.DropForeignKey(
                name: "FK_TextData_User_UploadBy",
                table: "TextData");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_TextData_UploadBy",
                table: "TextData");

            migrationBuilder.DropIndex(
                name: "IX_FileData_UploadBy",
                table: "FileData");
        }
    }
}
