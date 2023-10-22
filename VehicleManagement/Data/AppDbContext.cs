using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models.Database;

namespace VehicleManagement.Data;

/// <summary>
/// Provides the database tables
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the contacts
    /// </summary>
    public DbSet<ContactDbModel> Contacts => Set<ContactDbModel>();

    /// <summary>
    /// Gets or sets the finances entries
    /// </summary>
    public DbSet<FinancesDbModel> Finances => Set<FinancesDbModel>();

    /// <summary>
    /// Gets or sets the finance types
    /// </summary>
    public DbSet<FinancesTypeDbModel> FinanceTypes => Set<FinancesTypeDbModel>();

    /// <summary>
    /// Gets or sets the schedule types
    /// </summary>
    public DbSet<ScheduleTypeDbModel> ScheduleTypes => Set<ScheduleTypeDbModel>();

    /// <summary>
    /// Gets or sets the settings
    /// </summary>
    public DbSet<SettingsDbModel> Settings => Set<SettingsDbModel>();

    /// <summary>
    /// Gets or sets the vehicles
    /// </summary>
    public DbSet<VehicleDbModel> Vehicles => Set<VehicleDbModel>();

    /// <summary>
    /// Gets or sets the vehicles types
    /// </summary>
    public DbSet<VehicleTypeDbModel> VehicleTypes => Set<VehicleTypeDbModel>();

    /// <summary>
    /// Creates a new instance of the <see cref="AppDbContext"/>
    /// </summary>
    public AppDbContext(DbContextOptions options) : base(options) { }
}