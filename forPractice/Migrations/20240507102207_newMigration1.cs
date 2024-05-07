using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forPractice.Migrations
{
    public partial class newMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "taskModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_taskModels_memberModels_MemberId",
                        column: x => x.MemberId,
                        principalTable: "memberModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_taskModels_MemberId",
                table: "taskModels",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskModels");
        }
    }
}
