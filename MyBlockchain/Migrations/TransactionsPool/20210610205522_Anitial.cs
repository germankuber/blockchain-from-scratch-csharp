#region

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace MyBlockChain.Migrations.TransactionsPool
{
    public partial class Anitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         name: "Transactions",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<int>(type: "int", nullable: false)
                                                                         .Annotation("SqlServer:Identity", "1, 1"),
                                                               TransactionId =
                                                                   table.Column<string>(type: "nvarchar(max)",
                                                                                        nullable: true),
                                                               NumberOfTransactionsInput =
                                                                   table.Column<int>(type: "int", nullable: false),
                                                               NumberOfTransactionsOutputs =
                                                                   table.Column<int>(type: "int", nullable: false),
                                                               Date = table.Column<DateTime>(type: "datetime2",
                                                                                             nullable: false)
                                                           },
                                         constraints: table => { table.PrimaryKey("PK_Transactions", x => x.Id); });

            migrationBuilder.CreateTable(
                                         name: "Inputs",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<int>(type: "int", nullable: false)
                                                                         .Annotation("SqlServer:Identity", "1, 1"),
                                                               TransactionDocumentId =
                                                                   table.Column<int>(type: "int", nullable: true),
                                                               TransactionHash =
                                                                   table.Column<string>(type: "nvarchar(max)",
                                                                                        nullable: true),
                                                               TransactionOutputPosition =
                                                                   table.Column<int>(type: "int", nullable: false),
                                                               Signature = table.Column<string>(type: "nvarchar(max)",
                                                                                                nullable: true)
                                                           },
                                         constraints: table =>
                                                      {
                                                          table.PrimaryKey("PK_Inputs", x => x.Id);
                                                          table.ForeignKey(
                                                                           name:
                                                                           "FK_Inputs_Transactions_TransactionDocumentId",
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
                                                               TransactionDocumentId =
                                                                   table.Column<int>(type: "int", nullable: true),
                                                               State  = table.Column<int>(type: "int", nullable: false),
                                                               Amount = table.Column<int>(type: "int", nullable: false),
                                                               Receiver = table.Column<string>(type: "nvarchar(max)",
                                                                                               nullable: true)
                                                           },
                                         constraints: table =>
                                                      {
                                                          table.PrimaryKey("PK_Outputs", x => x.Id);
                                                          table.ForeignKey(
                                                                           name:
                                                                           "FK_Outputs_Transactions_TransactionDocumentId",
                                                                           column: x => x.TransactionDocumentId,
                                                                           principalTable: "Transactions",
                                                                           principalColumn: "Id",
                                                                           onDelete: ReferentialAction.Restrict);
                                                      });

            migrationBuilder.CreateTable(
                                         name: "TransactionsUtxo",
                                         columns: table => new
                                                           {
                                                               Id = table.Column<int>(type: "int", nullable: false)
                                                                         .Annotation("SqlServer:Identity", "1, 1"),
                                                               TotalFee = table.Column<int>(type: "int",
                                                                                            nullable: false),
                                                               Spend = table.Column<bool>(type: "bit", nullable: false),
                                                               TransactionId =
                                                                   table.Column<int>(type: "int", nullable: true)
                                                           },
                                         constraints: table =>
                                                      {
                                                          table.PrimaryKey("PK_TransactionsUtxo", x => x.Id);
                                                          table.ForeignKey(
                                                                           name:
                                                                           "FK_TransactionsUtxo_Transactions_TransactionId",
                                                                           column: x => x.TransactionId,
                                                                           principalTable: "Transactions",
                                                                           principalColumn: "Id",
                                                                           onDelete: ReferentialAction.Restrict);
                                                      });

            migrationBuilder.CreateIndex(
                                         name: "IX_Inputs_TransactionDocumentId",
                                         table: "Inputs",
                                         column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                                         name: "IX_Outputs_TransactionDocumentId",
                                         table: "Outputs",
                                         column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                                         name: "IX_TransactionsUtxo_TransactionId",
                                         table: "TransactionsUtxo",
                                         column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                                       name: "Inputs");

            migrationBuilder.DropTable(
                                       name: "Outputs");

            migrationBuilder.DropTable(
                                       name: "TransactionsUtxo");

            migrationBuilder.DropTable(
                                       name: "Transactions");
        }
    }
}