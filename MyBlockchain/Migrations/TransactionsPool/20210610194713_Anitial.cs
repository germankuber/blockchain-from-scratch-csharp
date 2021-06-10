using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlockChain.Migrations.TransactionsPool
{
    public partial class Anitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionsUtxo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalFee = table.Column<int>(type: "int", nullable: false),
                    Spend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsUtxo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionWithFeeDocumentId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfTransactionsInput = table.Column<int>(type: "int", nullable: false),
                    NumberOfTransactionsOutputs = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDocument_TransactionsUtxo_TransactionWithFeeDocumentId",
                        column: x => x.TransactionWithFeeDocumentId,
                        principalTable: "TransactionsUtxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InputDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDocumentId = table.Column<int>(type: "int", nullable: true),
                    TransactionHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionOutputPosition = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputDocument_TransactionDocument_TransactionDocumentId",
                        column: x => x.TransactionDocumentId,
                        principalTable: "TransactionDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutputDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDocumentId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputDocument_TransactionDocument_TransactionDocumentId",
                        column: x => x.TransactionDocumentId,
                        principalTable: "TransactionDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputDocument_TransactionDocumentId",
                table: "InputDocument",
                column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputDocument_TransactionDocumentId",
                table: "OutputDocument",
                column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocument_TransactionWithFeeDocumentId",
                table: "TransactionDocument",
                column: "TransactionWithFeeDocumentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputDocument");

            migrationBuilder.DropTable(
                name: "OutputDocument");

            migrationBuilder.DropTable(
                name: "TransactionDocument");

            migrationBuilder.DropTable(
                name: "TransactionsUtxo");
        }
    }
}
