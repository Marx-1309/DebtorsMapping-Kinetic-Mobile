using DebtorsMapping.Views;

namespace DebtorsMapping;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for navigation
        Routing.RegisterRoute(nameof(CustomerListPage), typeof(CustomerListPage));
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Routing.RegisterRoute("MapPage", typeof(MapPage));
    }
}
