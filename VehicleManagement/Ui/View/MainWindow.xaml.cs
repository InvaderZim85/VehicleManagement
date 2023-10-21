using MahApps.Metro.Controls;
using System.Windows;
using VehicleManagement.Common;

namespace VehicleManagement.Ui.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the window was loaded
        /// </summary>
        /// <param name="sender">The <see cref="MainWindow"/></param>
        /// <param name="e">The event arguments</param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // This method is needed to use the logger and taskbar manager!
            Helper.InitHelper(false);
        }
    }
}