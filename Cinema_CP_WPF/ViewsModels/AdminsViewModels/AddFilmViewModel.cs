using CinemaDAL;
using Microsoft.Win32;
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
    public class AddFilmViewModel : BaseViewModel
    {
        CinemaContext _context;

        ObservableCollection<Films> _Films;
        ObservableCollection<Genre> _Genre;


        ICommand _addImage;

        ICommand _saveChanges;

        ICommand _addNewFilm;

        Films _selectedFilm;
        public AddFilmViewModel()
        {
            _context = new CinemaContext();
            _context.Films.Include(g=>g.Genre).Load();
            _context.Genre.Load();
            Films = _context.Films.Local;
            Genre = _context.Genre.Local;
            SelectedFilm = Films.FirstOrDefault();
        }

        public Films SelectedFilm
        {
            get { return _selectedFilm; }
            set
            {
                _selectedFilm = value;
                RaisePropertyChanged();
            }

        }

        public ObservableCollection<Films> Films {
            get
            {
                return _Films;
            }
            set
            {
                _Films = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Genre> Genre
        {
            get
            {
                return _Genre;
            }
            set
            {
                _Genre = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddImage
        {
            get
            {
                return _addImage ?? (_addImage = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            OpenFileDialog fileDialog = new OpenFileDialog();
                            fileDialog.ShowDialog();
                            SelectedFilm.FilmImageUri = fileDialog.FileName;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                    ));
            }
        }

        public ICommand AddNewFilm
        {
            get
            {
                return _addNewFilm ?? (_addNewFilm = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            Films film = new Films()
                            {
                                FilmName = "Enter",
                                FilmGenre = 1,
                                FilmDuration = 0,
                                FilmStartDate = DateTime.Now,
                                FilmEndDate = DateTime.Now,
                                FilmActors = "Enter",
                                FilmDescription = "Enter",
                                FilmImageUri = "Enter"
                            };
                            Films.Add(film);
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
                return _saveChanges ?? (_saveChanges = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            _context.SaveChanges();
                            MessageBox.Show("Chage Saved");
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
