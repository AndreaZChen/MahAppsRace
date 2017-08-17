using System;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace MahAppsRace
{
    public partial class MainWindow
    {
        private bool _dialogHasBeenClosed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CloseCustomDialog(object sender, RoutedEventArgs e)
        {
            if (_dialogHasBeenClosed)
            {
                Console.WriteLine(@"Dialog already closed!");
                return;
            }
            _dialogHasBeenClosed = true;

            var dialog = (BaseMetroDialog)Resources["CustomCloseDialogTest"];

            Console.WriteLine(@"HideMetroDialogAsync called");
            var hideTask = this.HideMetroDialogAsync(dialog);

            // Open a very shortlived transient dialog while the first dialog is closing
            var transientDialog = (BaseMetroDialog)Resources["CustomDialogTest"];
            await this.ShowMetroDialogAsync(transientDialog);
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            await this.HideMetroDialogAsync(transientDialog);

            await hideTask;
            Console.WriteLine(@"HideMetroDialogAsync completed");
        }
        
        private async void OpenCustomDialog(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)Resources["CustomCloseDialogTest"];

            _dialogHasBeenClosed = false;
            await this.ShowMetroDialogAsync(dialog);
        }
    }
}
