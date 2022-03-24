using MyMusicListMobile.Models;
using MyMusicListMobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusicListMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteSong : ContentPage
    {
        public ObservableCollection<Song> songSearchCollection;
        public Song songDelete;
        public DeleteSong()
        {
            InitializeComponent();
            songSearchCollection = new ObservableCollection<Song>();
        }
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private async void SearchBarSong_SearchButtonPressed(object sender, EventArgs e)
        {
            var songSearch = SearchBarSong.Text;
            var songs = await ApiService.GetSongs();

            if (songs != null)
            {
                foreach (var song in songs)
                {
                    songSearchCollection.Add(song);
                }

                if (songSearchCollection.Where(x => x.SongName.ToLower() == songSearch.ToLower()).Any())
                {
                    var songSelected = songSearchCollection.Where(x => x.SongName.ToLower() == songSearch.ToLower()).FirstOrDefault();
                    LblSongName.Text = songSelected.SongName;
                    LblArtist.Text = songSelected.Artist;
                    songDelete = songSelected;
                }
                else
                {
                    await DisplayAlert("", "Your song not exist", "Cancel");
                }
            }
            else
            {
                await DisplayAlert("", "Your song not exist", "Cancel");
            }
        }
        private async void TapSignup_Tapped(object sender, EventArgs e)
        {
            var response = await ApiService.DeleteSong(songDelete);
            if (response == true)
            {
                await DisplayAlert("", "Your song deleted succesfully", "Ok");
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}