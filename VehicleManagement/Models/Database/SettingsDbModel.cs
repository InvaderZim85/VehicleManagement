using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models.Database;

/// <summary>
/// Represents a settings entry
/// </summary>
[Table("Settings", Schema = "dbo")]
public sealed class SettingsDbModel : CreationModifiedDateTimeBase
{
    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the key
    /// </summary>
    public int Key { get; set; }

    /// <summary>
    /// Gets or sets the value
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}