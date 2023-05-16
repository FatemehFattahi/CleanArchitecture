using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOnlyConvertor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PriceForDate",
                table: "ProductPrices",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductPrices",
                type: "decimalcls(15,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PriceForDate",
                table: "ProductPrices",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductPrices",
                type: "decimal(15,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimalcls(15,3)");
        }
    }
}
