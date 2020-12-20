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

namespace Cinema_CP_WPF.ViewsModels.AdminsViewModels
{
    public class AddSesionViewModel:BaseViewModel
    {
        ObservableCollection<FilmSessions> _sesionlist;
        ObservableCollection<FilmSessions> _sortedSesionList;
        ObservableCollection<Films> _filmlist;
        ObservableCollection<Halls> _hallist;
        bool _DateCheck;
        bool _HallCheck;
        FilmSessions _selectedSesion;
        Halls _selectedHall;
        DateTime _selectedDate;
        CinemaContext _context;

        ICommand _saveChangesSesion;
        ICommand _addnewsesion;
        ICommand _delelesesion;
        ICommand _sortsesions;

           
        public AddSesionViewModel()
        {
            _context = new CinemaContext();
            _context.FilmSessions.Include(h => h.Halls).Include(f => f.Films).Load();
            _context.Films.Include(s => s.FilmSessions).Include(g=>g.Genre).Load();
            _context.Halls.Include(s => s.FilmSessions).Load();
            SessionList = _context.FilmSessions.Local;
            Filmlist= _context.Films.Local;
            Hallslist = _context.Halls.Local;
            SortedSesionList = new ObservableCollection<FilmSessions>();
            SelectedSesion = SessionList.FirstOrDefault();
            SelectedDate = DateTime.Now;
        }
        public ObservableCollection<FilmSessions> SessionList
        {
            get { return _sesionlist; }
            set
            {
                _sesionlist = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<FilmSessions> SortedSesionList
        {
            get { return _sortedSesionList; }
            set
            {
                _sortedSesionList = value;
                RaisePropertyChanged();
            }
        }
        public bool DateCheck
        {
            get { return _DateCheck; }
            set
            {
                _DateCheck = value;
                RaisePropertyChanged();
            }
        }
        public bool HallCheck
        {
            get { return _HallCheck; }
            set
            {
                _HallCheck = value;
                RaisePropertyChanged();
            }
        }

        public FilmSessions SelectedSesion
        {
            get { return _selectedSesion; }
            set
            {
                _selectedSesion = value;
                RaisePropertyChanged();
            }
        }

        public Halls SelectedHall
        {
            get { return _selectedHall; }
            set
            {
                _selectedHall = value;
                RaisePropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Films> Filmlist
        {
            get { return _filmlist; }
            set
            {
                _filmlist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Halls> Hallslist
        {
            get { return _hallist; }
            set
            {
                _hallist = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SaveChanges
        {
            get
            {
                return _saveChangesSesion ?? (_saveChangesSesion = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            FilmSessions chkFilm = SessionList.Where(f=>f.Films==null).FirstOrDefault();
                            FilmSessions chkHall = SessionList.Where(f => f.Halls == null).FirstOrDefault();
                            if (chkFilm!=null)
                            {
                                MessageBox.Show("Check All Values. One or more sesions haven't film");
                            }
                            else if(chkHall!=null)
                            {
                                MessageBox.Show("Check All Values. One or more sesions haven't hall");
                            }
                            else
                            {
                                var result = MessageBox.Show($"Save Changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (result == MessageBoxResult.Yes)
                                {
                                    _context.SaveChanges();
                                    MessageBox.Show("Change Saved");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                    ));
            }
        }

        public ICommand AddSession
        {
            get
            {
                return _delelesesion ?? (_delelesesion = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            FilmSessions sesion = new FilmSessions()
                            {
                                FreePlaces =  1,
                                SessionDate =DateTime.Now,
                                SessionPrice= 1,                                
                            };
                            SessionList.Add(sesion);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                    ));
            }
        }

        public ICommand DeleteSession
        {
            get
            {
                return _addnewsesion ?? (_addnewsesion = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (SelectedSesion.Place.Count > 0)
                            {
                                MessageBox.Show("Sesion Have Tikets, Can't be Deleted");
                            }
                            else
                            {
                                SessionList.Remove(SelectedSesion);
                                SelectedSesion = SessionList.FirstOrDefault();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                ));
            }
        }

        public ICommand SortSessions
        {
            get
            {
                return _sortsesions ?? (_sortsesions = new RelayCommand(
                    x =>
                    {
                        try
                        {

                            if (DateCheck && HallCheck)
                            {
                                List<FilmSessions> SortedsesionlistTmp = SessionList.Where(d => d.SessionDate.Date == SelectedDate.Date).Where(h => h.HallId == SelectedHall.HallId).ToList();
                                if (SortedsesionlistTmp.Count > 0)
                                {
                                    SortedSesionList.Clear();
                                    foreach (var sesion in SortedsesionlistTmp)
                                    {                                   
                                        SortedSesionList.Add(sesion);
                                    }
                                }
                                else
                                {
                                    if (SortedSesionList != null)
                                    {
                                        SortedSesionList.Clear();
                                    }
                                }
                            }
                            else if (DateCheck)
                            {
                                List<FilmSessions> SortedsesionlistTmp = SessionList.Where(d => d.SessionDate.Date == SelectedDate.Date).ToList();
                                if (SortedsesionlistTmp.Count > 0)
                                {
                                    SortedSesionList.Clear();
                                    foreach (var sesion in SortedsesionlistTmp)
                                    {
                                        SortedSesionList.Add(sesion);
                                    }
                                }
                                else
                                {
                                    if (SortedSesionList != null)
                                    {
                                        SortedSesionList.Clear();
                                    }
                                }
                            }
                            else if (HallCheck)
                            {
                                List<FilmSessions> SortedsesionlistTmp = SessionList.Where(d => d.HallId == SelectedHall.HallId).ToList();
                                if (SortedsesionlistTmp.Count > 0)
                                {
                                    SortedSesionList.Clear();
                                    foreach (var sesion in SortedsesionlistTmp)
                                    {
                                        SortedSesionList.Add(sesion);
                                    }
                                }
                                else
                                {
                                    if (SortedSesionList != null)
                                    {
                                        SortedSesionList.Clear();
                                    }
                                }
                            }
                            else
                            {
                                SortedSesionList.Clear();
                                foreach (var Sesions in SessionList)
                                {
                                    SortedSesionList.Add(Sesions);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                ));
            }
        }


    }
}
