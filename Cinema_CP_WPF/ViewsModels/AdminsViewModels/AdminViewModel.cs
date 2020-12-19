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
        private ICommand _opAdminFilms;
        private ICommand _opAdminSesions;
        private ICommand _opAdminTickets;
        private ICommand _opAdminHalls;
        private ICommand _opAdminPersonal;
        private ICommand _opAdminUser;
        private ICommand _opAdminCinema;
        private ICommand _opAdminExit;
    
        public ICommand OpAdminFilms
        {
            get
            {
                return _opAdminFilms ?? (_opAdminFilms = new RelayCommand(x => { AddFilmView av = new AddFilmView() {DataContext= new AddFilmViewModel()}; av.Show(); }));
            }
        }
        public ICommand OpAdminSesions
        {
            get
            {
                return _opAdminSesions ?? (_opAdminSesions = new RelayCommand(x => { AddSesionView sv = new AddSesionView() { DataContext = new AddSesionViewModel() }; sv.Show(); }));
            }
        }
        public ICommand OpAdminTickets
        {
            get
            {
                return _opAdminTickets ?? (_opAdminTickets = new RelayCommand(x => { TicketsView tv = new TicketsView() { DataContext = new TicketsViewModel() }; tv.Show();}));
            }
        }
        public ICommand OpAdminHalls
        {
            get
            {
                return _opAdminHalls ?? (_opAdminHalls = new RelayCommand(x => { AddHallView hv = new AddHallView() { DataContext = new AddHallViewModel() }; hv.Show(); }));
            }
        }
        public ICommand OpAdminPersonal
        {
            get
            {
                return _opAdminPersonal ?? (_opAdminPersonal = new RelayCommand(x => { EditPersonalView pv = new EditPersonalView() { DataContext = new EditPersonalViewModel() }; pv.Show(); }));
            }
        }
        public ICommand OpAdminUsers
        {
            get
            {
                return _opAdminUser ?? (_opAdminUser = new RelayCommand(x => { EditUsersView pv = new EditUsersView() { DataContext = new EditUsersViewModel() }; pv.Show(); }));
            }
        }

        public ICommand OpAdminCinemaList
        {
            get
            {
                return _opAdminCinema ?? (_opAdminCinema = new RelayCommand(x => { CinemaListView cdv = new CinemaListView() { DataContext = new CinemaListViewModel() }; cdv.Show(); }));
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
