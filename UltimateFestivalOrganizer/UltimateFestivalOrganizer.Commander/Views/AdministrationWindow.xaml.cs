using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;
using UltimateFestivalOrganizer.Commander.ViewModels;

namespace UltimateFestivalOrganizer.Commander.Views
{
    /// <summary>
    /// Interaction logic for AdministrationWindow.xaml
    /// </summary>
    public partial class AdministrationWindow : MetroWindow
    {
        private IIOService ioService = new BaseIOService();

        public AdministrationWindow()
        {
            InitializeComponent();
            this.DataContext = new AdministrationVM(ServiceFactory.GetAdministrationService(), ioService);
            RegisterMessageBoxes();
        }
        private void RegisterMessageBoxes()
        {
            AppMessages.ShowSuccessMessage.Register(this, (message) =>
            {
                var dialogSettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "OK",
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                this.ShowMessageAsync("Success", message, MessageDialogStyle.Affirmative, dialogSettings);
            });

            AppMessages.ShowErrorMessage.Register(this, (message) =>
            {
                var dialogSettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "OK",
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                this.ShowMessageAsync("An Error Occured", message, MessageDialogStyle.Affirmative, dialogSettings);
            });
        }

    }
}
