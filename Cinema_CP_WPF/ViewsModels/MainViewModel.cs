using Cinema_CP_WPF.Views;
using Cinema_CP_WPF.Views.AdminViews;
using Cinema_CP_WPF.ViewsModels.AdminsViewModels;
using MVVMHelper.Commands;
using MVVMHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels
{
    internal class MainViewModel:BaseViewModel
    {
        private ICommand openCahierView;
        private ICommand openUserView;
        private ICommand openAdminView;
        private ICommand opAdminFilms;


        public ICommand OpenCahierView
        {
            get
            {
                return openCahierView ?? (openCahierView = new RelayCommand(
                    x => { СashierView cv = new СashierView() {DataContext=new СashierViewModel()}; cv.Show(); }
                    ));
            }
        }

        public ICommand OpenUserView
        {
            get 
            {
                return openUserView ?? (openUserView = new RelayCommand(
                    x => { UserView uv = new UserView(); uv.Show(); }
                    ));
            }
        }

        public ICommand OpenAdminView
        {
            get
            {
                return openAdminView ?? (openAdminView = new RelayCommand(x => { AdminView av = new AdminView() { DataContext = new AdminViewModel()}; av.Show(); }));
            }
        }

        public ICommand OpAdminFilms
        {
            get
            {
                return opAdminFilms ?? (opAdminFilms = new RelayCommand(x => { AddFilmView av = new AddFilmView(); av.Show(); }));
            }
        }
    }
}
