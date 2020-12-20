using Cinema_CP_WPF.Views.AdminViews;
using CinemaDAL;
using Microsoft.Win32;
using MVVMHelper.Commands;
using MVVMHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels.AdminsViewModels
{
    public class CinemaListViewModel : BaseViewModel
    {
        ObservableCollection<СinemaDetails> _cinemaDetailsList;
        ObservableCollection<СinemaDetails> _sortedСinemaDetailsList;
        ObservableCollection<City> _cityList;
        ObservableCollection<Halls> _hallsList;
        CinemaContext _context;
        СinemaDetails _сinemaDetails;
        string _cinemaPhoto;
        public CinemaListViewModel()
        {
            _context = new CinemaContext();
            SortedСinemaDetailsList = new ObservableCollection<СinemaDetails>();
            _context.СinemaDetails.Include(c => c.City).Load();
            _context.Halls.Load();
            _context.City.Load();
            HallList = _context.Halls.Local;
            СinemaDetailsList = _context.СinemaDetails.Local;
            CityList = _context.City.Local;
            SortList();
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
        public ObservableCollection<СinemaDetails> SortedСinemaDetailsList
        {
            get { return _sortedСinemaDetailsList; }
            set
            {
                _sortedСinemaDetailsList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Halls> HallList
        {
            get { return _hallsList; }
            set 
            {
                _hallsList = value;
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
        public СinemaDetails SelectedCinema
        {
            get { return _сinemaDetails; }
            set
            {
                _сinemaDetails = value;
                UpdateCinemaPhoto();
                RaisePropertyChanged();
            }
        }

        ICommand _addCinema;
        ICommand _deleteCinema;
        ICommand _saveChange;
        ICommand _addPhotoCinema;
        ICommand _opCityView;
        ICommand _updateCityList;


        void SortList()
        {
            List<СinemaDetails> tmpuserList = СinemaDetailsList.OrderBy(l => l.СinemaDetailsTitle).ToList();
            if (tmpuserList != null)
            {
                SortedСinemaDetailsList.Clear();
            }
            foreach (var cinema in tmpuserList)
            {
                SortedСinemaDetailsList.Add(cinema);
            }
        }
        public string CinemaPhoto
        {
            get { return _cinemaPhoto; }
            set
            {
                _cinemaPhoto = value;
                RaisePropertyChanged();
            }
        }
        public void UpdateCinemaPhoto()
        {
            if (SelectedCinema != null)
            {
                DirectoryInfo dir = new DirectoryInfo(".");
                CinemaPhoto = $"{dir.FullName}\\{SelectedCinema.СinemaDetailsPicture}";
            }

        }
        public ICommand AddCinema
        {
            get
            {
                return _addCinema ?? (_addCinema = new RelayCommand(x =>
                {
                    try
                    {
                        Random rand = new Random();
                        int l = rand.Next(1, 10000) + СinemaDetailsList.Count;
                        string TmpCinema = $"NewCinema_{l} ";
                        СinemaDetails cinema = new СinemaDetails()
                        {
                            СinemaDetailsTitle="Enter Cinema Title",
                            СinemaDetailsAddress="Enter Cinema address",
                            СinemaDetailsPhone="Enter Cinema Phone",
                        };
                        СinemaDetailsList.Add(cinema);
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
        public ICommand OpCityView
        {
            get
            {
                return _opCityView ?? (_opCityView = new RelayCommand(x =>
                {
                   CityView cvv = new CityView() { DataContext = new CityViewModel() }; cvv.Show(); 
                }));          
            }
        }
        public ICommand AddPhotoCinema
        {
            get
            {
                return _addPhotoCinema ?? (_addPhotoCinema = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (SelectedCinema != null)
                            {
                                OpenFileDialog fileDialog = new OpenFileDialog();
                                fileDialog.ShowDialog();
                                DirectoryInfo dirInfo = new DirectoryInfo("CinemaPhoto");
                                if (!dirInfo.Exists)
                                {
                                    dirInfo.Create();
                                }
                                string destfilename = $"CinemaPhoto\\{SelectedCinema.СinemaDetailsId}{SelectedCinema.СinemaDetailsTitle}{fileDialog.SafeFileName}";
                                File.Copy(fileDialog.FileName, destfilename, true);
                                SelectedCinema.СinemaDetailsPicture = destfilename;
                                UpdateCinemaPhoto();
                            }
                            else
                            {
                                MessageBox.Show("Please choose Cinema");
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
        public ICommand DeleteCinema
        {
            get
            {
                return _deleteCinema ?? (_deleteCinema = new RelayCommand(x =>
                {
                    try
                    {
                        Halls hall = HallList.Where(cd => cd.СinemaDetailsId == SelectedCinema.СinemaDetailsId).FirstOrDefault();
                        if (hall == null)
                        {
                            СinemaDetailsList.Remove(SelectedCinema);
                            SortList();
                        }
                        else
                        {
                            MessageBox.Show("Can't delete this Cinema. Delete all hall in this Cinema and try again");
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
                        СinemaDetails tmp = СinemaDetailsList.Where(p => p.City == null).FirstOrDefault();
                        var result = MessageBox.Show("Save changes?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            if (tmp == null)
                            {
                                _context.SaveChanges();
                            }
                            else
                            {
                                MessageBox.Show($"Cinema {tmp.СinemaDetailsTitle} hasn't City. Set City before save");
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
        public ICommand UpdateCityList
        {
            get
            {
                return _updateCityList ?? (_updateCityList = new RelayCommand(x =>
                {
                    try
                    {
                        _context.City.Load();
                        CityList = _context.City.Local;
                        SortList();
                        SelectedCinema = СinemaDetailsList.FirstOrDefault();
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
