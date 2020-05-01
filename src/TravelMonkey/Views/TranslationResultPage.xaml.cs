using System;
using TravelMonkey.Data;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class TranslationResultPage : ContentPage
    {
        private TranslateResultPageViewModel _translateResultPageViewModel;

        public TranslationResultPage(string inputText)
        {
            InitializeComponent();

            _translateResultPageViewModel = new TranslateResultPageViewModel();

            MessagingCenter.Subscribe<TranslateResultPageViewModel>(this,
                Constants.TranslationFailedMessage,
                async (s) =>
                {
                    await DisplayAlert("Whoops!", "We lost our dictionary, something went wrong while translating", "OK");
                });

            _translateResultPageViewModel.InputText = inputText;

            BindingContext = _translateResultPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (MockDataStore.Languages == null || MockDataStore.Languages.Count == 0)
                _translateResultPageViewModel.LoadLanguageListCommand.Execute(null);

            _translateResultPageViewModel.TranslateTextCommand.Execute(_translateResultPageViewModel.InputText);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SelectLanguagesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LangaugeSelectorPage());
        }
    }
}