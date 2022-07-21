using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataTier.Migrations
{
    public partial class addpaymenttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Amaunt = table.Column<int>(type: "int", nullable: false),
                    ExternalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartNubmer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPaymentDetails_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPaymentProceses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentProceses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPaymentProceses_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentDetails_ExternalID",
                table: "OrderPaymentDetails",
                column: "ExternalID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentDetails_OrderId",
                table: "OrderPaymentDetails",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentProceses_ExternalId",
                table: "OrderPaymentProceses",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentProceses_OrderId",
                table: "OrderPaymentProceses",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPaymentDetails");

            migrationBuilder.DropTable(
                name: "OrderPaymentProceses");
        }
    }
}
