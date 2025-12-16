using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorTry1.Migrations
{
    /// <inheritdoc />
    public partial class UserAwardmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAwards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AwardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ApprovedByName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    ApprovedById = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAwards_Awards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAwards_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserAwards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAwards_ApprovedById",
                table: "UserAwards",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAwards_AwardId",
                table: "UserAwards",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAwards_UserId",
                table: "UserAwards",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAwards");
        }
    }
}
