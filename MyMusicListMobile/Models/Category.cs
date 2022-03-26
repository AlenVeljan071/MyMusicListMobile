namespace MyMusicListMobile.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => "http://localhost/" + ImageUrl;
    }
}
