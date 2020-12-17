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
    public class EditUsersViewModel : BaseViewModel
    {
        ObservableCollection<CinemaUser> _userlist;
        ObservableCollection<CinemaUser> _sortedUserList;
        ObservableCollection<Ticket> _ticketList;
        CinemaUser _selectedUser;
        CinemaContext _cinemaContext;

        public EditUsersViewModel()
        {
            _cinemaContext = new CinemaContext();
            SortedUserList = new ObservableCollection<CinemaUser>();
            _cinemaContext.CinemaUser.Include(t => t.Ticket).Load();
            _cinemaContext.Ticket.Include(p => p.Place).Load();
            UserList = _cinemaContext.CinemaUser.Local;
            SortList();
            SelectedUser = SortedUserList.FirstOrDefault();
        }

        ICommand _addUser;
        ICommand _deleteUser;
        ICommand _saveChange;


        void SortList()
        {
            if (UserList != null)
            {
                SortedUserList.Clear();
                var tmpuserList = UserList.OrderBy(l => l.CinemaUserLogin);
                foreach (var user in tmpuserList)
                {
                    SortedUserList.Add(user);
                }
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
                            CinemaUserPass = TmpUserLogin
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

        public ICommand DeleteUser
        {
            get
            {
                return _deleteUser ?? (_deleteUser = new RelayCommand(x =>
                {
                    try
                    {
                        Random rand = new Random();
                        int l = rand.Next(1, 10000) + UserList.Count;
                        string TmpUserLogin = $"NewLogin_{l} ";
                        CinemaUser cuser = new CinemaUser()
                        {
                            CinemaUserLogin = TmpUserLogin,
                            CinemaUserPass = TmpUserLogin
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
    }
}
