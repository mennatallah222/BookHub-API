using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletedShippingMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_ShippingMethods_ShippingMethodId",
                table: "Shippings");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_ShippingMethodId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "ShippingMethodId",
                table: "Shippings");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseFee",
                table: "Shippings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shippings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseFee",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shippings");

            migrationBuilder.AddColumn<int>(
                name: "ShippingMethodId",
                table: "Shippings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    ShippingMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstimatedDeliveryTime = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.ShippingMethodId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_ShippingMethodId",
                table: "Shippings",
                column: "ShippingMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_ShippingMethods_ShippingMethodId",
                table: "Shippings",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "ShippingMethodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
