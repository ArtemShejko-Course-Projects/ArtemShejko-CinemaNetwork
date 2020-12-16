﻿using CinemaDAL;
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
        ObservableCollection<FilmSessions> _filmSesions;
        ObservableCollection<Films> _SortedFilms;



        ICommand _addImage;
        ICommand _saveChanges;
        ICommand _addNewFilm;
        ICommand _deleteFilm;


        Films _selectedFilm;
        public AddFilmViewModel()
        {
            _context = new CinemaContext();
            _context.Films.Include(g => g.Genre).Load();
            _context.Genre.Load();
            _context.FilmSessions.Include(f => f.Films).Load();
            SortedFilmsListFVM = new ObservableCollection<Films>();
            FilmsListFVM = _context.Films.Local;
            FilmSesionsList = _context.FilmSessions.Local;
            GenreList = _context.Genre.Local;
            SortFilms();
            SelectedFilm = SortedFilmsListFVM.FirstOrDefault();
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

        void SortFilms()
        {
            List<Films> tmpfilms = FilmsListFVM.OrderBy(fn => fn.FilmName).ToList();
            if (tmpfilms != null)
            {
                SortedFilmsListFVM.Clear();
            }
            foreach (var Film in tmpfilms)
            {
                SortedFilmsListFVM.Add(Film);
            }        
        }
        public ObservableCollection<Films> FilmsListFVM {
            get
            {
                return _Films;
            }
            set
            {
                _Films = value;
                SortFilms();
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Films> SortedFilmsListFVM
        {
            get
            {
                return _SortedFilms;
            }
            set
            {
                _SortedFilms = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Genre> GenreList
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


        public ObservableCollection<FilmSessions> FilmSesionsList
        {
            get
            {
                return _filmSesions;
            }
            set
            {
                _filmSesions = value;
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
                            SortFilms();
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
                                FilmDuration = 110,
                                FilmStartDate = DateTime.Now,
                                FilmEndDate = DateTime.Now,
                                FilmActors = "Enter",
                                FilmDescription = "Enter",
                                FilmImageUri = "Enter"
                            };
                            FilmsListFVM.Add(film);
                            SortFilms();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }
                    }

                    ));
            }
        }

        public ICommand DeleteFilm
        {
            get
            {
                return _deleteFilm ?? (_deleteFilm = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            FilmSessions filmSesionstmp = FilmSesionsList.Where(f => f.FilmId == SelectedFilm.FilmId).FirstOrDefault();
                            if(filmSesionstmp!=null)
                            {
                                MessageBox.Show("Can't delete Film. Delete all sesions with this film before delete film");
                            }
                            else 
                            {
                                //int filmId = SelectedFilm.FilmId;
                                //_context.Database.ExecuteSqlCommand($"delete from Films where FilmId={filmId}");
                                 FilmsListFVM.Remove(SelectedFilm);
                                //_context.Films.Include(g => g.Genre).Load();
                                //FilmsListFVM = _context.Films.Local;
                                SelectedFilm = FilmsListFVM.FirstOrDefault();
                                SortFilms();
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
                                SortFilms();
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
