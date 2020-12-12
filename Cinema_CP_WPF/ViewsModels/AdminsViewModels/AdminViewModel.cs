using Cinema_CP_WPF.Views;
using Cinema_CP_WPF.Views.AdminViews;
using MVVMHelper.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels.AdminsViewModels
{
    public class AdminViewModel
    {
        private ICommand opAdminFilms;
        private ICommand opAdminSesions;
        private ICommand opAdminTickets;
        private ICommand opAdminHalls;
        private ICommand opAdminPersonal;
        private ICommand opAdminReservedTickets;
        private ICommand opAdminExit;
    
        public ICommand OpAdminFilms
        {
            get
            {
                return opAdminFilms ?? (opAdminFilms = new RelayCommand(x => { AddFilmView av = new AddFilmView(); av.Show(); }));
            }
        }
    }
}
