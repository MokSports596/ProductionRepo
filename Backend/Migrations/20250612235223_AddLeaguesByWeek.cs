using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MokSportsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaguesByWeek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaguesByWeek",
                columns: table => new
                {
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    SkinsInPlay = table.Column<int>(type: "int", nullable: false),
                    Franchise1Id = table.Column<int>(type: "int", nullable: true),
                    Franchise1WeeklyPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise1SeasonPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise2Id = table.Column<int>(type: "int", nullable: true),
                    Franchise2WeeklyPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise2SeasonPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise3Id = table.Column<int>(type: "int", nullable: true),
                    Franchise3WeeklyPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise3SeasonPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise4Id = table.Column<int>(type: "int", nullable: true),
                    Franchise4WeeklyPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise4SeasonPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise5Id = table.Column<int>(type: "int", nullable: true),
                    Franchise5WeeklyPoints = table.Column<int>(type: "int", nullable: false),
                    Franchise5SeasonPoints = table.Column<int>(type: "int", nullable: false),
                    WeeklyRank1FranchiseId = table.Column<int>(type: "int", nullable: true),
                    WeeklyRank2FranchiseId = table.Column<int>(type: "int", nullable: true),
                    WeeklyRank3FranchiseId = table.Column<int>(type: "int", nullable: true),
                    WeeklyRank4FranchiseId = table.Column<int>(type: "int", nullable: true),
                    WeeklyRank5FranchiseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaguesByWeek", x => new { x.LeagueId, x.WeekId });
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_Franchise1Id",
                        column: x => x.Franchise1Id,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_Franchise2Id",
                        column: x => x.Franchise2Id,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_Franchise3Id",
                        column: x => x.Franchise3Id,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_Franchise4Id",
                        column: x => x.Franchise4Id,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_Franchise5Id",
                        column: x => x.Franchise5Id,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_WeeklyRank1FranchiseId",
                        column: x => x.WeeklyRank1FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_WeeklyRank2FranchiseId",
                        column: x => x.WeeklyRank2FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_WeeklyRank3FranchiseId",
                        column: x => x.WeeklyRank3FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_WeeklyRank4FranchiseId",
                        column: x => x.WeeklyRank4FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaguesByWeek_Franchises_WeeklyRank5FranchiseId",
                        column: x => x.WeeklyRank5FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise1Id",
                table: "LeaguesByWeek",
                column: "Franchise1Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise2Id",
                table: "LeaguesByWeek",
                column: "Franchise2Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise3Id",
                table: "LeaguesByWeek",
                column: "Franchise3Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise4Id",
                table: "LeaguesByWeek",
                column: "Franchise4Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_Franchise5Id",
                table: "LeaguesByWeek",
                column: "Franchise5Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_WeeklyRank1FranchiseId",
                table: "LeaguesByWeek",
                column: "WeeklyRank1FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_WeeklyRank2FranchiseId",
                table: "LeaguesByWeek",
                column: "WeeklyRank2FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_WeeklyRank3FranchiseId",
                table: "LeaguesByWeek",
                column: "WeeklyRank3FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_WeeklyRank4FranchiseId",
                table: "LeaguesByWeek",
                column: "WeeklyRank4FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguesByWeek_WeeklyRank5FranchiseId",
                table: "LeaguesByWeek",
                column: "WeeklyRank5FranchiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaguesByWeek");
        }
    }
}
