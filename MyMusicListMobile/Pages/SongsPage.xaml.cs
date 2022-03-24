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
    public partial class SongsPage : ContentPage
    {
        public ObservableCollection<Song> songsByCategory;
        public SongsPage(string categoryId, string categoryName)
        {

            InitializeComponent();
            songsByCategory = new ObservableCollection<Song>();
            LblCategoryName.Text = categoryName;
            GetSongsByCategory(categoryId);
        }
        private async void GetSongsByCategory(string categoryId)
        {
            var songs = await ApiService.GetSongsByCategory(categoryId);
            foreach (var song in songs)
            {
                songsByCategory.Add(song);
            }
            CvSongs.ItemsSource = songsByCategory;
        }
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void CvSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Song;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new PlayPage(currentSelection.Url));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}