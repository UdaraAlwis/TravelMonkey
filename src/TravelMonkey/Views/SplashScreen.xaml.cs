using System.Threading.Tasks;
using TravelMonkey.Data;
using TravelMonkey.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class SplashScreen : ContentPage
    {
        private readonly BingSearchService _bingSearchService = new BingSearchService();

        public SplashScreen()
        {
            InitializeComponent();

            InitializeData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartPulseAnimation();
        }

        private void StartPulseAnimation()
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => logo.Scale = f, logo.Scale, logo.Scale, Easing.Linear, 0, 0.1);
            animation.WithConcurrent((f) => logo.Scale = f, logo.Scale, logo.Scale * 1.05, Easing.Linear, 0.1, 0.4);
            animation.WithConcurrent((f) => logo.Scale = f, logo.Scale * 1.05, logo.Scale, Easing.Linear, 0.4, 1);

            Device.BeginInvokeOnMainThread(() =>
            {
                logo.Animate("Pulse", animation, 16, 1000, repeat: () => true);
            });
        }

        private async void InitializeData()
        {
            MockDataStore.TopDestinations = await _bingSearchService.GetTopDestinationList();

            await MainThread.InvokeOnMainThreadAsync(AnimateTransition);

            NavigateToMainPage();
        }

        private async Task AnimateTransition()
        {
            // To at least show the pulse animation. Give a feeling that we're loading the app.
            await Task.Delay(1000);

            // Explode the logo and fade to white, which is what the incoming page comes up as.
            // UdaraAlwis - NO! that looks horrible! please try this out instead,
            // Scaling up smoothly upto 5x and Fading out as the next page comes up.
            var explodeImageTask = Task.WhenAll(
                Content.ScaleTo(5, 300, Easing.CubicOut),
                Content.FadeTo(0, 300, Easing.CubicInOut));

            BackgroundColor = Color.White;
            await explodeImageTask;
        }

        private void NavigateToMainPage()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new MainPage();
            });
        }
    }
}