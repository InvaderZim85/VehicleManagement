using Microsoft.WindowsAPICodePack.Taskbar;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using VehicleManagement.Model.Internal;

namespace VehicleManagement.Common;

/// <summary>
/// Provides several helper functions
/// </summary>
internal static class Helper
{
    /// <summary>
    /// Contains the instance of the taskbar manager
    /// </summary>
    private static TaskbarManager? _taskbarInstance;

    /// <summary>
    /// Contains the settings
    /// </summary>
    private static Settings? _settings;

    /// <summary>
    /// Gets the settings
    /// </summary>
    public static Settings Settings => _settings ??= LoadSettings();

    /// <summary>
    /// Init the helper
    /// </summary>
    /// <param name="verbose">true to create verbose log, otherwise false</param>
    public static void InitHelper(bool verbose)
    {
        const string template = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        if (verbose)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("log/log_.log", rollingInterval: RollingInterval.Day, outputTemplate: template)
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log/log_.log", rollingInterval: RollingInterval.Day, outputTemplate: template)
                .CreateLogger();
        }

        if (TaskbarManager.IsPlatformSupported)
            _taskbarInstance = TaskbarManager.Instance;
    }

    #region Various
    /// <summary>
    /// Opens the explorer and selects the specified file
    /// </summary>
    /// <param name="path">The path of the file</param>
    public static void ShowInExplorer(string path)
    {
        var arguments = Path.HasExtension(path) ? $"/n, /e, /select, \"{path}\"" : $"/n, /e, \"{path}\"";
        Process.Start("explorer.exe", arguments);
    }

    /// <summary>
    /// Opens the specified link
    /// </summary>
    /// <param name="url">The url of the link</param>
    public static void OpenLink(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
    #endregion

    #region Taskbar
    /// <summary>
    /// Sets the taskbar into an indeterminate state
    /// </summary>
    /// <param name="enable"><see langword="true"/> to set the indeterminate state, otherwise <see langword="false"/></param>
    public static void SetTaskbarIndeterminate(bool enable)
    {
        SetTaskbarState(enable, TaskbarProgressBarState.Indeterminate);
    }

    /// <summary>
    /// Sets the taskbar into an error state
    /// </summary>
    /// <param name="enable"><see langword="true"/> to set the indeterminate state, otherwise <see langword="false"/></param>
    public static void SetTaskbarError(bool enable)
    {
        SetTaskbarState(enable, TaskbarProgressBarState.Error);
    }

    /// <summary>
    /// Sets the taskbar into an pause state
    /// </summary>
    /// <param name="enable"><see langword="true"/> to set the indeterminate state, otherwise <see langword="false"/></param>
    public static void SetTaskbarPause(bool enable)
    {
        SetTaskbarState(enable, TaskbarProgressBarState.Paused);
    }

    /// <summary>
    /// Sets the taskbar state
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to set the state, <see langword="false"/> to set <see cref="TaskbarProgressBarState.NoProgress"/></param>
    /// <param name="state">The desired state</param>
    private static void SetTaskbarState(bool enabled, TaskbarProgressBarState state)
    {
        try
        {
            _taskbarInstance?.SetProgressState(enabled ? state : TaskbarProgressBarState.NoProgress);
            switch (enabled)
            {
                case true when state != TaskbarProgressBarState.Indeterminate:
                    _taskbarInstance?.SetProgressValue(100, 100);
                    break;
                case false:
                    _taskbarInstance?.SetProgressValue(0, 0);
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Can't change taskbar state");
        }
    }
    #endregion

    #region Settings
    /// <summary>
    /// Loads the settings
    /// </summary>
    /// <returns>The settings</returns>
    private static Settings LoadSettings()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Settings.json");
        if (!File.Exists(path))
            CreateSettingsFile(path);

        var content = File.ReadAllText(path);

        return JsonConvert.DeserializeObject<Settings>(content) ?? new Settings();
    }

    /// <summary>
    /// Creates an empty settings file with the default values
    /// </summary>
    /// <param name="filepath">The path of the settings file</param>
    private static void CreateSettingsFile(string filepath)
    {
        var content = JsonConvert.SerializeObject(new Settings(), Formatting.Indented);

        File.WriteAllText(filepath, content);
    }
    #endregion
}