using System;
using System.Collections.Generic;
using System.Text;
using TravelMonkey.Models;
using TravelMonkey.Services;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class DestinationResultPageViewModel : BaseViewModel
    {
        private readonly BingSearchService _bingSearchService =
            new BingSearchService();

        private bool _isBusy;
        private string _destinationName;
        private List<Destination> _destinationImages;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public string DestinationName
        {
            get => _destinationName;
            set => Set(ref _destinationName, value);
        }

        public List<Destination> DestinationImages
        {
            get => _destinationImages;
            set => Set(ref _destinationImages, value);
        }

        public Command<string> LoadDataCommand => new Command<string>(async (destination) =>
        {
            if (IsBusy)
                return;

            IsBusy = true;

            DestinationImages = await _bingSearchService.GetDestinationData(destination);

            IsBusy = false;
        });
    }
}
