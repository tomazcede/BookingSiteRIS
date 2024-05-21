using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookingSite.Migrations
{
    /// <inheritdoc />
    public partial class AutoIncrementPrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bookingdatabase");

            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "drzave",
                schema: "bookingdatabase",
                columns: table => new
                {
                    drzava_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ime = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.drzava_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipi_uporabnika",
                schema: "bookingdatabase",
                columns: table => new
                {
                    tip_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    naziv = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.tip_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "uporabniki",
                schema: "bookingdatabase",
                columns: table => new
                {
                    uporabnik_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ime = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    priimek = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    datum_rojstva = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    geslo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    tip_uporabnika_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uporabnik_id);
                    table.ForeignKey(
                        name: "uporabniki_ibfk_1",
                        column: x => x.tip_uporabnika_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "tipi_uporabnika",
                        principalColumn: "tip_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "neprimicnine",
                schema: "bookingdatabase",
                columns: table => new
                {
                    nepremicnina_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    naslov = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    kraj = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "'NULL'"),
                    postna_st = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true, defaultValueSql: "'NULL'"),
                    uporabnik_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'"),
                    nadstropje = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, defaultValueSql: "'NULL'"),
                    stevilka_sobe = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, defaultValueSql: "'NULL'"),
                    drzava_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.nepremicnina_id);
                    table.ForeignKey(
                        name: "neprimicnine_ibfk_1",
                        column: x => x.uporabnik_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "uporabniki",
                        principalColumn: "uporabnik_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "neprimicnine_ibfk_2",
                        column: x => x.drzava_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "drzave",
                        principalColumn: "drzava_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "listingi",
                schema: "bookingdatabase",
                columns: table => new
                {
                    listing_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    datum_od = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "'NULL'"),
                    datum_do = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "'NULL'"),
                    neprimicnina_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'"),
                    uporabnik_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'"),
                    opis = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "'NULL'"),
                    slika_url = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.listing_id);
                    table.ForeignKey(
                        name: "listingi_ibfk_1",
                        column: x => x.uporabnik_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "uporabniki",
                        principalColumn: "uporabnik_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "listingi_ibfk_2",
                        column: x => x.neprimicnina_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "neprimicnine",
                        principalColumn: "nepremicnina_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rezervacije",
                schema: "bookingdatabase",
                columns: table => new
                {
                    rezervacija_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    datum_od = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "'NULL'"),
                    datum_do = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "'NULL'"),
                    listing_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'"),
                    uporabnik_id = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.rezervacija_id);
                    table.ForeignKey(
                        name: "rezervacije_ibfk_1",
                        column: x => x.uporabnik_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "uporabniki",
                        principalColumn: "uporabnik_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "rezervacije_ibfk_2",
                        column: x => x.listing_id,
                        principalSchema: "bookingdatabase",
                        principalTable: "listingi",
                        principalColumn: "listing_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "neprimicnina_id",
                schema: "bookingdatabase",
                table: "listingi",
                column: "neprimicnina_id");

            migrationBuilder.CreateIndex(
                name: "uporabnik_id",
                schema: "bookingdatabase",
                table: "listingi",
                column: "uporabnik_id");

            migrationBuilder.CreateIndex(
                name: "drzava_id",
                schema: "bookingdatabase",
                table: "neprimicnine",
                column: "drzava_id");

            migrationBuilder.CreateIndex(
                name: "uporabnik_id1",
                schema: "bookingdatabase",
                table: "neprimicnine",
                column: "uporabnik_id");

            migrationBuilder.CreateIndex(
                name: "listing_id",
                schema: "bookingdatabase",
                table: "rezervacije",
                column: "listing_id");

            migrationBuilder.CreateIndex(
                name: "uporabnik_id2",
                schema: "bookingdatabase",
                table: "rezervacije",
                column: "uporabnik_id");

            migrationBuilder.CreateIndex(
                name: "tip_uporabnika_id",
                schema: "bookingdatabase",
                table: "uporabniki",
                column: "tip_uporabnika_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rezervacije",
                schema: "bookingdatabase");

            migrationBuilder.DropTable(
                name: "listingi",
                schema: "bookingdatabase");

            migrationBuilder.DropTable(
                name: "neprimicnine",
                schema: "bookingdatabase");

            migrationBuilder.DropTable(
                name: "uporabniki",
                schema: "bookingdatabase");

            migrationBuilder.DropTable(
                name: "drzave",
                schema: "bookingdatabase");

            migrationBuilder.DropTable(
                name: "tipi_uporabnika",
                schema: "bookingdatabase");
        }
    }
}
