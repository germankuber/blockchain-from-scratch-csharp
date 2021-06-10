using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlockChain.Migrations.BlockChain
{
    public partial class Initial_Creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockHeaderDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSpan = table.Column<TimeSpan>(type: "time", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nonce = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    MerkleRoot = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockHeaderDocument", x => x.Id);
                });

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
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_BlockHeaderDocument_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "BlockHeaderDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionWithFeeDocumentId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfTransactionsInput = table.Column<int>(type: "int", nullable: false),
                    NumberOfTransactionsOutputs = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockDocumentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Blocks_BlockDocumentId",
                        column: x => x.BlockDocumentId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionsUtxo_TransactionWithFeeDocumentId",
                        column: x => x.TransactionWithFeeDocumentId,
                        principalTable: "TransactionsUtxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
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
                    table.PrimaryKey("PK_Inputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inputs_Transactions_TransactionDocumentId",
                        column: x => x.TransactionDocumentId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Outputs",
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
                    table.PrimaryKey("PK_Outputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outputs_Transactions_TransactionDocumentId",
                        column: x => x.TransactionDocumentId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_HeaderId",
                table: "Blocks",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_TransactionDocumentId",
                table: "Inputs",
                column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_TransactionDocumentId",
                table: "Outputs",
                column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BlockDocumentId",
                table: "Transactions",
                column: "BlockDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionWithFeeDocumentId",
                table: "Transactions",
                column: "TransactionWithFeeDocumentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Outputs");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "TransactionsUtxo");

            migrationBuilder.DropTable(
                name: "BlockHeaderDocument");
        }
    }
}
