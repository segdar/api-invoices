using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiInvoices.Infrastructure.Migrations
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
                    USERNAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    PASSWORDHASH = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    STATUS = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    REFRESHTOKEN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    REFRESHTOKENEXPIRESAT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INVOICEHEADER",
                columns: table => new
                {
                    INVOICEHEADERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLIENTID = table.Column<int>(type: "int", nullable: false),
                    INVOICENUMBER = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    INVOICEDATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    STATUS = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TOTALAMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICEHEADER", x => x.INVOICEHEADERID);
                    table.ForeignKey(
                        name: "FK_INVOICEHEADER_CLIENT_CLIENTID",
                        column: x => x.CLIENTID,
                        principalTable: "CLIENT",
                        principalColumn: "CLIENTID",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_INVOICEDETAIL_PRODUCT_PRODUCTID",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "PRODUCTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "ID", "EMAIL", "NAME", "PASSWORDHASH", "REFRESHTOKEN", "REFRESHTOKENEXPIRESAT", "STATUS", "USERNAME" },
                values: new object[] { 1, "admin@example.com", "Test Admin", "$2a$11$TAZ6f2YwaL7zeBm166w6Mujy6kKap6KK6nGm7qaVR9nSr7ylzJj.G", null, null, "active", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_INVOICEDETAIL_INVOICEHEADERID",
                table: "INVOICEDETAIL",
                column: "INVOICEHEADERID");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICEDETAIL_PRODUCTID",
                table: "INVOICEDETAIL",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICEHEADER_CLIENTID",
                table: "INVOICEHEADER",
                column: "CLIENTID");

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
                name: "INVOICEDETAIL");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "INVOICEHEADER");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "CLIENT");
        }
    }
}
