using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelMonkey.Data;
using TravelMonkey.Models.CognitiveServices;
using TravelMonkey.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelMonkey.ViewModels
{
    public class TranslateResultPageViewModel : BaseViewModel
    {
        private readonly TranslationService _translationService =
            new TranslationService();

        private string _inputText;
        private List<AvailableTranslation> _translations;
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText == value)
                    return;

                Set(ref _inputText, value);
            }
        }

        public List<AvailableTranslation> Translations
        {
            get => _translations;
            set
            {
                Set(ref _translations, value);
            }
        }

        public Command<string> TranslateTextCommand => new Command<string>(async (inputText) =>
        {
            if (IsBusy)
                return;

            IsBusy = true;

            InputText = inputText;
            await TranslateText();
        });

        public Command LoadLanguageListCommand => new Command(async () =>
        {
            IsBusy = true;

            MockDataStore.Languages = await _translationService.GetLanguageList();

            // Pick random 5 Languages to translate first time
            var randomlySelectedLanguages = MockDataStore.Languages.OrderBy(arg => Guid.NewGuid()).Take(5).ToList();
            for (int i = 0; i < randomlySelectedLanguages.Count; i++)
            {
                MockDataStore.Languages.FirstOrDefault(x => x == randomlySelectedLanguages[i]).IsSelected = true;
            }

            await TranslateText();
        });

        private async Task TranslateText()
        {
            var listOfLanguages = MockDataStore.Languages.Where(x => x.IsSelected == true).ToList();

            var result = await _translationService.TranslateText(_inputText, listOfLanguages);
            if (result == null || result.Count == 0)
                MessagingCenter.Send(this, Constants.TranslationFailedMessage);

            Translations = result;

            IsBusy = false;
        }
    }
}