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
using System.Windows;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels.AdminsViewModels
{
    public class TicketsViewModel : BaseViewModel
    {
        CinemaContext _context;
        ObservableCollection<Ticket> _ticketlist;
        ObservableCollection<Ticket> _sortedticketlist;
        ObservableCollection<Place> _places;
        Ticket _selectedTicket;
        Place _selectedPlace;

        public int forsortPlaceId { get; set; }

        ICommand _sortticketbyplaceid;
        ICommand _reset;
        ICommand _deleteTiket;
        ICommand _saveChanges;
        ICommand _deletePlace;


        public TicketsViewModel()
        {
            _context = new CinemaContext();
            SortedTicketList = new ObservableCollection<Ticket>();
            _context.Ticket.Include(f => f.Place.FilmSessions).Include(p => p.Place).Include(fm => fm.Place.FilmSessions.Films).Include(h => h.Place.Halls).Load();
            TicketList = _context.Ticket.Local;
            _context.Place.Load();
            Places = _context.Place.Local;
            UpdateSortedTicketList();
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
        public ObservableCollection<Ticket> SortedTicketList
        {
            get
            {
                return _sortedticketlist;
            }
            set
            {
                _sortedticketlist = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SortByPlaceId
        {
            get
            {
                return _sortticketbyplaceid ?? (_sortticketbyplaceid = new RelayCommand(x =>
                {
                    try
                    {
                        if (forsortPlaceId == 0)
                        {
                            if (SortedTicketList.Count > 0)
                                SortedTicketList.Clear();
                            foreach (var ticket in TicketList)
                            {
                                SortedTicketList.Add(ticket);
                            }
                        }
                        else
                        {
                            if (SortedTicketList.Count > 0)
                                SortedTicketList.Clear();

                            foreach (var ticket in TicketList.Where(p => p.PlaceId == forsortPlaceId))
                            {
                                SortedTicketList.Add(ticket);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }));
            }
        }

        public ICommand ResetPlaceIdSort
        {
            get
            {
                return _reset ?? (_reset = new RelayCommand(x =>
                {
                    try
                    {
                        if (SortedTicketList.Count > 0)
                            SortedTicketList.Clear();
                        foreach (var ticket in TicketList)
                        {
                            SortedTicketList.Add(ticket);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }));
            }
        }

        public ICommand DeleteTicket
        {
            get
            {
                return _deleteTiket ?? (_deleteTiket = new RelayCommand(x =>
                {
                    try
                    {
                        TicketList.Remove(SelectedTicket);
                        UpdateSortedTicketList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }));
            }
        }
        public ICommand DeletePlace
        {
            get
            {
                return _deletePlace ?? (_deletePlace = new RelayCommand(x =>
                {
                    try
                    {
                        Ticket ticket = TicketList.Where(pid =>pid.PlaceId==SelectedPlace.PlaceId).FirstOrDefault();
                        if (ticket == null)
                        {
                            Places.Remove(SelectedPlace);
                        }
                        else
                        {
                            MessageBox.Show($"Before delete this place delete ticket with ID={ticket.TicketId}","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }));
            }
        }
        public ICommand SaveChanges
        {
            get
            {
                return _saveChanges ?? (_saveChanges = new RelayCommand(x =>
                {
                    try
                    {
                       _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }));
            }
        }
        void UpdateSortedTicketList()
        {
            if (TicketList != null)
            {
                SortedTicketList.Clear();
                foreach (var ticket in TicketList)
                {
                    SortedTicketList.Add(ticket);
                }
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

