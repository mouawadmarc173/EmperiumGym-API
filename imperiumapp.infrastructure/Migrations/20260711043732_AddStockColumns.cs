using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace imperiumapp.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // إضافة عمود الستوك لجدول المنتجات الموجود أصلاً
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // إضافة عمود التنبيه لجدول المنتجات الموجود أصلاً
            migrationBuilder.AddColumn<int>(
                name: "AlertLimit",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // حذف العواميد في حال التراجع عن التحديث
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AlertLimit",
                table: "Products");
        }
    }
}