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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cinema_CP_WPF.ViewsModels
{
    internal class СashierViewModel : BaseViewModel
    {
        //ObservableCollection<Halls> hallslist;
        ObservableCollection<FilmSessions> _sesionlist;
        ObservableCollection<FilmSessions> _sortedsesionlist;
        ObservableCollection<Ticket> _tiketlist;
        ObservableCollection<Place> _placeList;
        CinemaContext _context;
        Halls _SelectedHalls;
        FilmSessions _SelectedSesions;
        ObservableCollection<Label> _selectedHallplaces;
        DateTime _dpDate;
        Grid _placeGridView;


        private ICommand _sellTicket;
        private ICommand _reserveName;
        private ICommand _returnTicket;
        private ICommand _sellreserveName;

        public int Place { get; set; }
        public int Row { get; set; }
        public string tbReserveName { get; set; }

        
        public СashierViewModel()
        {
            _context = new CinemaContext();
            _context.Halls.Include(f => f.FilmSessions).Load();
            _context.FilmSessions.Include(f => f.Films).Load();
            _context.Place.Include(t => t.Ticket).Include(h => h.Halls).Load();
            _context.Ticket.Load();
            PlaceGridView = new Grid();
            Hallslist = _context.Halls.Local;
            Sesionlist = _context.FilmSessions.Local;
            PlaceList = _context.Place.Local;
            Tiketlist = _context.Ticket.Local;
            SelectedHall = Hallslist.FirstOrDefault();
            Sortedsesionlist = new ObservableCollection<FilmSessions>();
            SelectedDate = DateTime.Now;
            tbReserveName = String.Empty;
        }

        void UpdatePlacesFromSQl()
        {
            _context.Place.Include(t => t.Ticket).Include(h => h.Halls).Load();
            PlaceList = _context.Place.Local;
            FillPlaces();
        }
        public bool CheckValues()
        {
            if (SelectedSesion != null)
            {
                if (Place > 0 || Row > 0)
                {
                    if (Row <= SelectedHall.HallRow && Place <= SelectedHall.HallColumn)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Row or Place");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Row or Place cant be 0");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Please choose Sesion");
                return false;
            }
        }
        public void SortSesions()
        {
            if (SelectedHall != null)
            {
                List<FilmSessions> SortedsesionlistTmp = SelectedHall.FilmSessions.Where(d => d.SessionDate.Date == SelectedDate.Date).ToList();
                if (SortedsesionlistTmp.Count > 0)
                {
                    Sortedsesionlist.Clear();
                    foreach (var sesion in SortedsesionlistTmp)
                    {
                        Sortedsesionlist.Add(sesion);
                        FillPlaces();
                    }
                }
                else
                {
                    if (Sortedsesionlist != null)
                    {
                        Sortedsesionlist.Clear();
                    }
                }
            }
            //MessageBox.Show($"Date Changed {_dpDate.ToString()}");
        }
        public void FillPlaces()
        {
            Grid tmpGrid = new Grid();
            if (SelectedHall != null)
            {
                for (int i = 0; i < SelectedHall.HallRow; i++)
                {
                    tmpGrid.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < SelectedHall.HallColumn; i++)
                {
                    tmpGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (int h = 1; h <= SelectedHall.HallRow; h++)
                {
                    for (int k = 1; k <= SelectedHall.HallColumn; k++)
                    {
                        if (SelectedSesion != null)
                        {
                            int state = 0;
                            Ticket tmpplace = SelectedSesion.Ticket.Where(r => r.Place.PlaceRow == h).Where(c => c.Place.PlaceColumn == k).FirstOrDefault();
                            if (tmpplace != null)
                            {
                                state = tmpplace.Place.PlaceState;
                            }
                            if (state == 1)
                            {
                                Label tmp = new Label()
                                {
                                    Content = $"{h}-{k}",
                                    Name = $"Label{h}{k}",
                                    Background = new SolidColorBrush(Color.FromRgb(255, 204, 0)),
                                    Margin = new Thickness(3, 3, 3, 3),
                                    Padding = new Thickness(1, 1, 1, 1),
                                    VerticalContentAlignment = VerticalAlignment.Center,
                                    HorizontalContentAlignment = HorizontalAlignment.Center
                                };
                                Grid.SetRow(tmp, h-1);
                                Grid.SetColumn(tmp, k-1);
                                tmpGrid.Children.Add(tmp);
                            }
                            else if (state == 2)
                            {
                                Label tmp = new Label()
                                {
                                    Content = $"{h}-{k}",
                                    Name = $"Label{h}{k}",
                                    Background = new SolidColorBrush(Color.FromRgb(204, 51, 0)),
                                    Margin = new Thickness(3, 3, 3, 3),
                                    Padding = new Thickness(1, 1, 1, 1),
                                    VerticalContentAlignment = VerticalAlignment.Center,
                                    HorizontalContentAlignment = HorizontalAlignment.Center
                                };
                                Grid.SetRow(tmp, h-1);
                                Grid.SetColumn(tmp, k-1);
                                tmpGrid.Children.Add(tmp);
                            }
                            else
                            {
                                Label tmp = new Label()
                                {
                                    Content = $"{h}-{k}",
                                    Name = $"Label{h}{k}",
                                    Background = new SolidColorBrush(Color.FromRgb(204, 255, 204)),
                                    Margin = new Thickness(3, 3, 3, 3),
                                    Padding = new Thickness(1, 1, 1, 1),
                                    VerticalContentAlignment = VerticalAlignment.Center,
                                    HorizontalContentAlignment = HorizontalAlignment.Center
                                };
                                Grid.SetRow(tmp, h-1);
                                Grid.SetColumn(tmp, k-1);
                                tmpGrid.Children.Add(tmp);
                            }
                        }
                        else
                        {
                            Label tmp = new Label()
                            {
                                Content = $"{h}-{k}",
                                Name = $"Label{h}{k}",
                                Background = new SolidColorBrush(Color.FromRgb(204, 255, 204)),
                                Margin = new Thickness(3, 3, 3, 3),
                                Padding = new Thickness(1, 1, 1, 1),
                                VerticalContentAlignment = VerticalAlignment.Center,
                                HorizontalContentAlignment = HorizontalAlignment.Center
                            };
                            Grid.SetRow(tmp, h-1);
                            Grid.SetColumn(tmp, k-1);
                            tmpGrid.Children.Add(tmp);
                        }
                    }
                }
                PlaceGridView = tmpGrid;
            }
            //MessageBox.Show($"Date Changed {_dpDate.ToString()}");
        }

        public ICommand SellTicket
        {
            get
            {
                return _sellTicket ?? (_sellTicket = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (CheckValues())
                            {
                                UpdatePlacesFromSQl();
                                Place placeExist = PlaceList.Where(h => h.HallId == SelectedHall.HallId).Where(r => r.PlaceRow == Row).Where(p => p.PlaceColumn == Place).FirstOrDefault();
                                if (placeExist == null)
                                {
                                    if (SelectedSesion.FreePlaces > 0)
                                    {
                                        //PlaceState 0-Free 1-Reserve 2-sell
                                        Place place = new Place() { HallId = SelectedHall.HallId, PlaceRow = Row, PlaceColumn = Place, PlaceState = 2 }; PlaceList.Add(place);
                                        SelectedSesion.FreePlaces -= 1;
                                        _context.SaveChanges();
                                        int placeId = place.PlaceId;
                                        int filmId = SelectedSesion.FilmSessionsId;
                                        decimal price = SelectedSesion.SessionPrice;
                                        Ticket ticket = new Ticket() { PlaceId = placeId, FilmSessionsId = filmId, Price = price }; Tiketlist.Add(ticket);
                                        FillPlaces();
                                        MessageBox.Show("Ticket added");
                                        foreach (var item in SelectedHall.FilmSessions)
                                        {
                                            Sortedsesionlist.Clear();
                                            Sortedsesionlist.Add(item);
                                        }
                                        _context.SaveChanges();
                                    }
                                    else
                                    {
                                        MessageBox.Show("All Ticket Sold");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ticket already Sold or Reserved. Please choose another");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                        ));
            }
        }
        public ICommand ReturnTicket
        {
            get
            {
                return _returnTicket ?? (_returnTicket = new RelayCommand(
                    x =>
                    {
                        try
                        {
                        if (CheckValues())
                        {
                            int placestate = 0;
                            UpdatePlacesFromSQl();
                            Place placeExist = PlaceList.Where(h => h.HallId == SelectedHall.HallId).Where(r => r.PlaceRow == Row).Where(p => p.PlaceColumn == Place).FirstOrDefault();
                            if (placeExist != null)
                            {
                                placestate = placeExist.PlaceState;
                            }
                            if (placestate == 1|| placestate == 2)
                                {
                                    Ticket ticket = Tiketlist.Where(t => t.PlaceId == placeExist.PlaceId).FirstOrDefault();
                                    Tiketlist.Remove(ticket);
                                    PlaceList.Remove(placeExist);
                                    SelectedSesion.FreePlaces += 1;
                                    _context.SaveChanges();
                                    FillPlaces();
                                    MessageBox.Show("Ticked Retuned");
                                }
                                else
                                {
                                    MessageBox.Show("Ticket not found. Check all values (sesion,date,row,etc...)");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                        ));
            }
        }
        public ICommand ReserveName
        {
            get
            {
                return _reserveName ?? (_reserveName = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (CheckValues())
                            {
                                int placestate = 0;
                                UpdatePlacesFromSQl();
                                Place placeExist = PlaceList.Where(h => h.HallId == SelectedHall.HallId).Where(r => r.PlaceRow == Row).Where(p => p.PlaceColumn == Place).FirstOrDefault();
                                if (placeExist != null)
                                {
                                    placestate = placeExist.PlaceState;
                                }
                                if (placeExist==null||placestate != 1 && placestate != 2)
                                {
                                    if (SelectedSesion.FreePlaces > 0)
                                    {
                                        if (tbReserveName.Length != 0)
                                        {
                                            //PlaceState 0-Free 1-Reserve 2-sell
                                            Place place = new Place() { HallId = SelectedHall.HallId, PlaceRow = Row, PlaceColumn = Place, PlaceState = 1, PlaceFIO = tbReserveName }; PlaceList.Add(place);
                                            SelectedSesion.FreePlaces += 1;
                                            _context.SaveChanges();
                                            int placeId = place.PlaceId;
                                            int filmId = SelectedSesion.FilmSessionsId;
                                            decimal price = SelectedSesion.SessionPrice;
                                            Ticket ticket = new Ticket() { PlaceId = placeId, FilmSessionsId = filmId, Price = price }; Tiketlist.Add(ticket);
                                            MessageBox.Show("Ticket reserved");
                                            FillPlaces();
                                            _context.SaveChanges();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Enter FIO for reserve");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("All Ticket Sold");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ticket sold or reserved");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                        ));
            }
        }
        public ICommand SellreserveName
        {
            get
            {
                return _sellreserveName ?? (_sellreserveName = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (CheckValues())
                            {
                                UpdatePlacesFromSQl();
                                Place placeExist = PlaceList.Where(h => h.HallId == SelectedHall.HallId).Where(r => r.PlaceRow == Row).Where(p => p.PlaceColumn == Place).Where(f => f.PlaceFIO == tbReserveName).FirstOrDefault();
                                if (placeExist != null && placeExist.PlaceState == 1)
                                {
                                    //PlaceState 0-Free 1-Reserve 2-sell
                                    var result = MessageBox.Show($"Sell ticket for {placeExist.PlaceFIO}?","Sell reserved ticket", MessageBoxButton.YesNo);
                                    if (result == MessageBoxResult.Yes)
                                    {
                                        placeExist.PlaceState = 2;
                                        _context.SaveChanges();
                                        FillPlaces();
                                        MessageBox.Show("Reserved Ticket Sold");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ticket not found. Check all values (Name for reserve,sesion,date,row,etc...)");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
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
                SortSesions();
                FillPlaces();
                RaisePropertyChanged();
            }
        }

        public FilmSessions SelectedSesion
        {
            get
            {
                return _SelectedSesions;
            }
            set
            {
                _SelectedSesions = value;
                FillPlaces();
                RaisePropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return _dpDate;
            }
            set
            {
                if (value > _dpDate)
                {
                    _dpDate = value;
                }
                else
                {
                    MessageBox.Show("Can't sell ticket to previous date");
                }
                SortSesions();
                FillPlaces();
                RaisePropertyChanged();
            }
        }

        public Grid PlaceGridView
        {
            get
            {
                return _placeGridView;
            }
            set
            {
                _placeGridView = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<FilmSessions> Sortedsesionlist
        {
            get
            {
                return _sortedsesionlist;
            }
            set
            {
                _sortedsesionlist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Halls> Hallslist { get; set; }
        public ObservableCollection<Ticket> Tiketlist { get => _tiketlist; set => _tiketlist = value; }
        public ObservableCollection<FilmSessions> Sesionlist { get => _sesionlist; set => _sesionlist = value; }
        public ObservableCollection<Place> PlaceList { get => _placeList; set => _placeList = value; }
        public DateTime DpDate { get => _dpDate; set => _dpDate = value; }
        public ObservableCollection<Label> SelectedHallplaces { get => _selectedHallplaces; set => _selectedHallplaces = value; }
    }
}
