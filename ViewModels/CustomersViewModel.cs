using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtorsMapping.Data;
using DebtorsMapping.Models;
using DebtorsMapping.Views;

namespace DebtorsMapping.ViewModels
{
    public partial class CustomersViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Customer> Customers { get; } = new();

        public CustomersViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        async Task LoadCustomers()
        {
            Customers.Clear();
            var customersFromDb = await _databaseService.GetCustomersAsync();
            foreach (var customer in customersFromDb)
            {
                Customers.Add(customer);
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _databaseService.UpdateCustomerAsync(customer);
        }
    }
}