using CommunityToolkit.Maui;
using DebtorsMapping.Data;
using DebtorsMapping.ViewModels;
using DebtorsMapping.Views;
using Microsoft.Extensions.Logging;

namespace DebtorsMapping;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiMaps()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // --- Database and Service Registration ---
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "customers3.db3");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<DatabaseService>(s, dbPath));

        // --- ViewModel and View Registration ---
        builder.Services.AddSingleton<CustomersViewModel>();
        builder.Services.AddSingleton<CustomerListPage>();
        builder.Services.AddSingleton<MapPage>(); // Changed to Singleton as it's the main page

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}