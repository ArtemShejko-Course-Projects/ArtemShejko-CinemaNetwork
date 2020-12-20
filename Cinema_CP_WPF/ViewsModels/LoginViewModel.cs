using Cinema_CP_WPF.Views;
using Cinema_CP_WPF.ViewsModels.AdminsViewModels;
using CinemaDAL;
using MVVMHelper.Commands;
using MVVMHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels
{
    public class LoginViewModel:BaseViewModel
    {
        List<String> _Roles;
        ObservableCollection<CinemaUser> _users;
        ObservableCollection<CinemaStaff> _cinemaStaff;
        CinemaContext _context;
        ICommand _CheckUser;
        public LoginViewModel()
        {
            _context = new CinemaContext();
            Roles = new List<string>(); 
            _context.CinemaUser.Load();
            _context.CinemaStaff.Load();
            _users =_context.CinemaUser.Local;
            _cinemaStaff = _context.CinemaStaff.Local;
            Role = String.Empty;
            RoleInitializer();
        }
        public List<string> Roles { get => _Roles; set => _Roles = value; }

        public string Role { get; set; }
        public string Login { get; set; }
        public SecureString SecurePassword { private get; set; }

        void RoleInitializer()
        {
            Roles.Add("User");
            Roles.Add("Employee");
        }
        public ICommand CheckUser
        {
            get
            {
                return _CheckUser ?? (_CheckUser = new RelayCommand(x=>
                    {
                        try
                        {
                            if (Role == "Employee")
                            {
                                string tmpPass = new System.Net.NetworkCredential(string.Empty, SecurePassword).Password; ;
                                CinemaStaff tmpStuff = _cinemaStaff.Where(l => l.CinemaStaffLogin == Login).Where(p => p.CinemaStaffPass == tmpPass).FirstOrDefault();
                                if (tmpStuff != null)
                                {
                                    if (tmpStuff.CinemaStaffRole == "Administrator")
                                    {
                                        AdminView av = new AdminView() { DataContext = new AdminViewModel() }; av.Show();
                                    }
                                    else if (tmpStuff.CinemaStaffRole == "Cashier")
                                    {
                                        ChooseCityCinemaView ccv = new ChooseCityCinemaView() { DataContext = new ChooseCityCinemaViewModel(tmpStuff.CinemaStaffRole) }; ccv.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Wrong Login or Password. Login and password be case sensitive");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Wrong Login or Password. Login and password be case sensitive");
                                }
                            }
                            else if (Role == "User")
                            {

                            }
                            else if (Role.Length==0) 
                            {
                                MessageBox.Show("Please choose Role");
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
