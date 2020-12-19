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
    public class CityViewModel:BaseViewModel
    {
        ObservableCollection<СinemaDetails> _cinemaDetailsList;
        ObservableCollection<City> _cityList;
        ObservableCollection<City> _sortedcityList;
        CinemaContext _context;
        City _selectedCity;
        public CityViewModel()
        {
            _context = new CinemaContext();
            SortedCityList = new ObservableCollection<City>();
            _context.City.Load();
            _context.СinemaDetails.Load();
            CityList = _context.City.Local;
            СinemaDetailsList = _context.СinemaDetails.Local;
            SortList();
            SelectedCity = CityList.FirstOrDefault();
        }
        public ObservableCollection<СinemaDetails> СinemaDetailsList
        {
            get { return _cinemaDetailsList; }
            set
            {
                _cinemaDetailsList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<City> CityList
        {
            get { return _cityList; }
            set
            {
                _cityList = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<City> SortedCityList
        {
            get { return _sortedcityList; }
            set
            {
                _sortedcityList = value;
                RaisePropertyChanged();
            }
        }
        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                RaisePropertyChanged();
            }
        }

        ICommand _addCity;
        ICommand _deleteCity;
        ICommand _saveChange;
     

        void SortList()
        {
            if (CityList != null)
            {
                List<City> tmpcitylist = CityList.OrderBy(l => l.CityTitle).ToList();
                if (tmpcitylist != null)
                {
                    SortedCityList.Clear();
                }
                foreach (var city in tmpcitylist)
                {
                    SortedCityList.Add(city);
                }
            }
        }

  
        public ICommand AddCity
        {
            get
            {
                return _addCity ?? (_addCity = new RelayCommand(x =>
                {
                    try
                    {
                        Random rand = new Random();
                        int l = rand.Next(1, 10000) + CityList.Count;
                        string TmpCity = $"NewCity_{l} ";
                        City city = new City()
                        {
                            CityTitle = TmpCity
                        };
                        CityList.Add(city);
                        SortList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
               ));
            }
        }

        public ICommand DeleteCity
        {
            get
            {
                return _deleteCity ?? (_deleteCity = new RelayCommand(x =>
                {
                    try
                    {
                        СinemaDetails cd = СinemaDetailsList.Where(cdt => cdt.СinemaDetailsCity == SelectedCity.GityId).FirstOrDefault();
                        if (cd == null)
                        {
                            CityList.Remove(SelectedCity);
                            SortList();
                        }
                        else
                        {
                            MessageBox.Show("Can't delete this City. Delete all cinema with this city and try again");
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
        public ICommand SaveChanges
        {
            get
            {
                return _saveChange ?? (_saveChange = new RelayCommand(x =>
                {
                    try
                    {
                        var result = MessageBox.Show("Save changes?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {                      
                            _context.SaveChanges();                   
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
