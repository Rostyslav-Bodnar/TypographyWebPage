namespace Курсова_робота.Models.Entities
{
    public class TypographyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdTypographyStatus { get; set; }
        public TypographyStatusEntity TypographyStatus { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
