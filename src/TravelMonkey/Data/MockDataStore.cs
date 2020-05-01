using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using TravelMonkey.Models;
using TravelMonkey.Models.CognitiveServices;

namespace TravelMonkey.Data
{
    public class MockDataStore
    {
		public static ObservableCollection<PictureEntry> Pictures { get; set; }
            = new ObservableCollection<PictureEntry>();

        public static List<Destination> TopDestinations { get; set; } 
            = new List<Destination>();

        public static Dictionary<string, List<Destination>> DestinationSearchCache { get; set; }
            = new Dictionary<string, List<Destination>>();

        public static List<AvailableLanguage> Languages { get; set; }

        public static List<string> Countries { get { return GetCountries(); } }

        private static List<string> GetCountries()
        {
            List<string> countryList = new List<string>();

            var getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var getCulture in getCultureInfo)
            {
                var regionInfo = new RegionInfo(getCulture.LCID);
                if (!countryList.Contains(regionInfo.EnglishName))
                {
                    countryList.Add(regionInfo.EnglishName);
                }
            }

            return countryList;
        }
    }
}