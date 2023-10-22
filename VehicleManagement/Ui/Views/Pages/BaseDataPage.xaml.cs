using VehicleManagement.Ui.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace VehicleManagement.Ui.Views.Pages;

/// <summary>
/// Interaction logic for BaseDataPage.xaml
/// </summary>
public partial class BaseDataPage : INavigableView<BaseDataViewModel>
{
    public BaseDataViewModel ViewModel { get; }

    public BaseDataPage(BaseDataViewModel vieModel)
    {
        ViewModel = vieModel;
        DataContext = this;

        InitializeComponent();
    }

    private void BaseDataPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadData();
    }
}