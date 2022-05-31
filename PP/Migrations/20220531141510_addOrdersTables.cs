using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP.Migrations
{
    public partial class addOrdersTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<DateTime>(type: "datetime2", nullable: false),
                    summ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    customerid = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_customers_customerid",
                        column: x => x.customerid,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    goodid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    summ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRows", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderRows_goods_goodid",
                        column: x => x.goodid,
                        principalTable: "goods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_orderid",
                        column: x => x.orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_goodid",
                table: "OrderRows",
                column: "goodid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_orderid",
                table: "OrderRows",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerid",
                table: "Orders",
                column: "customerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRows");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
