using System;
using System.Collections.Generic;
using System.Text;
using TravelMonkey.Models.CognitiveServices;

namespace TravelMonkey.ViewModels
{
    public class LangaugeSelectorPageViewModel : BaseViewModel
    {
        private List<AvailableLanguage> _availableLanguages;

        public List<AvailableLanguage> AvailableLanguages
        {
            get => _availableLanguages;
            set
            {
                Set(ref _availableLanguages, value);
            }
        }
    }
}
