using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace DebtorsMapping.Models
{
    public partial class Customer : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private double? _latitude;

        [ObservableProperty]
        private double? _longitude;

        [Ignore]
        public bool HasLocation => Latitude.HasValue && Longitude.HasValue;

        [Ignore]
        public string LocationString => HasLocation
            ? $"{Latitude:F4}, {Longitude:F4}"
            : "Not set";

        partial void OnLatitudeChanged(double? value)
        {
            OnPropertyChanged(nameof(HasLocation));
            OnPropertyChanged(nameof(LocationString));
        }

        partial void OnLongitudeChanged(double? value)
        {
            OnPropertyChanged(nameof(HasLocation));
            OnPropertyChanged(nameof(LocationString));
        }
    }
}