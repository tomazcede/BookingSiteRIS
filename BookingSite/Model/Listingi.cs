using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSite.Model;

public partial class Listingi
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ListingId { get; set; }

    public DateTime? DatumOd { get; set; }

    public DateTime? DatumDo { get; set; }

    public int? NeprimicninaId { get; set; }

    public int? UporabnikId { get; set; }

    public string? Opis { get; set; }

    public string? SlikaUrl { get; set; }


    public virtual Neprimicnine? Neprimicnina { get; set; }

    public virtual ICollection<Rezervacije> Rezervacijes { get; set; } = new List<Rezervacije>();

    public virtual Uporabniki? Uporabnik { get; set; }
}
