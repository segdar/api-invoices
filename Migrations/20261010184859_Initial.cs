using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiInvoices.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENT",
                columns: table => new
                {
                    CLIENTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    LASTNAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    ADDRESS = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT", x => x.CLIENTID);
                });

            migrationBuilder.CreateTable(
                name: "INVOICEHEADER",
                columns: table => new
                {
                    INVOICEHEADERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLIENTID = table.Column<int>(type: "int", nullable: false),
                    INVOICENUMBER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    INVOICEDATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    STATUS = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TOTALAMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICEHEADER", x => x.INVOICEHEADERID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    PRODUCTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.PRODUCTID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PASSWORDHASH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INVOICEDETAIL",
                columns: table => new
                {
                    INVOICEDETAILID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INVOICEHEADERID = table.Column<int>(type: "int", nullable: false),
                    PRODUCTID = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SUBTOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICEDETAIL", x => x.INVOICEDETAILID);
                    table.ForeignKey(
                        name: "FK_INVOICEDETAIL_INVOICEHEADER_INVOICEHEADERID",
                        column: x => x.INVOICEHEADERID,
                        principalTable: "INVOICEHEADER",
                        principalColumn: "INVOICEHEADERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CLIENT",
                columns: new[] { "CLIENTID", "ADDRESS", "LASTNAME", "NAME" },
                values: new object[,]
                {
                    { 1, "Ciudad de Guatemala", "Perez", "Juan" },
                    { 2, "Antigua Guatemala", "Lopez", "Maria" }
                });

            migrationBuilder.InsertData(
                table: "PRODUCT",
                columns: new[] { "PRODUCTID", "DESCRIPTION" },
                values: new object[,]
                {
                    { 1, "Laptop" },
                    { 2, "Mouse" },
                    { 3, "Keyboard" }
                });

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "ID", "EMAIL", "NAME", "PASSWORDHASH", "STATUS", "USERNAME" },
                values: new object[] { 1, "admin@invoices.com", "Administrator", "$2b$11$D5LrW1xtwO0JzvcGpjYQi.X.JYKNyMye361wTCa2M9D.C..dIwiGq", "active", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_INVOICEDETAIL_INVOICEHEADERID",
                table: "INVOICEDETAIL",
                column: "INVOICEHEADERID");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICEHEADER_INVOICENUMBER",
                table: "INVOICEHEADER",
                column: "INVOICENUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_USERNAME",
                table: "USER",
                column: "USERNAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENT");

            migrationBuilder.DropTable(
                name: "INVOICEDETAIL");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "INVOICEHEADER");
        }
    }
}
