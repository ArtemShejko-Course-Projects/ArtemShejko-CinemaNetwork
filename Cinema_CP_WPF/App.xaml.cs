using Cinema_CP_WPF.Views;
using Cinema_CP_WPF.ViewsModels;
using Cinema_CP_WPF.ViewsModels.AdminsViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema_CP_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var view = new LoginView() { DataContext = new LoginViewModel() };
            MainWindow = view;
            view.Show();
        }
    }
}
