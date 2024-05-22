﻿// <auto-generated />
using System;
using BookingSite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingSite.Migrations
{
    [DbContext(typeof(BookingDatabaseContext))]
    [Migration("20240522003057_AdjustListingId")]
    partial class AdjustListingId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BookingSite.Model.Drzave", b =>
                {
                    b.Property<int>("DrzavaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("drzava_id");

                    b.Property<string>("Ime")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ime")
                        .HasDefaultValueSql("'NULL'");

                    b.HasKey("DrzavaId")
                        .HasName("PRIMARY");

                    b.ToTable("drzave", "bookingdatabase");
                });

            modelBuilder.Entity("BookingSite.Model.Listingi", b =>
                {
                    b.Property<int>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("listing_id");

                    b.Property<DateTime?>("DatumDo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("datum_do")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<DateTime?>("DatumOd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("datum_od")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<int?>("NeprimicninaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("neprimicnina_id")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Opis")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext")
                        .HasColumnName("opis")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("SlikaUrl")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("slika_url")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<int?>("UporabnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("uporabnik_id")
                        .HasDefaultValueSql("'NULL'");

                    b.HasKey("ListingId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "NeprimicninaId" }, "neprimicnina_id");

                    b.HasIndex(new[] { "UporabnikId" }, "uporabnik_id");

                    b.ToTable("listingi", "bookingdatabase");
                });

            modelBuilder.Entity("BookingSite.Model.Neprimicnine", b =>
                {
                    b.Property<int>("NepremicninaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("nepremicnina_id");

                    b.Property<int?>("DrzavaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("drzava_id")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Kraj")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("kraj")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Nadstropje")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("nadstropje")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Naslov")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("naslov")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("PostnaSt")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("postna_st")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("StevilkaSobe")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("stevilka_sobe")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<int?>("UporabnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("uporabnik_id")
                        .HasDefaultValueSql("'NULL'");

                    b.HasKey("NepremicninaId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "DrzavaId" }, "drzava_id");

                    b.HasIndex(new[] { "UporabnikId" }, "uporabnik_id")
                        .HasDatabaseName("uporabnik_id1");

                    b.ToTable("neprimicnine", "bookingdatabase");
                });

            modelBuilder.Entity("BookingSite.Model.Rezervacije", b =>
                {
                    b.Property<int>("RezervacijaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("rezervacija_id");

                    b.Property<DateTime?>("DatumDo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("datum_do")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<DateTime?>("DatumOd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("datum_od")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<int?>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("listing_id")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<int?>("UporabnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("uporabnik_id")
                        .HasDefaultValueSql("'NULL'");

                    b.HasKey("RezervacijaId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ListingId" }, "listing_id");

                    b.HasIndex(new[] { "UporabnikId" }, "uporabnik_id")
                        .HasDatabaseName("uporabnik_id2");

                    b.ToTable("rezervacije", "bookingdatabase");
                });

            modelBuilder.Entity("BookingSite.Model.TipiUporabnika", b =>
                {
                    b.Property<int>("TipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("tip_id");

                    b.Property<string>("Naziv")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("naziv")
                        .HasDefaultValueSql("'NULL'");

                    b.HasKey("TipId")
                        .HasName("PRIMARY");

                    b.ToTable("tipi_uporabnika", "bookingdatabase");
                });

            modelBuilder.Entity("BookingSite.Model.Uporabniki", b =>
                {
                    b.Property<int>("UporabnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("uporabnik_id");

                    b.Property<string>("DatumRojstva")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("datum_rojstva")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Geslo")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("geslo")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Ime")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ime")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Priimek")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("priimek")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<int?>("TipUporabnikaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("tip_uporabnika_id")
                        .HasDefaultValueSql("'NULL'");

                    b.HasKey("UporabnikId");

                    b.HasIndex("TipUporabnikaId");

                    b.ToTable("uporabniki", (string)null);
                });

            modelBuilder.Entity("BookingSite.Model.Listingi", b =>
                {
                    b.HasOne("BookingSite.Model.Neprimicnine", "Neprimicnina")
                        .WithMany("Listingis")
                        .HasForeignKey("NeprimicninaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("listingi_ibfk_2");

                    b.HasOne("BookingSite.Model.Uporabniki", "Uporabnik")
                        .WithMany("Listingis")
                        .HasForeignKey("UporabnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("listingi_ibfk_1");

                    b.Navigation("Neprimicnina");

                    b.Navigation("Uporabnik");
                });

            modelBuilder.Entity("BookingSite.Model.Neprimicnine", b =>
                {
                    b.HasOne("BookingSite.Model.Drzave", "Drzava")
                        .WithMany("Neprimicnines")
                        .HasForeignKey("DrzavaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("neprimicnine_ibfk_2");

                    b.HasOne("BookingSite.Model.Uporabniki", "Uporabnik")
                        .WithMany("Neprimicnines")
                        .HasForeignKey("UporabnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("neprimicnine_ibfk_1");

                    b.Navigation("Drzava");

                    b.Navigation("Uporabnik");
                });

            modelBuilder.Entity("BookingSite.Model.Rezervacije", b =>
                {
                    b.HasOne("BookingSite.Model.Listingi", "Listing")
                        .WithMany("Rezervacijes")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("rezervacije_ibfk_2");

                    b.HasOne("BookingSite.Model.Uporabniki", "Uporabnik")
                        .WithMany("Rezervacijes")
                        .HasForeignKey("UporabnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("rezervacije_ibfk_1");

                    b.Navigation("Listing");

                    b.Navigation("Uporabnik");
                });

            modelBuilder.Entity("BookingSite.Model.Uporabniki", b =>
                {
                    b.HasOne("BookingSite.Model.TipiUporabnika", "TipUporabnika")
                        .WithMany("Uporabnikis")
                        .HasForeignKey("TipUporabnikaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("uporabniki_ibfk_1");

                    b.Navigation("TipUporabnika");
                });

            modelBuilder.Entity("BookingSite.Model.Drzave", b =>
                {
                    b.Navigation("Neprimicnines");
                });

            modelBuilder.Entity("BookingSite.Model.Listingi", b =>
                {
                    b.Navigation("Rezervacijes");
                });

            modelBuilder.Entity("BookingSite.Model.Neprimicnine", b =>
                {
                    b.Navigation("Listingis");
                });

            modelBuilder.Entity("BookingSite.Model.TipiUporabnika", b =>
                {
                    b.Navigation("Uporabnikis");
                });

            modelBuilder.Entity("BookingSite.Model.Uporabniki", b =>
                {
                    b.Navigation("Listingis");

                    b.Navigation("Neprimicnines");

                    b.Navigation("Rezervacijes");
                });
#pragma warning restore 612, 618
        }
    }
}
