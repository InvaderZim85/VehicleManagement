using System.Numerics;
using VehicleManagement.Common.Enums;
using VehicleManagement.Model.Internal;

namespace VehicleManagement.Common;

/// <summary>
/// Contains the different messages
/// </summary>
internal static class MessageHelper
{
    #region Error messages
    /// <summary>
    /// Gets the error message according to the specified type
    /// </summary>
    /// <param name="type">The error type</param>
    /// <param name="additionalInfo">Additional information. Only supported when <paramref name="type"/> is <see cref="Complex"/></param>
    /// <returns>The error message</returns>
    public static MessageEntry GetErrorMessage(ErrorMessageType type, string additionalInfo = "")
    {
        var message = type switch
        {
            ErrorMessageType.Load => "An error occurred while loading the data.",
            ErrorMessageType.Save => "An error occurred while saving / exporting the data.",
            ErrorMessageType.Generate => "An error occurred while generating the data.",
            ErrorMessageType.Import => "An error occurred while importing the data.",
            ErrorMessageType.Connection => "An error occurred while establishing the connection.",
            ErrorMessageType.Complex => string.IsNullOrWhiteSpace(additionalInfo)
                ? "An error has occurred."
                : $"An error has occurred: {additionalInfo}",
            _ => "An error has occurred."
        };

        return new MessageEntry("Error", message, "", "For more information please check the log.");
    }
    #endregion
}