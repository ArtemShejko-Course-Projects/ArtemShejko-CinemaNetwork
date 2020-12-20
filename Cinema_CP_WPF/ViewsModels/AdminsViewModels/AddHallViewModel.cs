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

namespace Cinema_CP_WPF.ViewsModels.AdminsViewModels
{
    public class AddHallViewModel : BaseViewModel
    {
        ObservableCollection<Halls> _hallsList;
        ObservableCollection<FilmSessions> _filmsesionList;
        ObservableCollection<СinemaDetails> _cinemaList;

        CinemaContext _context;
        Halls _selectedHall;
        Grid _placeGridViewHall;

        ICommand _addHall;
        ICommand _deletehall;
        ICommand _saveChangesHall;
        public AddHallViewModel()
        {
            _context = new CinemaContext();
            _context.Halls.Load();
            _context.FilmSessions.Load();
            _context.СinemaDetails.Load();
            Hallslist = _context.Halls.Local;
            FilmsesionList = _context.FilmSessions.Local;
            CinemaList = _context.СinemaDetails.Local;
            SelectedHall = _hallsList.FirstOrDefault();
            FillPlaces();
        }

        public Halls SelectedHall
        {
            get { return _selectedHall; }
            set
            {
                _selectedHall = value;
                FillPlaces();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Halls> Hallslist
        {
            get { return _hallsList; }
            set
            {
                _hallsList = value;
                FillPlaces();
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<СinemaDetails> CinemaList
        {
            get { return _cinemaList; }
            set
            {
                _cinemaList = value;
                FillPlaces();
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<FilmSessions> FilmsesionList
        {
            get { return _filmsesionList; }
            set
            {
                _filmsesionList = value;
                FillPlaces();
                RaisePropertyChanged();
            }
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

                for (int h = 0; h < SelectedHall.HallRow; h++)
                {
                    for (int k = 0; k < SelectedHall.HallColumn; k++)
                    {
                        Label tmp = new Label()
                        {
                            Content = $"{h + 1}-{k + 1}",
                            Name = $"Label{h}{k}",
                            Background = new SolidColorBrush(Color.FromRgb(204, 255, 204)),
                            Margin = new Thickness(3, 3, 3, 3),
                            Padding = new Thickness(1, 1, 1, 1),
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalContentAlignment = HorizontalAlignment.Center
                        };
                        Grid.SetRow(tmp, h);
                        Grid.SetColumn(tmp, k);
                        tmpGrid.Children.Add(tmp);
                    }
                }
                PlaceGridViewHall = tmpGrid;
            }
        }

        public Grid PlaceGridViewHall
        {
            get
            {
                return _placeGridViewHall;
            }
            set
            {
                _placeGridViewHall = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddHall
        {
            get
            {
                return _addHall ?? (_addHall = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            Halls hall = new Halls()
                            {
                                 HallName="Enter Value",
                                 HallPlaceQuantity=0,                             
                                 HallColumn=0,
                                 HallRow=0,                                 
                            };
                            Hallslist.Add(hall);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                    ));
            }
        }

        public ICommand DeleteHall
        {
            get
            {
                return _deletehall ?? (_deletehall = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            FilmSessions tmpfilmsession = FilmsesionList.Where(h => h.HallId == SelectedHall.HallId).FirstOrDefault();
                            if (tmpfilmsession!=null)
                            {
                                MessageBox.Show("Can't delete Hall becouse it used in sesions");
                            }
                            else
                            {
                                Hallslist.Remove(SelectedHall);
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
        public ICommand SaveChanges
        {
            get
            {
                return _saveChangesHall ?? (_saveChangesHall = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            var result = MessageBox.Show($"Save Changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                _context.SaveChanges();
                                MessageBox.Show("Change Saved");
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
