// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Business;
using VehicleManagement.Common;
using VehicleManagement.Data;
using VehicleManagement.Services;
using VehicleManagement.Ui.ViewModels.Pages;
using VehicleManagement.Ui.ViewModels.Windows;
using VehicleManagement.Ui.Views.Pages;
using VehicleManagement.Ui.Views.Windows;

namespace VehicleManagement;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost AppHost = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(Path.Combine(AppContext.BaseDirectory));
        })
        .ConfigureServices((_, services) =>
        {
            services.AddHostedService<ApplicationHostService>();

            // Add the database
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(new SqlConnectionStringBuilder
            {
                DataSource = Helper.Settings.Server,
                InitialCatalog = Helper.Settings.Database,
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ApplicationName = "VehicleManagement"
            }.ConnectionString));

            // Add the manager
            services.AddScoped<BaseDataManager>();

            // Main window
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            // Navigation, Snackbar, DialogService
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();

            // Pages
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<BaseDataPage>();
            services.AddSingleton<BaseDataViewModel>();
        }).Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T? GetService<T>() where T : class
    {
        return AppHost.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        AppHost.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await AppHost.StopAsync();

        AppHost.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}