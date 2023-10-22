using VehicleManagement.Data;

namespace VehicleManagement.Business;

/// <summary>
/// Provides the base functions for the manager
/// </summary>
public class ManagerBase
{
    /// <summary>
    /// Gets the database context
    /// </summary>
    protected AppDbContext? Context => App.GetService<AppDbContext>();
}