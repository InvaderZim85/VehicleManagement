using System.Collections.ObjectModel;
using VehicleManagement.Business;
using VehicleManagement.Models.Database;

namespace VehicleManagement.Ui.ViewModels.Pages;

public partial class BaseDataViewModel : ViewModelBase
{
    private readonly BaseDataManager _manager;

    [ObservableProperty]
    private ObservableCollection<VehicleTypeDbModel> _vehicleTypes = new();

    public BaseDataViewModel(BaseDataManager manager)
    {
        _manager = manager;
    }

    public async void LoadData()
    {
        var values = await _manager.LoadVehicleTypesAsync();
        VehicleTypes = new ObservableCollection<VehicleTypeDbModel>(values);
    }
}