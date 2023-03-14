using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EWalletService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "amount_money",
                table: "Wallets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "TransactionsHistory",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderWalletIdId = table.Column<int>(type: "integer", nullable: false),
                    ReceiverWalletIdId = table.Column<int>(type: "integer", nullable: false),
                    transaction_amount = table.Column<double>(type: "double precision", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsHistory", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_TransactionsHistory_Wallets_ReceiverWalletIdId",
                        column: x => x.ReceiverWalletIdId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionsHistory_Wallets_SenderWalletIdId",
                        column: x => x.SenderWalletIdId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_ReceiverWalletIdId",
                table: "TransactionsHistory",
                column: "ReceiverWalletIdId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_SenderWalletIdId",
                table: "TransactionsHistory",
                column: "SenderWalletIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionsHistory");

            migrationBuilder.AlterColumn<int>(
                name: "amount_money",
                table: "Wallets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
