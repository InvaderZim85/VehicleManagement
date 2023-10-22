namespace VehicleManagement.Model.Internal;

/// <summary>
/// Represents a settings entry
/// </summary>
internal sealed class Settings
{
    /// <summary>
    /// Gets or sets the name / path of the ms sql server
    /// </summary>
    public string Server { get; set; } = "(localdb)\\MsSqlLocalDb";

    /// <summary>
    /// Gets or sets the name of the database
    /// </summary>
    public string Database { get; set; } = "VehicleManagement";

    /// <summary>
    /// Gets or sets the value which indicates if a verbose log should be created
    /// </summary>
    public bool VerboseLog { get; set; } = true;
}