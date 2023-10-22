using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models.Database;

/// <summary>
/// Represents a contact
/// </summary>
[Table("Contact", Schema = "dbo")]
public sealed class ContactDbModel : CreationModifiedDateTimeBase
{
    /// <summary>
    /// Gets or sets the id of the entry
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the mail address
    /// </summary>
    public string Mail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the additional information
    /// </summary>
    public string AdditionalInfo { get; set; } = string.Empty;
}