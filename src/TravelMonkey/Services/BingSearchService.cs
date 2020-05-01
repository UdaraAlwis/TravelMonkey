using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using TravelMonkey.Models;
using System.Globalization;
using Xamarin.Forms;
using TravelMonkey.Data;

namespace TravelMonkey.Services
{
    public class BingSearchService
    {
        private Random _randomResultIndex = new Random();

        public async Task<List<Destination>> GetTopDestinationList()
        {
            // Most visited destinations by international tourist arrivals
            // https://en.wikipedia.org/wiki/World_Tourism_rankings
            var searchDestinations = new[] { "France", "Spain", "United States", "China", "Italy", "Turkey", "Mexico", "Germany", "Thailand", "United Kingdom" };

            try
            {
                var resultDestinations = new List<Destination>();

                foreach (var destination in searchDestinations)
                {
                    var results = await GetDestinationData(destination);

                    var randomIdx = _randomResultIndex.Next(results.Count);
                    resultDestinations.Add(results[randomIdx]);
                }

                return resultDestinations;
            }
            catch (Exception ex)
            {
                return new List<Destination> {
                    new Destination
                    {
                        Title = $"Something went wrong {new Emoji(0x1F615).ToString()} Here is a cat instead!",
                        ImageUrl = "https://cataas.com/cat",
                        MoreInfoUrl = "https://cataas.com/"
                    }
                };
            }
        }

        public async Task<List<Destination>> GetDestinationData(string destination)
        {
            if (MockDataStore.DestinationSearchCache.ContainsKey(destination))
            {
                return MockDataStore.DestinationSearchCache[destination];
            }

            var client = new ImageSearchClient(
                new Microsoft.Azure.CognitiveServices.Search.ImageSearch.ApiKeyServiceClientCredentials(
                    ApiKeys.BingImageSearch));

            var imageResult = await client.Images.SearchAsync($"{destination} attractions", minWidth: 500, minHeight: 500, imageType: "Photo", license: "Public", count: 10, maxHeight: 1200, maxWidth: 1200);

            var resultDestinations = new List<Destination>();
            foreach (var imageResultItem in imageResult.Value)
            {
                resultDestinations.Add(new Destination
                {
                    Title = destination,
                    ImageUrl = imageResultItem.ContentUrl,
                    MoreInfoUrl = imageResultItem.HostPageUrl
                });
            }

            MockDataStore.DestinationSearchCache.Add(destination, resultDestinations);

            return resultDestinations;
        }
    }
}