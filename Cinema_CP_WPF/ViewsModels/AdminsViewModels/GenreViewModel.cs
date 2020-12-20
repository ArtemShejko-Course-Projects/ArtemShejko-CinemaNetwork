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
    public class GenreViewModel : BaseViewModel
    {
        ObservableCollection<Genre> _genrelist;

        ObservableCollection<Films> _filmslist;

        ObservableCollection<Genre> _viewgenreList;

        CinemaContext _context;
        Genre _selectedGenre;

        ICommand _addGenre;
        ICommand _deleteGenre;
        ICommand _saveChanges;

        public GenreViewModel()
        {
            _context = new CinemaContext();
            _context.Genre.Include(f=>f.Films).Load();
            _context.Films.Include(g => g.Genre).Load();
            GenreList= _context.Genre.Local;
            SelectedGenre = GenreList.FirstOrDefault();
            Filmslist = _context.Films.Local;
        }

        public GenreViewModel(ObservableCollection<Genre> viewgenreList)
        {
            _context = new CinemaContext();
            _context.Genre.Include(f => f.Films).Load();
            GenreList = _context.Genre.Local;
            _viewgenreList = viewgenreList;
            SelectedGenre = GenreList.FirstOrDefault();
        }
        public ObservableCollection<Genre> GenreList
        {
            get { return _genrelist; }
            set 
            {
                _genrelist = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Films> Filmslist
        {
            get { return _filmslist; }
            set
            {
                _filmslist = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Genre> ViewGenreList
        {
            get { return _viewgenreList; }
            set
            {
                _viewgenreList = value;
                RaisePropertyChanged();
            }
        }

        public Genre SelectedGenre
        {
            get
            {
                return _selectedGenre;
            }
            set
            {
                _selectedGenre = value;
                RaisePropertyChanged();
            }
        }


        public ICommand AddNewGenre
        {
            get
            {
                return _addGenre ?? (_addGenre = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            Genre genre = new Genre()
                            {
                                GenreTitle = "Enter Genre Title",                   
                            };
                            GenreList.Add(genre);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                    ));
            }
        }
        public ICommand DeleteGenre
        {
            get
            {
                return _deleteGenre ?? (_deleteGenre = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            Films tmpGenreUsed = Filmslist.Where(g=>g.Genre.GenreId==SelectedGenre.GenreId).FirstOrDefault();
                            if (tmpGenreUsed != null)
                            {
                                MessageBox.Show("Can't delete Genre. Delete all films with this genre or change genre on films");
                            }
                            else
                            {                     
                                GenreList.Remove(SelectedGenre);
                                SelectedGenre = GenreList.FirstOrDefault();
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
                return _saveChanges ?? (_saveChanges = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            var result = MessageBox.Show($"Save Changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                _context.SaveChanges();
                                ViewGenreList = _context.Genre.Local;
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
