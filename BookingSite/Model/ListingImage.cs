using BookingSite.Model;

public class ListingImage
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public string Url { get; set; }

    public virtual Listingi Listing { get; set; }
}
