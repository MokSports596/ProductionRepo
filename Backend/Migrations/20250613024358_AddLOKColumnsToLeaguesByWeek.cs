using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MokSportsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddLOKColumnsToLeaguesByWeek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Franchise1LOKedTeamId",
                table: "LeaguesByWeek",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Franchise1UsedLoad",
                table: "LeaguesByWeek",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Franchise2LOKedTeamId",
                table: "LeaguesByWeek",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Franchise2UsedLoad",
                table: "LeaguesByWeek",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Franchise3LOKedTeamId",
                table: "LeaguesByWeek",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Franchise3UsedLoad",
                table: "LeaguesByWeek",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Franchise4LOKedTeamId",
                table: "LeaguesByWeek",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Franchise4UsedLoad",
                table: "LeaguesByWeek",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Franchise5LOKedTeamId",
                table: "LeaguesByWeek",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Franchise5UsedLoad",
                table: "LeaguesByWeek",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise1LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise1LOKedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise2LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise2LOKedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise3LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise3LOKedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise4LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise4LOKedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise5LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise5LOKedTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise1LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise1LOKedTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise2LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise2LOKedTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise3LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise3LOKedTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise4LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise4LOKedTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise5LOKedTeamId",
                table: "LeaguesByWeek",
                column: "Franchise5LOKedTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise1LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise2LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise3LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise4LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaguesByWeek_Teams_Franchise5LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropIndex(
                name: "IX_LeaguesByWeek_Franchise1LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropIndex(
                name: "IX_LeaguesByWeek_Franchise2LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropIndex(
                name: "IX_LeaguesByWeek_Franchise3LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropIndex(
                name: "IX_LeaguesByWeek_Franchise4LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropIndex(
                name: "IX_LeaguesByWeek_Franchise5LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise1LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise1UsedLoad",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise2LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise2UsedLoad",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise3LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise3UsedLoad",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise4LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise4UsedLoad",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise5LOKedTeamId",
                table: "LeaguesByWeek");

            migrationBuilder.DropColumn(
                name: "Franchise5UsedLoad",
                table: "LeaguesByWeek");
        }
    }
}
