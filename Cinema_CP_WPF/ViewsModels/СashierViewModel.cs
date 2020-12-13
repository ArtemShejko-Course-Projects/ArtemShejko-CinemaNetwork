using CinemaDAL;
using MVVMHelper.Commands;
using MVVMHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels
{
    internal class СashierViewModel:BaseViewModel
    {
        //ObservableCollection<Halls> hallslist;
        ObservableCollection<FilmSessions> _sesionlist;
        ObservableCollection<FilmSessions> _sortedbydatesesionlist;
        ObservableCollection<Ticket> tiketlist;
        private ICommand _sellTicket;
        CinemaContext _context;
        Halls _SelectedHalls;

        public СashierViewModel()
        {
            _context = new CinemaContext();
            _context.Halls.Include(f=>f.FilmSessions).Load();
            _context.FilmSessions.Include(f => f.Films).Load();
            Hallslist = _context.Halls.Local;
            Sesionlist = _context.FilmSessions.Local;
        }

        public ICommand SellTicket
        {
            get
            {
                return _sellTicket ?? (_sellTicket = new RelayCommand(
                    x => { Ticket ticket = new Ticket(); Tiketlist.Add(ticket); }
                    ));
            }
        }

        public Halls SelectedHall
        {
            get
            {
                return _SelectedHalls;
            }
            set
            {
                _SelectedHalls = value;
                 RaisePropertyChanged();
            }
        }

        public ObservableCollection<Halls> Hallslist { get; set; }
        public ObservableCollection<Ticket> Tiketlist { get => tiketlist; set => tiketlist = value; }
        public ObservableCollection<FilmSessions> Sesionlist { get => _sesionlist; set => _sesionlist = value; }
        public ObservableCollection<FilmSessions> Sortedbydatesesionlist { get => _sortedbydatesesionlist; set => _sortedbydatesesionlist = value; }
    }
}
