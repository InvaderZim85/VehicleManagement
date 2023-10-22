using Microsoft.EntityFrameworkCore;
using VehicleManagement.Common.Enums;
using VehicleManagement.Models.Database;

namespace VehicleManagement.Business;

/// <summary>
/// Provides the functions for the interaction with the database settings
/// </summary>
internal sealed class SettingsManager : ManagerBase
{
    /// <summary>
    /// Loads all available settings
    /// </summary>
    /// <returns>The list with the settings</returns>
    public async Task<List<SettingsDbModel>> LoadSettingsAsync()
    {
        if (Context == null)
            return new List<SettingsDbModel>();

        return await Context.Settings.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Loads the value of a single settings entry
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="key">The settings key</param>
    /// <param name="fallback">The fallback if there is no value</param>
    /// <returns>The value</returns>
    public async Task<T?> LoadSettingsValueAsync<T>(SettingsKey key, T? fallback = default)
    {
        if (Context == null)
            return default;

        var value = await Context.Settings.AsNoTracking().FirstOrDefaultAsync(f => f.Key == (int)key);

        if (value == null)
            return fallback;

        return (T)Convert.ChangeType(value, typeof(T));
    }

    /// <summary>
    /// Saves the settings value
    /// </summary>
    /// <param name="key">The key</param>
    /// <param name="value">The value which should be saved</param>
    /// <returns>The awaitable task</returns>
    public async Task SaveSettingsValueAsync(SettingsKey key, object value)
    {
        if (Context == null)
            return;

        var entry = await Context.Settings.FirstOrDefaultAsync(f => f.Key == (int)key);
        if (entry == null)
        {
            entry = new SettingsDbModel
            {
                Key = (int)key,
                Description = "/",
                Value = value.ToString() ?? string.Empty
            };

            await Context.Settings.AddAsync(entry);
        }
        else
        {
            entry.Value = value.ToString() ?? string.Empty;
            entry.ModifiedDateTime = DateTime.Now;
        }

        await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Saves a settings entry
    /// </summary>
    /// <param name="entry">The entry which should be saved</param>
    /// <returns>The awaitable task</returns>
    public async Task SaveSettingsEntryAsync(SettingsDbModel entry)
    {
        if (Context == null)
            return;

        if (entry.Id == 0)
        {
            await Context.Settings.AddAsync(entry);
        }
        else
        {
            entry.ModifiedDateTime = DateTime.Now;
            Context.Settings.Update(entry);
        }

        await Context.SaveChangesAsync();
    }
}