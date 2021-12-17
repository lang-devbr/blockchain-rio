using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blockchain.Rio.Infra.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(nullable: true),
                    Index = table.Column<long>(nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Hash = table.Column<string>(nullable: false),
                    PrevHash = table.Column<string>(nullable: false),
                    Nounce = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(nullable: true),
                    From = table.Column<string>(maxLength: 100, nullable: false),
                    To = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(maxLength: 500, nullable: false),
                    BlockId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BlockId",
                table: "Transaction",
                column: "BlockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Block");
        }
    }
}
