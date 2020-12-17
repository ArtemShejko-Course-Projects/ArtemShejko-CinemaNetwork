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
    public class EditPersonalViewModel : BaseViewModel
    {
        ObservableCollection<CinemaStaff> _employeelist;
        ObservableCollection<CinemaStaff> _sortedemployeelist;
        CinemaStaff _selectedStuff;
        CinemaContext _cinemaContext;

        public EditPersonalViewModel()
        {
            _cinemaContext = new CinemaContext();
            SortedEmployeeList = new ObservableCollection<CinemaStaff>();
            _cinemaContext.CinemaStaff.Load();
            EmployeeList = _cinemaContext.CinemaStaff.Local;
            SortList();
        }

        ICommand _addEmp;
        ICommand _deleteEmp;
        ICommand _saveChange;


        void SortList()
        {
            List<CinemaStaff> tmpuserList = EmployeeList.OrderBy(l => l.CinemaStaffLogin).ToList();
            if (tmpuserList != null)
            {
                SortedEmployeeList.Clear();
            }
            foreach (var user in tmpuserList)
            {
                SortedEmployeeList.Add(user);
            }
        }

        public ObservableCollection<CinemaStaff> SortedEmployeeList
        {
            get { return _sortedemployeelist; }
            set
            {
                _sortedemployeelist = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<CinemaStaff> EmployeeList
        {
            get { return _employeelist; }
            set
            {
                _employeelist = value;
                RaisePropertyChanged();
            }
        }
        public CinemaStaff SelectedEmployee
        {
            get
            {
                return _selectedStuff;
            }
            set
            {
                _selectedStuff = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddEmployee
        {
            get
            {
                return _addEmp ?? (_addEmp = new RelayCommand(x =>
                {
                    try
                    {
                        Random rand = new Random();
                        int l = rand.Next(1, 10000) + EmployeeList.Count;
                        string TmpUserLogin = $"NewemployeeLogin_{l} ";
                        CinemaStaff cemp = new CinemaStaff()
                        {
                            CinemaStaffLogin = TmpUserLogin,
                            CinemaStaffPass = TmpUserLogin,
                            CinemaStaffPost = "Enter Post",
                            CinemaStaffrName = "Enter Name",
                            CinemaStaffPhone = "Enter Phone",
                            CinemaStaffEmail = "Enter Email",                 
                        };
                        EmployeeList.Add(cemp);
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

        public ICommand DeleteEmployee
        {
            get
            {
                return _deleteEmp ?? (_deleteEmp = new RelayCommand(x =>
                {
                    try
                    {
                        EmployeeList.Remove(SelectedEmployee);
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
                        var result = MessageBox.Show("Save changes?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
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




