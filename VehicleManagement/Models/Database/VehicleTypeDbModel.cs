using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models.Database;

/// <summary>
/// Represents a vehicle type
/// </summary>
[Table("VehicleType", Schema = "dbo")]
public sealed class VehicleTypeDbModel
{
    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}