using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models.Database;

namespace VehicleManagement.Business;

/// <summary>
/// Provides the functions for the administration of the base data
/// </summary>
public sealed class BaseDataManager : ManagerBase
{
    #region Vehicle types
    /// <summary>
    /// Loads all available vehicle types
    /// </summary>
    /// <returns>The list with the vehicle types</returns>
    public async Task<List<VehicleTypeDbModel>> LoadVehicleTypesAsync()
    {
        if (Context == null)
            return new List<VehicleTypeDbModel>();

        return await Context.VehicleTypes.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Adds / updates a vehicle type
    /// </summary>
    /// <param name="vehicleType">The vehicle type</param>
    /// <returns>The awaitable task</returns>
    public async Task SaveVehicleTypeAsync(VehicleTypeDbModel vehicleType)
    {
        if (Context == null)
            return;

        if (vehicleType.Id == 0)
        {
            await Context.VehicleTypes.AddAsync(vehicleType);
        }
        else
        {
            Context.VehicleTypes.Update(vehicleType);
        }

        await Context.SaveChangesAsync();
    }
    #endregion
}