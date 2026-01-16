using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsimManager.Migrations
{
    /// <inheritdoc />
    public partial class UnitAssignmentLogmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitAssignmentLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    UnitName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    UnitAbbreviation = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ApprovedById = table.Column<int>(type: "INTEGER", nullable: true),
                    ApprovedByName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitAssignmentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitAssignmentLogs_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UnitAssignmentLogs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UnitAssignmentLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitAssignmentLogs_ApprovedById",
                table: "UnitAssignmentLogs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitAssignmentLogs_UnitId",
                table: "UnitAssignmentLogs",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitAssignmentLogs_UserId",
                table: "UnitAssignmentLogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitAssignmentLogs");
        }
    }
}
