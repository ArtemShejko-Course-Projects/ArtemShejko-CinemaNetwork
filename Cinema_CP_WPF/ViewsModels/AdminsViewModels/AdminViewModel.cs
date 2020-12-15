using Cinema_CP_WPF.Views;
using Cinema_CP_WPF.Views.AdminViews;
using MVVMHelper.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                return opAdminFilms ?? (opAdminFilms = new RelayCommand(x => { AddFilmView av = new AddFilmView() {DataContext= new AddFilmViewModel()}; av.Show(); }));
            }
        }
        public ICommand OpAdminSesions
        {
            get
            {
                return opAdminSesions ?? (opAdminSesions = new RelayCommand(x => { AddSesionView sv = new AddSesionView() { DataContext = new AddSesionViewModel() }; sv.Show(); }));
            }
        }
        public ICommand OpAdminTickets
        {
            get
            {
                return opAdminTickets ?? (opAdminTickets = new RelayCommand(x => { TicketsView tv = new TicketsView() { DataContext = new TicketsViewModel() }; tv.Show();}));
            }
        }
        public ICommand OpAdminHalls
        {
            get
            {
                return opAdminHalls ?? (opAdminHalls = new RelayCommand(x => { AddHallView hv = new AddHallView() { DataContext = new AddHallViewModel() }; hv.Show(); }));
            }
        }
        public ICommand OpAdminPersonal
        {
            get
            {
                return opAdminPersonal ?? (opAdminPersonal = new RelayCommand(x => { EditPersonalView pv = new EditPersonalView() { DataContext = new EditPersonalViewModel() }; pv.Show(); }));
            }
        }
        //public ICommand OpAdminExit
        //{
        //    get
        //    {
        //        return opAdminExit ?? (opAdminExit = new RelayCommand(x => Window.GetWindow.Close());
        //    }
        //}
    }
}
