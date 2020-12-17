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
    public class EditPersonalViewModel : BaseViewModel
    {
        ObservableCollection<CinemaStaff> _employeelist;
        ObservableCollection<CinemaStaff> _sortedemployeelist;
        CinemaStaff _selectedStuff;
        CinemaContext _cinemaContext;
        string _EmployeePhoto;
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
        ICommand _addPhotoEmp;


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

        public string EmployeePhoto
        {
            get { return _EmployeePhoto; }
            set
            {
                _EmployeePhoto = value;
                RaisePropertyChanged();
            }
        }

        public void UpdateEmployeePhoto()
        {
            string ImageUri = String.Empty;
            if (SelectedEmployee != null)
            {
                DirectoryInfo dir = new DirectoryInfo(".");
                EmployeePhoto = $"{dir.FullName}\\{SelectedEmployee.CinemaStaffPhoto}";
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
                UpdateEmployeePhoto();
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
                            CinemaStaffName = "Enter Name",
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
                        if (SelectedEmployee != null)
                        {
                            EmployeeList.Remove(SelectedEmployee);
                            SortList();
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

        public ICommand AddPhotoEmp
        {
            get
            {
                return _addPhotoEmp ?? (_addPhotoEmp = new RelayCommand(
                    x =>
                    {
                        try
                        {
                            if (SelectedEmployee != null)
                            {
                                OpenFileDialog fileDialog = new OpenFileDialog();
                                fileDialog.ShowDialog();
                                DirectoryInfo dirInfo = new DirectoryInfo("EmployeesPhoto");
                                if (!dirInfo.Exists)
                                {
                                    dirInfo.Create();
                                }
                                string destfilename = $"EmployeesPhoto\\{SelectedEmployee.CinemaStaffId}{SelectedEmployee.CinemaStaffLogin}{fileDialog.SafeFileName}";
                                File.Copy(fileDialog.FileName, destfilename, true);
                                SelectedEmployee.CinemaStaffPhoto = destfilename;
                                UpdateEmployeePhoto();
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
    }
}




