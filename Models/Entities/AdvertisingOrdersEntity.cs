namespace Курсова_робота.Models.Entities
{
    public class AdvertisingOrdersEntity
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public string URL { get; set; }
        public DateTime ViewDate { get; set; }

        public bool isHavingDesign { get; set; }
        public string? ImagePath { get; set; }
    }
}
