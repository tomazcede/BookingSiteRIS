using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Model;

public partial class BookingDatabaseContext : DbContext
{
    public BookingDatabaseContext()
    {
    }

    public BookingDatabaseContext(DbContextOptions<BookingDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Drzave> Drzaves { get; set; }

    public virtual DbSet<Listingi> Listingis { get; set; }

    public virtual DbSet<Neprimicnine> Neprimicnines { get; set; }

    public virtual DbSet<Rezervacije> Rezervacijes { get; set; }

    public virtual DbSet<TipiUporabnika> TipiUporabnikas { get; set; }

    public virtual DbSet<Uporabniki> Uporabnikis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost,3306;user=root;password=;database=bookingDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drzave>(entity =>
        {
            entity.HasKey(e => e.DrzavaId).HasName("PRIMARY");

            entity.ToTable("drzave", "bookingdatabase");

            entity.Property(e => e.DrzavaId)
                .HasColumnType("int(11)")
                .HasColumnName("drzava_id");
            entity.Property(e => e.Ime)
                .HasMaxLength(40)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ime");
        });

        modelBuilder.Entity<Listingi>(entity =>
        {
            entity.HasKey(e => e.ListingId).HasName("PRIMARY");

            entity.ToTable("listingi", "bookingdatabase");

            entity.HasIndex(e => e.NeprimicninaId, "neprimicnina_id");

            entity.HasIndex(e => e.UporabnikId, "uporabnik_id");

            entity.Property(e => e.ListingId)
                .HasColumnType("int(11)")
                .HasColumnName("listing_id");
            entity.Property(e => e.DatumDo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("datum_do");
            entity.Property(e => e.DatumOd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("datum_od");
            entity.Property(e => e.NeprimicninaId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("neprimicnina_id");
            entity.Property(e => e.Opis)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("opis");
            entity.Property(e => e.SlikaUrl)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("slika_url");
            entity.Property(e => e.UporabnikId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("uporabnik_id");

            entity.HasOne(d => d.Neprimicnina).WithMany(p => p.Listingis)
                .HasForeignKey(d => d.NeprimicninaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("listingi_ibfk_2");

            entity.HasOne(d => d.Uporabnik).WithMany(p => p.Listingis)
                .HasForeignKey(d => d.UporabnikId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("listingi_ibfk_1");
        });

        modelBuilder.Entity<Neprimicnine>(entity =>
        {
            entity.HasKey(e => e.NepremicninaId).HasName("PRIMARY");

            entity.ToTable("neprimicnine", "bookingdatabase");

            entity.HasIndex(e => e.DrzavaId, "drzava_id");

            entity.HasIndex(e => e.UporabnikId, "uporabnik_id");

            entity.Property(e => e.NepremicninaId)
                .HasColumnType("int(11)")
                .HasColumnName("nepremicnina_id");
            entity.Property(e => e.DrzavaId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("drzava_id");
            entity.Property(e => e.Kraj)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("kraj");
            entity.Property(e => e.Nadstropje)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("nadstropje");
            entity.Property(e => e.Naslov)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("naslov");
            entity.Property(e => e.PostnaSt)
                .HasMaxLength(4)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("postna_st");
            entity.Property(e => e.StevilkaSobe)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("stevilka_sobe");
            entity.Property(e => e.UporabnikId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("uporabnik_id");

            entity.HasOne(d => d.Drzava).WithMany(p => p.Neprimicnines)
                .HasForeignKey(d => d.DrzavaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("neprimicnine_ibfk_2");

            entity.HasOne(d => d.Uporabnik).WithMany(p => p.Neprimicnines)
                .HasForeignKey(d => d.UporabnikId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("neprimicnine_ibfk_1");
        });

        modelBuilder.Entity<Rezervacije>(entity =>
        {
            entity.HasKey(e => e.RezervacijaId).HasName("PRIMARY");

            entity.ToTable("rezervacije", "bookingdatabase");

            entity.HasIndex(e => e.ListingId, "listing_id");

            entity.HasIndex(e => e.UporabnikId, "uporabnik_id");

            entity.Property(e => e.RezervacijaId)
                .HasColumnType("int(11)")
                .HasColumnName("rezervacija_id");
            entity.Property(e => e.DatumDo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("datum_do");
            entity.Property(e => e.DatumOd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("datum_od");
            entity.Property(e => e.ListingId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("listing_id");
            entity.Property(e => e.UporabnikId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("uporabnik_id");

            entity.HasOne(d => d.Listing).WithMany(p => p.Rezervacijes)
                .HasForeignKey(d => d.ListingId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rezervacije_ibfk_2");

            entity.HasOne(d => d.Uporabnik).WithMany(p => p.Rezervacijes)
                .HasForeignKey(d => d.UporabnikId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rezervacije_ibfk_1");
        });

        modelBuilder.Entity<TipiUporabnika>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PRIMARY");

            entity.ToTable("tipi_uporabnika", "bookingdatabase");

            entity.Property(e => e.TipId)
                .HasColumnType("int(11)")
                .HasColumnName("tip_id");
            entity.Property(e => e.Naziv)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("naziv");
        });

        modelBuilder.Entity<Uporabniki>(entity =>
        {
            entity.HasKey(e => e.UporabnikId).HasName("PRIMARY");

            entity.ToTable("uporabniki", "bookingdatabase");

            entity.HasIndex(e => e.TipUporabnikaId, "tip_uporabnika_id");

            entity.Property(e => e.UporabnikId)
                .HasColumnType("int(11)")
                .HasColumnName("uporabnik_id");
            entity.Property(e => e.DatumRojstva)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("datum_rojstva");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Geslo)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("geslo");
            entity.Property(e => e.Ime)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ime");
            entity.Property(e => e.Priimek)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("priimek");
            entity.Property(e => e.TipUporabnikaId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("tip_uporabnika_id");

            entity.HasOne(d => d.TipUporabnika).WithMany(p => p.Uporabnikis)
                .HasForeignKey(d => d.TipUporabnikaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("uporabniki_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
