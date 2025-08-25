using DebtorsMapping.Models;
using DebtorsMapping.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace DebtorsMapping.Views;

[QueryProperty(nameof(CustomerToHighlight), "Customer")]
[QueryProperty(nameof(IsSettingLocation), "IsSettingLocation")]
public partial class MapPage : ContentPage
{
    private readonly CustomersViewModel _viewModel;

    public Customer CustomerToHighlight { get; set; }
    public bool IsSettingLocation { get; set; }

    public MapPage(CustomersViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCustomersCommand.ExecuteAsync(null);
        DrawCustomersOnMap();
    }

    private void DrawCustomersOnMap()
    {
        map.Pins.Clear();

        foreach (var customer in _viewModel.Customers)
        {
            if (customer.HasLocation)
            {
                var pin = new Pin
                {
                    Label = customer.Name,
                    Location = new Location(customer.Latitude.Value, customer.Longitude.Value),
                    Type = PinType.Place
                };
                map.Pins.Add(pin);
            }
        }

        if (CustomerToHighlight?.HasLocation == true)
        {
            var highlightedLocation = new Location(CustomerToHighlight.Latitude.Value, CustomerToHighlight.Longitude.Value);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(highlightedLocation, Distance.FromKilometers(1)));
        }
        else
        {
            var windhoekLocation = new Location(-22.57, 17.083);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(windhoekLocation, Distance.FromKilometers(5)));
        }
    }

    private async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        if (IsSettingLocation && CustomerToHighlight != null)
        {
            var confirmed = await DisplayAlert("Confirm Location",
                $"Set location for {CustomerToHighlight.Name} to these coordinates?", "Yes", "No");

            if (confirmed)
            {
                CustomerToHighlight.Latitude = e.Location.Latitude;
                CustomerToHighlight.Longitude = e.Location.Longitude;
                await _viewModel.UpdateCustomer(CustomerToHighlight);
                await Shell.Current.GoToAsync(".."); // Go back to the previous page (list)
            }
        }
    }

    private async void OnViewListClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CustomerListPage));
    }
}