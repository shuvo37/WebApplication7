using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class createTHree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Problems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProblemViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_ProblemViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestCases",
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
                    table.PrimaryKey("PK_TestCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCases_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_ProblemId",
                table: "TestCases",
                column: "ProblemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProblemViewModel");

            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.CreateTable(
                name: "Problem_for_trial",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Constraints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputGenScript = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputGenScript = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem_for_trial", x => x.ProblemId);
                });

            migrationBuilder.CreateTable(
                name: "Problem_Test_trialViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Problem_for_trialProblemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem_Test_trialViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problem_Test_trialViewModel_Problem_for_trial_Problem_for_trialProblemId",
                        column: x => x.Problem_for_trialProblemId,
                        principalTable: "Problem_for_trial",
                        principalColumn: "ProblemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "testCase_for_trial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpectedOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSample = table.Column<bool>(type: "bit", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Problem_Test_trialViewModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testCase_for_trial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_testCase_for_trial_Problem_Test_trialViewModel_Problem_Test_trialViewModelId",
                        column: x => x.Problem_Test_trialViewModelId,
                        principalTable: "Problem_Test_trialViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Problem_Test_trialViewModel_Problem_for_trialProblemId",
                table: "Problem_Test_trialViewModel",
                column: "Problem_for_trialProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_testCase_for_trial_Problem_Test_trialViewModelId",
                table: "testCase_for_trial",
                column: "Problem_Test_trialViewModelId");
        }
    }
}
