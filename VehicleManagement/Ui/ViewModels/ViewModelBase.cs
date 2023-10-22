namespace VehicleManagement.Ui.ViewModels;

/// <summary>
/// Provides the base functions of a view model
/// </summary>
public class ViewModelBase : ObservableObject
{
    /// <summary>
    /// Gets or sets the value which indicates if there is any error (only needed for the input validation)
    /// </summary>
    protected bool HasErrors { get; set; }

    /// <summary>
    /// The message timer
    /// </summary>
    private readonly System.Timers.Timer _messageTimer = new(TimeSpan.FromSeconds(10).TotalMilliseconds);

    /// <summary>
    /// Backing field for <see cref="InfoMessage"/>
    /// </summary>
    private string _infoMessage = string.Empty;

    /// <summary>
    /// Gets or sets the message which should be shown
    /// </summary>
    public string InfoMessage
    {
        get => _infoMessage;
        private set => SetProperty(ref _infoMessage, value);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ViewModelBase"/>
    /// </summary>
    protected ViewModelBase()
    {
        _messageTimer.Elapsed += (_, _) =>
        {
            InfoMessage = string.Empty;
            _messageTimer.Stop();
        };
    }

    

    /// <summary>
    /// Shows an info message for 10 seconds
    /// </summary>
    /// <param name="message">The message which should be shown</param>
    protected void ShowInfoMessage(string message)
    {
        InfoMessage = message;
        _messageTimer.Start();
    }

    /// <summary>
    /// Copies the content to the clipboard
    /// </summary>
    /// <param name="content">The content which should be copied</param>
    protected static void CopyToClipboard(string content)
    {
        Clipboard.SetText(content);
    }
}