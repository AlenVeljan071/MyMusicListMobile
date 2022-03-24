using System;

namespace MyMusicListMobile.Models
{
    public class Song
    {
        public string SongId { get; set; }
        public string SongName { get; set; }
        public string Artist { get; set; }
        public string Url { get; set; }
        public int SongRating { get; set; }
        public bool IsAFavorite { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
