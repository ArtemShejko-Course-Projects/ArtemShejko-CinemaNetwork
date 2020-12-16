using CinemaDAL;
using MVVMHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_CP_WPF.ViewsModels.AdminsViewModels
{
    public class TicketsViewModel:BaseViewModel
    {
        CinemaContext _context;
        ObservableCollection<Ticket> _ticketlist;
        ObservableCollection<Place> _places;
        Ticket _selectedTicket;
        Place _selectedPlace;

        public TicketsViewModel()
        {
            _context = new CinemaContext();
            _context.Ticket.Include(f=>f.FilmSessions).Include(p=>p.Place).Include(fm=>fm.FilmSessions.Films).Include(h=>h.Place.Halls).Load();
            TicketList = _context.Ticket.Local;
            _context.Place.Load();
            Places = _context.Place.Local;

        }
        public ObservableCollection<Ticket> TicketList
        {
            get 
            {               
                return _ticketlist; 
            }
            set
            {
                _ticketlist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Place> Places
        {
            get
            {
                return _places;
            }
            set
            {
                _places = value;
                RaisePropertyChanged();
            }
        }

        public Ticket SelectedTicket
        {
            get { return _selectedTicket; }
            set 
            {
                _selectedTicket = value;
                RaisePropertyChanged();
            }
        }

        public Place SelectedPlace
        {
            get { return _selectedPlace; }
            set 
            {
                _selectedPlace = value;
                RaisePropertyChanged();
            }
        }


    }
}
