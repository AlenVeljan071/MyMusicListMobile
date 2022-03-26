using MyMusicListMobile.Models;
using MyMusicListMobile.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusicListMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSongPage : ContentPage
    {
        public Category selectedCategory = new Category();
        public ObservableCollection<Category> categoryCollection;
        public AddSongPage()
        {
            InitializeComponent();
            categoryCollection = new ObservableCollection<Category>();
            GetCategories();
        }
        private async void GetCategories()
        {
            var categories = await ApiService.GetCategories();
            foreach (var category in categories)
            {
                categoryCollection.Add(category);
            }
            comboCat.ItemsSource = categoryCollection;
        }
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private async void TapSignup_Tapped(object sender, EventArgs e)
        {
                var song = new Song()
                {
                    CategoryId = selectedCategory.CategoryId,
                    SongName = EntName.Text,
                    Artist = EntArtist.Text,
                    Url = EntUrl.Text,
                    IsAFavorite = EntIsFavorite.IsChecked,
                    SongRating = Rating.SelectedStarValue,
                };
                var response = await ApiService.AddSong(song);
                if (response == true)
                {
                    await DisplayAlert("", "Your song  is Added", "Ok");
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                }
                else
                {
                    await DisplayAlert("Oops", "Something went wrong, make sure you have entered correctly", "Cancel");
                }
            
        }
        private void comboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCategory = (Category)comboCat.SelectedItem;
        }

    }
}