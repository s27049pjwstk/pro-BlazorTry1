using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsimManager.Migrations
{
    /// <inheritdoc />
    public partial class UserCertificationmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCertifications",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CertificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ApprovedByName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    ApprovedById = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCertifications", x => new { x.UserId, x.CertificationId });
                    table.ForeignKey(
                        name: "FK_UserCertifications_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCertifications_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserCertifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCertifications_ApprovedById",
                table: "UserCertifications",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertifications_CertificationId",
                table: "UserCertifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertifications_UserId",
                table: "UserCertifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCertifications");
        }
    }
}
