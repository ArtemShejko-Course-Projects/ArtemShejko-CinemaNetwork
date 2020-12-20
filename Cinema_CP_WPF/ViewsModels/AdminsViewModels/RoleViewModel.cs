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
    public class RoleViewModel:BaseViewModel
    {
        ObservableCollection<RoleTable> _sortedRoleList;
        ObservableCollection<RoleTable> _roleList;
        ObservableCollection<CinemaUser> _cinemaUser;
        ObservableCollection<CinemaStaff> _cinemaStuff;

        CinemaContext _context;
        RoleTable _selectedRole;
        public RoleViewModel()
        {
            _context = new CinemaContext();
            SortedRoleList = new ObservableCollection<RoleTable>();
            _context.RoleTable.Load();
            _context.CinemaUser.Load();
            _context.CinemaStaff.Load();
            RoleList = _context.RoleTable.Local;
            _cinemaUser = _context.CinemaUser.Local;
            _cinemaStuff = _context.CinemaStaff.Local;
            SortList();
            SelectedRole = SortedRoleList.FirstOrDefault();
        }
        public ObservableCollection<RoleTable> SortedRoleList
        {
            get { return _sortedRoleList; }
            set
            {
                _sortedRoleList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<RoleTable> RoleList
        {
            get { return _roleList; }
            set
            {
                _roleList = value;
                RaisePropertyChanged();
            }
        }

        public RoleTable SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                RaisePropertyChanged();
            }
        }

        ICommand _addRole;
        ICommand _deleteRole;
        ICommand _saveChange;


        void SortList()
        {
            if (RoleList != null)
            {
                List<RoleTable> tmpcitylist = RoleList.OrderBy(l => l.RoleTitle).ToList();
                if (tmpcitylist != null)
                {
                    SortedRoleList.Clear();
                }
                foreach (var city in tmpcitylist)
                {
                    SortedRoleList.Add(city);
                }
            }
        }


        public ICommand AddRole
        {
            get
            {
                return _addRole ?? (_addRole = new RelayCommand(x =>
                {
                    try
                    {
                        Random rand = new Random();
                        int l = rand.Next(1, 10000) + RoleList.Count;
                        string TmpRole = $"NewRole_{l} ";
                        RoleTable role = new RoleTable()
                        {
                            RoleTitle = TmpRole
                        };
                        RoleList.Add(role);
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

        public ICommand DeleteRole
        {
            get
            {
                return _deleteRole ?? (_deleteRole = new RelayCommand(x =>
                {
                    try
                    {
                        CinemaUser cu = _cinemaUser.Where(cdt => cdt.RoleTable.RoleId == SelectedRole.RoleId).FirstOrDefault();
                        CinemaStaff cf = _cinemaStuff.Where(cdt => cdt.RoleTable.RoleId == SelectedRole.RoleId).FirstOrDefault();
                        if (cu == null&&cf==null)
                        {
                            RoleList.Remove(SelectedRole);
                            SortList();
                        }
                        else
                        {
                            MessageBox.Show("Can't delete this Role. Delete all users with this role and try again");
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

