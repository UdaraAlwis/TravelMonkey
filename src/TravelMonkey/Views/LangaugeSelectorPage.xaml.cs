using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMonkey.Data;
using TravelMonkey.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelMonkey.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LangaugeSelectorPage : ContentPage
    {
        LangaugeSelectorPageViewModel _langaugesPageViewModel;

        public LangaugeSelectorPage()
        {
            InitializeComponent();

            _langaugesPageViewModel = new LangaugeSelectorPageViewModel();

            BindingContext = _langaugesPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _langaugesPageViewModel.AvailableLanguages = MockDataStore.Languages;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            MockDataStore.Languages = _langaugesPageViewModel.AvailableLanguages;
        }
    }
}