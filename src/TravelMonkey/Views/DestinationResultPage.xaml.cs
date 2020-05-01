using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMonkey.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelMonkey.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DestinationResultPage : ContentPage
    {
        private readonly string _destination;
        DestinationResultPageViewModel _destinationResultPageViewModel;

        public DestinationResultPage(string destination)
        {
            InitializeComponent();

            _destination = destination;
            _destinationResultPageViewModel = new DestinationResultPageViewModel();

            BindingContext = _destinationResultPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _destinationResultPageViewModel.DestinationName = _destination;
            _destinationResultPageViewModel.LoadDataCommand.Execute(_destination);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}