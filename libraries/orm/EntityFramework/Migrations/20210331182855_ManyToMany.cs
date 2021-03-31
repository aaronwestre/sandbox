using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fingers_Hands_HandId",
                table: "Fingers");

            migrationBuilder.DropForeignKey(
                name: "FK_Hands_Arms_ArmId",
                table: "Hands");

            migrationBuilder.DropIndex(
                name: "IX_Hands_ArmId",
                table: "Hands");

            migrationBuilder.DropIndex(
                name: "IX_Fingers_HandId",
                table: "Fingers");

            migrationBuilder.DropColumn(
                name: "ArmId",
                table: "Hands");

            migrationBuilder.DropColumn(
                name: "HandId",
                table: "Fingers");

            migrationBuilder.CreateTable(
                name: "ArmHand",
                columns: table => new
                {
                    ArmsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    HandsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmHand", x => new { x.ArmsId, x.HandsId });
                    table.ForeignKey(
                        name: "FK_ArmHand_Arms_ArmsId",
                        column: x => x.ArmsId,
                        principalTable: "Arms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArmHand_Hands_HandsId",
                        column: x => x.HandsId,
                        principalTable: "Hands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FingerHand",
                columns: table => new
                {
                    FingersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    HandsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FingerHand", x => new { x.FingersId, x.HandsId });
                    table.ForeignKey(
                        name: "FK_FingerHand_Fingers_FingersId",
                        column: x => x.FingersId,
                        principalTable: "Fingers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FingerHand_Hands_HandsId",
                        column: x => x.HandsId,
                        principalTable: "Hands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArmHand_HandsId",
                table: "ArmHand",
                column: "HandsId");

            migrationBuilder.CreateIndex(
                name: "IX_FingerHand_HandsId",
                table: "FingerHand",
                column: "HandsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmHand");

            migrationBuilder.DropTable(
                name: "FingerHand");

            migrationBuilder.AddColumn<Guid>(
                name: "ArmId",
                table: "Hands",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HandId",
                table: "Fingers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hands_ArmId",
                table: "Hands",
                column: "ArmId");

            migrationBuilder.CreateIndex(
                name: "IX_Fingers_HandId",
                table: "Fingers",
                column: "HandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fingers_Hands_HandId",
                table: "Fingers",
                column: "HandId",
                principalTable: "Hands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hands_Arms_ArmId",
                table: "Hands",
                column: "ArmId",
                principalTable: "Arms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
