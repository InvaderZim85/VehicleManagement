using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Model.Database;

namespace VehicleManagement.Data;

/// <summary>
/// Provides the database tables
/// </summary>
internal class AppDbContext : DbContext
{
    /// <summary>
    /// Contains the name / path of the MS SQL server
    /// </summary>
    private readonly string _server;

    /// <summary>
    /// Contains the name of the database
    /// </summary>
    private readonly string _database;

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
    /// <param name="server">The name / path of the MS SQL server</param>
    /// <param name="database">The name of the database</param>
    protected AppDbContext(string server, string database)
    {
        _server = server;
        _database = database;
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conString = new SqlConnectionStringBuilder
        {
            DataSource = _server,
            InitialCatalog = _database,
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            ApplicationName = "VehicleManagement"
        }.ConnectionString;

        optionsBuilder.UseSqlServer(conString);
    }
}