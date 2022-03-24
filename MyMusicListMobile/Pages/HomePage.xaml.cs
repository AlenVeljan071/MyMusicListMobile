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
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Category> categoryCollection;
        public ObservableCollection<Song> songCollection;
        public ObservableCollection<Song> songSearchCollection;
        public HomePage()
        {
            InitializeComponent();
            categoryCollection = new ObservableCollection<Category>();
            songCollection = new ObservableCollection<Song>();
            songSearchCollection = new ObservableCollection<Song>();
            GetCategories();
            GetFavoriteSong();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CloseHamburger();
        }
        private async void GetFavoriteSong()
        {
            var songs = await ApiService.GetSongsByFavorite();
            foreach (var song in songs)
            {
                songCollection.Add(song);
            }
            CvSongs.ItemsSource = songCollection;
        }
        private async void GetCategories()
        {
            var categories = await ApiService.GetCategories();
            foreach (var category in categories)
            {
                categoryCollection.Add(category);
            }
            CvCategories.ItemsSource = categoryCollection;
        }

        private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new SongsPage(currentSelection.CategoryId, currentSelection.CategoryName));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void CvSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Song;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new PlayPage(currentSelection.Url));
            ((CollectionView)sender).SelectedItem = null;
        }
        private async void TapMenu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }
        private void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            CloseHamburger();
        }
        private async void CloseHamburger()
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }
        private void AddSongs_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddSongPage());
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
                    if (songSelected == null) await DisplayAlert("", "Your song not exist", "Cancel");
                    await Navigation.PushModalAsync(new PlayPage(songSelected.Url.ToString()));
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
        private void TapFavorite_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EditSongPage());
        }
        private void TapRemove_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DeleteSong());
        }

    }
}