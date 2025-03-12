using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class for_view_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Problem_Test_trialViewModelId",
                table: "testCase_for_trial",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_testCase_for_trial_Problem_Test_trialViewModelId",
                table: "testCase_for_trial",
                column: "Problem_Test_trialViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_Test_trialViewModel_Problem_for_trialProblemId",
                table: "Problem_Test_trialViewModel",
                column: "Problem_for_trialProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_testCase_for_trial_Problem_Test_trialViewModel_Problem_Test_trialViewModelId",
                table: "testCase_for_trial",
                column: "Problem_Test_trialViewModelId",
                principalTable: "Problem_Test_trialViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_testCase_for_trial_Problem_Test_trialViewModel_Problem_Test_trialViewModelId",
                table: "testCase_for_trial");

            migrationBuilder.DropTable(
                name: "Problem_Test_trialViewModel");

            migrationBuilder.DropIndex(
                name: "IX_testCase_for_trial_Problem_Test_trialViewModelId",
                table: "testCase_for_trial");

            migrationBuilder.DropColumn(
                name: "Problem_Test_trialViewModelId",
                table: "testCase_for_trial");
        }
    }
}
