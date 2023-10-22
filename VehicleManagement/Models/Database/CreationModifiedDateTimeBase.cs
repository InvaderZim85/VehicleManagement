namespace VehicleManagement.Models.Database;

/// <summary>
/// Provides the creation / modification date / time properties
/// </summary>
public class CreationModifiedDateTimeBase
{
    /// <summary>
    /// Gets or sets the creation date / time
    /// </summary>
    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the modification date / time
    /// </summary>
    public DateTime ModifiedDateTime { get; set; } = DateTime.Now;
}