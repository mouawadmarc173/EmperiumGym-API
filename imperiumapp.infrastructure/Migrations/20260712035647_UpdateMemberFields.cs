using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace imperiumapp.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemberFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // تم تعطيل هذه الأسطر لأن الأعمدة موجودة مسبقاً في الداتابيز
            // migrationBuilder.AddColumn<int>(
            //     name: "LowStockAlert",
            //     table: "Products",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);

            // migrationBuilder.AddColumn<int>(
            //     name: "StockQuantity",
            //     table: "Products",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // تم تعطيل هذه الأسطر أيضاً للحماية
            // migrationBuilder.DropColumn(
            //     name: "Barcode",
            //     table: "Products");

            // migrationBuilder.DropColumn(
            //     name: "LowStockAlert",
            //     table: "Products");

            // migrationBuilder.DropColumn(
            //     name: "StockQuantity",
            //     table: "Products");
        }
    }
}