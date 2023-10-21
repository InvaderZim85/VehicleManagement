﻿using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using Serilog;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using VehicleManagement.Common;

namespace VehicleManagement.Ui.ViewModel
{
    /// <summary>
    /// Provides the base functions of a view model
    /// </summary>
    internal class ViewModelBase : ObservableObject
    {
        /// <summary>
        /// The instance of the mah apps dialog coordinator
        /// </summary>
        private readonly IDialogCoordinator _dialogCoordinator;

        /// <summary>
        /// The message timer
        /// </summary>
        private readonly Timer _messageTimer = new(TimeSpan.FromSeconds(10).TotalMilliseconds);

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
            _dialogCoordinator = DialogCoordinator.Instance;

            _messageTimer.Elapsed += (_, _) =>
            {
                InfoMessage = string.Empty;
                _messageTimer.Stop();
            };
        }

        /// <summary>
        /// Shows a message dialog
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="message">The message of the dialog</param>
        /// <returns>The awaitable task</returns>
        protected async Task ShowMessageAsync(string title, string message)
        {
            await _dialogCoordinator.ShowMessageAsync(this, title, message);
        }

        /// <summary>
        /// Shows a question dialog with two buttons
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="message">The message of the dialog</param>
        /// <param name="okButtonText">The text of the ok button (optional)</param>
        /// <param name="cancelButtonText">The text of the cancel button (optional)</param>
        /// <returns>The dialog result</returns>
        protected async Task<MessageDialogResult> ShowQuestionAsync(string title, string message, string okButtonText = "OK",
            string cancelButtonText = "Cancel")
        {
            Helper.SetTaskbarPause(true);

            var result = await _dialogCoordinator.ShowMessageAsync(this, title, message,
                MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings
                {
                    AffirmativeButtonText = okButtonText,
                    NegativeButtonText = cancelButtonText
                });

            Helper.SetTaskbarPause(false);

            return result;
        }

        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="ex">The exception which was thrown</param>
        /// <param name="caller">The name of the method, which calls this method. Value will be filled automatically</param>
        /// <returns>The awaitable task</returns>
        protected async Task ShowErrorAsync(Exception ex, [CallerMemberName] string caller = "")
        {
            Helper.SetTaskbarError(true);

            LogError(ex, caller);
            await _dialogCoordinator.ShowMessageAsync(this, "Error", $"An error has occurred: {ex.Message}");

            Helper.SetTaskbarError(false);
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="ex">The exception which was thrown</param>
        /// <param name="caller">The name of the method, which calls this method. Value will be filled automatically</param>
        protected static void LogError(Exception ex, [CallerMemberName] string caller = "")
        {
            Log.Error(ex, "An error has occurred. Caller: {caller}", caller);
        }

        /// <summary>
        /// Shows a progress dialog
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="message">The message of the dialog</param>
        /// <returns>The awaitable task</returns>
        protected async Task<ProgressDialogController> ShowProgressAsync(string title, string message)
        {
            // Close the dialog before a new one will be created
            Helper.SetTaskbarIndeterminate(true);

            var controller = await _dialogCoordinator.ShowProgressAsync(this, title, message);
            controller.SetIndeterminate();

            return controller;
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
        protected void CopyToClipboard(string content)
        {
            Clipboard.SetText(content);
        }
    }
}