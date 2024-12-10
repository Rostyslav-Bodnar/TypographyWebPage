namespace Курсова_робота.Models.Entities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EditionEntity> Editions { get; set; } = new List<EditionEntity>();

    }
}
