using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusicListMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : ContentPage
    {
        public PlayPage(string url)
        {
            InitializeComponent();
            webView_Video.Source = url;
        }
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}