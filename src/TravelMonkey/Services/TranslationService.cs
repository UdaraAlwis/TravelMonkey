using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelMonkey.Data;
using TravelMonkey.Models;
using TravelMonkey.Models.CognitiveServices;

namespace TravelMonkey.Services
{
    public class TranslationService
    {
        public async Task<List<AvailableTranslation>> TranslateText(string inputText, List<AvailableLanguage> listOfLanguages)
        {
            // build the translate url chunk
            string requiredTranslationsString = "";
            foreach (var item in listOfLanguages)
            {
                requiredTranslationsString = requiredTranslationsString + $"&to={item.Key}";
            }

            var body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(ApiKeys.TranslationsEndpoint + "/translate?api-version=3.0" + requiredTranslationsString);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKeys.TranslationsApiKey);

                try
                {
                    // Send the request and get response.
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    // Read response as a string.
                    var result = await response.Content.ReadAsStringAsync();
                    TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);

                    var bestResult = deserializedOutput.OrderByDescending(t => t.DetectedLanguage.Score).FirstOrDefault();

                    // Iterate over the results and compose the result
                    var translations = new List<AvailableTranslation>();

                    foreach (Translation t in bestResult.Translations)
                        translations.Add( new AvailableTranslation()
                        {
                            InputLanguage = bestResult.DetectedLanguage.Language,
                            InputText = inputText,
                            TranslatedLanguageKey = t.To,
                            TranslatedLanguageText = t.Text,
                            TranslatedLanguageName = MockDataStore.Languages.FirstOrDefault(x => x.Key == t.To).Name,
                        });

                    return translations;
                }
                catch
                {
                    return new List<AvailableTranslation>();
                }
            }
        }

        public async Task<List<AvailableLanguage>> GetLanguageList() 
        {
            string endpoint = ApiKeys.TranslationsEndpoint;
            string route = "/languages?api-version=3.0";

            TranslationStatsResult translationStatsResult = new TranslationStatsResult();

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // In the next few sections you'll add code to construct the request.

                // Set the method to GET
                request.Method = HttpMethod.Get;
                // Construct the full URI
                request.RequestUri = new Uri(endpoint + route);
                // Send request, get response
                var response = client.SendAsync(request).Result;
                var result = await response.Content.ReadAsStringAsync();
                translationStatsResult = JsonConvert.DeserializeObject<TranslationStatsResult>(result);
            }

            List<AvailableLanguage> availableLanguages = new List<AvailableLanguage>();

            foreach (var item in translationStatsResult.Translation)
            {
                availableLanguages.Add(new AvailableLanguage() 
                {
                    Key = item.Key,
                    Name = item.Value.Name,
                    NativeName = item.Value.NativeName
                });
            }

            return availableLanguages;
        }
    }
}