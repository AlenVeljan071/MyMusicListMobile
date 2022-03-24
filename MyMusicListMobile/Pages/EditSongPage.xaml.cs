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
    public partial class EditSongPage : ContentPage
    {
        public Song songEdit;
        public ObservableCollection<Song> songSearchCollection;
        public EditSongPage()
        {
            InitializeComponent();
            songSearchCollection = new ObservableCollection<Song>();
        }
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private async void TapSignup_Tapped(object sender, EventArgs e)
        {
            var spngUpdate = new Song()
            {
                SongId = songEdit.SongId,
                SongName = songEdit.SongName,
                Artist = songEdit.Artist,
                CategoryId = songEdit.CategoryId,
                CreatedDate = songEdit.CreatedDate,
                Url = songEdit.Url,
                SongRating = Rating.SelectedStarValue,
                IsAFavorite = EntIsFavorite.IsChecked
            };

            var response = await ApiService.EditSong(spngUpdate);
            if (response)
            {
                await DisplayAlert("", "Your song updated succesfully", "Ok");
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }

            Application.Current.MainPage = new NavigationPage(new HomePage());
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
                    Rating.SelectedStarValue = songSelected.SongRating;
                    EntIsFavorite.IsChecked = songSelected.IsAFavorite;

                    songEdit = songSelected;
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

    }
}