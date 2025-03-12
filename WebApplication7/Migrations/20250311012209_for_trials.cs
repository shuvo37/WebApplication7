using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class for_trials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Problem_for_trial",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Constraints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputGenScript = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputGenScript = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem_for_trial", x => x.ProblemId);
                });

            migrationBuilder.CreateTable(
                name: "testCase_for_trial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    InputData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSample = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testCase_for_trial", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Problem_for_trial");

            migrationBuilder.DropTable(
                name: "testCase_for_trial");
        }
    }
}
