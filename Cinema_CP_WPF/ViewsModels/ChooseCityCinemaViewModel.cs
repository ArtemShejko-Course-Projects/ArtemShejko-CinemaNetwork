using Cinema_CP_WPF.Views;
using Cinema_CP_WPF.ViewsModels.AdminsViewModels;
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

namespace Cinema_CP_WPF.ViewsModels
{
    public class ChooseCityCinemaViewModel : BaseViewModel
    {
        City _selectedcity;
        СinemaDetails _selectedcinema;
        ObservableCollection<City> _citylist;
        ObservableCollection<СinemaDetails> _cdlist;
        ObservableCollection<СinemaDetails> _sortedcdlist;
        CinemaContext _context;
        public string ViewRole { get; set; }
        public string ViewLogin { get; set; }


        ICommand _openView;
        public ChooseCityCinemaViewModel()
        {
            _context = new CinemaContext();
            SortedCinemaDetailsList = new ObservableCollection<СinemaDetails>();
            _context.City.Load();
            _context.СinemaDetails.Include(c => c.City).Include(h=>h.Halls).Load();
            CityList = _context.City.Local;
            CinemaDetailsList = _context.СinemaDetails.Local;
        }
        public ChooseCityCinemaViewModel(string role)
        {
            _context = new CinemaContext();
            SortedCinemaDetailsList = new ObservableCollection<СinemaDetails>();
            _context.City.Load();
            _context.СinemaDetails.Include(c => c.City).Load();
            CityList = _context.City.Local;
            ViewRole = role;
            CinemaDetailsList = _context.СinemaDetails.Local;
        }
        public ObservableCollection<СinemaDetails> CinemaDetailsList
        {
            get { return _cdlist; }
            set
            {
                _cdlist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<СinemaDetails> SortedCinemaDetailsList
        {
            get { return _sortedcdlist; }
            set
            {
                _sortedcdlist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<City> CityList
        {
            get { return _citylist; }
            set
            {
                _citylist = value;
                RaisePropertyChanged();
            }
        }
        public City SelectedCity
        {
            get { return _selectedcity; }
            set
            {
                _selectedcity = value;
                SortCinema();
                RaisePropertyChanged();
            }
        }
        public СinemaDetails SelectedCinema
        {
            get { return _selectedcinema; }
            set
            {
                _selectedcinema = value;
                RaisePropertyChanged();
            }
        }

        void SortCinema()
        {
            var tmp = _context.СinemaDetails.Local.Where(p => p.City == SelectedCity);
            if (tmp != null)
            {
                if (SortedCinemaDetailsList != null)
                {
                    SortedCinemaDetailsList.Clear();
                }
                foreach (var сinema in tmp)
                {
                    SortedCinemaDetailsList.Add(сinema);
                }
            }
        }

        public ICommand OpenView
        {
            get
            {
                return _openView ?? (_openView = new RelayCommand(x =>
                {
                    try
                    {
                        if (ViewRole == "Cashier")
                        {
                            СashierView ccv = new СashierView() { DataContext = new СashierViewModel(SelectedCinema.СinemaDetailsId) }; ccv.Show();
                        }
                        else if (ViewRole == "Administrator")
                        {
                            AdminView av = new AdminView() { DataContext = new AdminViewModel() }; av.Show();
                        }
                        else if (ViewRole == "User")
                        {
                            UserView uv = new UserView() { DataContext = new UserViewModel(SelectedCinema.СinemaDetailsId) { tbReserveName = ViewLogin } }; uv.Show();
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

    }
}
