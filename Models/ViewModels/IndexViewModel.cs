using Курсова_робота.Models.Entities;

namespace Курсова_робота.Models.ViewModels
{

    public class IndexViewModel
    {
        public IEnumerable<EditionEntity> Books { get; set; }
        public IEnumerable<AdvertisingOrdersEntity> Ads { get; set; }
        public IEnumerable<GenreEntity> Genres { get; set; }
    }

}
