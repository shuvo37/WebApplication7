using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class create_Profile_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Submission",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserImgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_UserImg_UserImgId",
                        column: x => x.UserImgId,
                        principalTable: "UserImg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submission_ProfileId",
                table: "Submission",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_UserImgId",
                table: "Profile",
                column: "UserImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Profile_ProfileId",
                table: "Submission",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Profile_ProfileId",
                table: "Submission");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Submission_ProfileId",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Submission");
        }
    }
}
