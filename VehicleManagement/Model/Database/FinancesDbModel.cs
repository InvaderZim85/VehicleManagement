using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Model.Database;

/// <summary>
/// Represents a finances entry (insurance, etc.)
/// </summary>
[Table("Finances", Schema = "dbo")]
internal sealed class FinancesDbModel : CreationModifiedDateTimeBase
{
    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the entry
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the type
    /// <para />
    /// Matches <see cref="FinancesTypeDbModel.Id"/>
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// Gets or sets the id of the schedule type
    /// <para />
    /// Matches <see cref="ScheduleTypeDbModel.Id"/>
    /// </summary>
    public int ScheduleTypeId { get; set; }

    /// <summary>
    /// Gets or sets the costs
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Gets or sets the contract date
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime ContractDate { get; set; }

    /// <summary>
    /// Gets or sets the id of the contact
    /// </summary>
    public int? ContactId { get; set; }
}