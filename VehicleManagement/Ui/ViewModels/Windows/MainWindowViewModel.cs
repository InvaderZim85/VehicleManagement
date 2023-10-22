// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using VehicleManagement.Ui.Views.Pages;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace VehicleManagement.Ui.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF UI - VehicleManagement";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            //new NavigationViewItem()
            //{
            //    Content = "Home",
            //    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            //    TargetPageType = typeof(DashboardPage)
            //},
            //new NavigationViewItem()
            //{
            //    Content = "Data",
            //    Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            //    TargetPageType = typeof(DataPage)
            //}
            new NavigationViewItem
            {
                Content = "Base data",
                Icon = new SymbolIcon
                {
                    Symbol = SymbolRegular.DataArea24,
                },
                TargetPageType = typeof(BaseDataPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
