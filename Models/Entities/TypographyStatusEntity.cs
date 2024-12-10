namespace Курсова_робота.Models.Entities
{
    public class TypographyStatusEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TypographyEntity> Typographies { get; set; } = new List<TypographyEntity>();

    }
}
