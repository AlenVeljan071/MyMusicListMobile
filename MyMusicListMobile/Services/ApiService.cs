using MyMusicListMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicListMobile.Services
{
    public static class ApiService
    {
        public static async Task<IEnumerable<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
        public static async Task<IEnumerable<Song>> GetSongsByFavorite()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "songs/favorite");
            return JsonConvert.DeserializeObject<List<Song>>(response);
        }
        public static async Task<IEnumerable<Song>> GetSongsByCategory(string categoryId)
        {
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "songs/byCategory/" + categoryId);
                return JsonConvert.DeserializeObject<List<Song>>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<bool> AddSong(Song song)
        {
            song.CreatedDate = DateTime.Now;
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(AppSettings.ApiUrl + "songs", content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public static async Task<IEnumerable<Song>> GetSongs()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "songs");
            return JsonConvert.DeserializeObject<List<Song>>(response);
        }
        public static async Task<Song> GetSongsById(string Id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "songs/" + Id);
            return JsonConvert.DeserializeObject<Song>(response);
        }
        public static async Task<bool> EditSong(Song song)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PutAsync(AppSettings.ApiUrl + "songs", content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public static async Task<bool> DeleteSong(Song song)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "songs/" + content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
