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
    public class EditUsersViewModel : BaseViewModel
    {
        ObservableCollection<CinemaUser> _userlist;
        ObservableCollection<CinemaUser> _sortedUserList;
        ObservableCollection<RoleTable> _roleTable;
        ObservableCollection<Ticket> _ticketList;
        CinemaUser _selectedUser;
        CinemaContext _cinemaContext;
        string _userPhoto;

        public EditUsersViewModel()
        {
            _cinemaContext = new CinemaContext();
            SortedUserList = new ObservableCollection<CinemaUser>();
            _cinemaContext.CinemaUser.Include(t => t.Ticket).Include(r=>r.RoleTable).Load();
            _cinemaContext.Ticket.Include(p => p.Place).Include(h=>h.Place.Halls).Include(fm=>fm.Place.FilmSessions).Include(f=>f.Place.FilmSessions.Films).Load();
            _cinemaContext.RoleTable.Load();
            UserList = _cinemaContext.CinemaUser.Local;
            TicketList = _cinemaContext.Ticket.Local;
            RoleList = _cinemaContext.RoleTable.Local;
            SortList();
        }

        ICommand _addUser;
        ICommand _deleteUser;
        ICommand _saveChange;
        ICommand _addPhotoUser;


        void SortList()
        {
            List<CinemaUser> tmpuserList = UserList.OrderBy(l => l.CinemaUserLogin).ToList();
            if (tmpuserList != null)
            {
                SortedUserList.Clear();    
            }
            foreach (var user in tmpuserList)
            {
                SortedUserList.Add(user);
            }
        }

        public ObservableCollection<CinemaUser> SortedUserList
        {
            get { return _sortedUserList; }
            set
            {
                _sortedUserList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<CinemaUser> UserList
        {
            get { return _userlist; }
            set
            {
                _userlist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Ticket> TicketList
        {
            get { return _ticketList; }
            set
            {
                _ticketList = value;
                RaisePropertyChanged();
            }
        }
        public CinemaUser SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                UpdateUserPhoto();
                RaisePropertyChanged();
            }
        }
        public string UserPhoto
        {
            get { return _userPhoto; }
            set
            {
                _userPhoto = value;
                RaisePropertyChanged();
            }
        }
        public void UpdateUserPhoto()
        {
            if (SelectedUser != null)
            {
                DirectoryInfo dir = new DirectoryInfo(".");
                UserPhoto = $"{dir.FullName}\\{SelectedUser.CinemaUserPhoto}";
            }

        }

        public ObservableCollection<RoleTable> RoleList
        {
            get { return _roleTable; }
            set
            {
                _roleTable = value;
                RaisePropertyChanged();
            }
        }


        public ICommand AddUser
        {
            get
            {
                return _addUser ?? (_addUser = new RelayCommand(x =>
                {
                    try
                    {
                        Random rand = new Random();
                        int l = rand.Next(1, 10000) + UserList.Count;
                        string TmpUserLogin = $"NewLogin_{l} ";
                        CinemaUser cuser = new CinemaUser()
                        {
                            CinemaUserLogin = TmpUserLogin,
                            CinemaUserPass = TmpUserLogin,                          
                        };
                        UserList.Add(cuser);
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
        public ICommand AddPhotoUser
        {
            get
            {
                return _addPhotoUser ?? (_addPhotoUser = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (SelectedUser != null)
                            {
                                OpenFileDialog fileDialog = new OpenFileDialog();
                                fileDialog.ShowDialog();
                                DirectoryInfo dirInfo = new DirectoryInfo("UsersPhoto");
                                if (!dirInfo.Exists)
                                {
                                    dirInfo.Create();
                                }
                                string destfilename = $"UsersPhoto\\{SelectedUser.CinemaUserId}{SelectedUser.CinemaUserLogin}{fileDialog.SafeFileName}";
                                File.Copy(fileDialog.FileName, destfilename, true);
                                SelectedUser.CinemaUserPhoto = destfilename;
                                UpdateUserPhoto();
                            }
                            else
                            {
                                MessageBox.Show("Please choose Employee");
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
        public ICommand DeleteUser
        {
            get
            {
                return _deleteUser ?? (_deleteUser = new RelayCommand(x =>
                {
                    try
                    {
                        UserList.Remove(SelectedUser);
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

        public ICommand SaveChanges
        {
            get
            {
                return _saveChange ?? (_saveChange = new RelayCommand(x =>
                {
                    try
                    {
                        var result=MessageBox.Show("Save changes?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result==MessageBoxResult.Yes)
                        {
                            _cinemaContext.SaveChanges();
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
