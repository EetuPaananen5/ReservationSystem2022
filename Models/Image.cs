namespace ReservationSystem2022.Models
{
    public class Image
    {
        public long Id { get; set; }
        public String? Description  { get; set; }
        public String Url { get; set; }
        public Item? Target { get; set; }
    }
    public class ImageDTO
    {
        public String Url { get; set; }
        public String? Description { get; set; }

    }
}
