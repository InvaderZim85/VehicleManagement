using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Model.Database;

/// <summary>
/// Represents a vehicle
/// </summary>
[Table("Vehicle", Schema = "dbo")]
internal sealed class VehicleDbModel
{
    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the manufacturer
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the model
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the VIN
    /// </summary>
    public string Vin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the vehicle type
    /// <para />
    /// Matches <see cref="VehicleTypeDbModel.Id"/>
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// Gets or sets the license plate
    /// </summary>
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial registration date
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime InitialRegistrationDate { get; set; }

    /// <summary>
    /// Gets or sets the buy date
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime BuyDate { get; set; }
}