using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Ean = table.Column<string>(type: "varchar(128)", nullable: false),
                Sku = table.Column<string>(type: "varchar(64)", nullable: false),
                Name = table.Column<string>(type: "varchar(512)", nullable: false),
                Description = table.Column<string>(type: "varchar(2048)", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Sellers",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "varchar(256)", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sellers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProductPrices",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                SellerId = table.Column<int>(type: "INTEGER", nullable: false),
                Price = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                PriceForDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductPrices", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductPrices_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProductPrices_Sellers_SellerId",
                    column: x => x.SellerId,
                    principalTable: "Sellers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Sellers",
            columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
            values: new object[,]
            {
                { 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "SellerOne", null },
                { 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "SellerTwo", null }
            });

        migrationBuilder.CreateIndex(
            name: "IX_ProductPrices_ProductId",
            table: "ProductPrices",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductPrices_SellerId",
            table: "ProductPrices",
            column: "SellerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ProductPrices");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "Sellers");
    }
}