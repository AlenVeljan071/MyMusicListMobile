namespace MyMusicListMobile.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => "http://192.168.0.24:84/" + ImageUrl;
    }
}
