using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP.Migrations
{
    public partial class addOrderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    summ = table.Column<decimal>(type: "decimal(15,4)", nullable: false, defaultValue: 0m),
                    customerid = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_orders_customers_customerid",
                        column: x => x.customerid,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "o_rows",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    goodid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(15,4)", nullable: false, defaultValue: 0m),
                    summ = table.Column<decimal>(type: "decimal(15,4)", nullable: false, computedColumnSql: "[quantity]*[price]"),
                    quantity = table.Column<decimal>(type: "decimal(15,4)", nullable: false, defaultValue: 0m),
                    Ordersid = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_o_rows", x => x.id);
                    table.ForeignKey(
                        name: "FK_o_rows_goods_goodid",
                        column: x => x.goodid,
                        principalTable: "goods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_o_rows_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_o_rows_orders_Ordersid",
                        column: x => x.Ordersid,
                        principalTable: "orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_o_rows_goodid",
                table: "o_rows",
                column: "goodid");

            migrationBuilder.CreateIndex(
                name: "IX_o_rows_orderid",
                table: "o_rows",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_o_rows_Ordersid",
                table: "o_rows",
                column: "Ordersid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customerid",
                table: "orders",
                column: "customerid")
                .Annotation("SqlServer:Include", new[] { "id", "summ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "o_rows");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
