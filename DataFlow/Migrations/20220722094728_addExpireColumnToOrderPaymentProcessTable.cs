using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataTier.Migrations
{
    public partial class addExpireColumnToOrderPaymentProcessTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expired",
                table: "OrderPaymentProceses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expired",
                table: "OrderPaymentProceses");
        }
    }
}
