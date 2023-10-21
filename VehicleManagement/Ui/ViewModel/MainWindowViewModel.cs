using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace VehicleManagement.Ui.ViewModel
{
    /// <summary>
    /// Provides the logic for the <see cref="View.MainWindow"/>
    /// </summary>
    internal partial class MainWindowViewModel : ViewModelBase
    {
        /*
        * NOTE: This example makes usage of the community toolkit generator which generates automatically the public property and the needed command
        *       If you don't want to use them, you can use the "old" way
        *
        * Example
        * private string _someInfoText = "Hello from the view model...";
        * public string SomeInfoText
        * {
        *     get => _someInfoText;
        *     set => SetProperty(ref _someInfoText, value);
        * }
        */

        /// <summary>
        /// Backing field for <see cref="SomeInfoText"/>
        /// </summary>
        [ObservableProperty]
        private string _someInfoText = "Hello from the view model...";

        /// <summary>
        /// Shows a dummy message
        /// </summary>
        /// <returns>The awaitable task</returns>
        [RelayCommand]
        private async Task ShowMessageAsync()
        {
            await ShowMessageAsync("Test", "Some message here...");
        }
    }
}