using DebtorsMapping.Models;
using DebtorsMapping.ViewModels;

namespace DebtorsMapping.Views;

public partial class CustomerListPage : ContentPage
{
    private readonly CustomersViewModel _viewModel;

    public CustomerListPage(CustomersViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCustomersCommand.Execute(null);
    }

    private async void OnActionsClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        var customer = button?.CommandParameter as Customer;
        if (customer == null) return;

        var action = await DisplayActionSheet("Actions", "Cancel", null, "Set Location", "View on Map");

        var navParams = new Dictionary<string, object>
        {
            { "Customer", customer }
        };

        if (action == "Set Location")
        {
            navParams["IsSettingLocation"] = true;
            await Shell.Current.GoToAsync("MainPage", navParams);
        }
        else if (action == "View on Map")
        {
            if (customer.HasLocation)
            {
                await Shell.Current.GoToAsync("MainPage", navParams);
            }
            else
            {
                await DisplayAlert("No Location", "This customer does not have a location set.", "OK");
            }
        }
    }
}