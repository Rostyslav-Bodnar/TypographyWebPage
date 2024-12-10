namespace Курсова_робота.Models.Entities
{

    public class EditionEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int IdType { get; set; }
        public TypeEntity TypeEntity { get; set; }
        public int IdGenre { get; set; }
        public GenreEntity Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }


    }
}
