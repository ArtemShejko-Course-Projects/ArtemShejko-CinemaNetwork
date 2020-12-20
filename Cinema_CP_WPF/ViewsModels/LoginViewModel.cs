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
            _context.CinemaUser.Include(r=>r.RoleTable).Load();
            _context.CinemaStaff.Include(r => r.RoleTable).Load();
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
                            string tmpPass = new System.Net.NetworkCredential(string.Empty, SecurePassword).Password; ;
                            if (Role == "Employee")
                            {
                                CinemaStaff tmpStuff = _cinemaStaff.Where(l => l.CinemaStaffLogin == Login).Where(p => p.CinemaStaffPass == tmpPass).FirstOrDefault();
                                if (tmpStuff != null)
                                {
                                    if (tmpStuff.RoleTable.RoleTitle == "Administrator")
                                    {
                                        AdminView av = new AdminView() { DataContext = new AdminViewModel() }; av.Show();
                                    }
                                    else if (tmpStuff.RoleTable.RoleTitle == "Cashier")
                                    {
                                        ChooseCityCinemaView ccv = new ChooseCityCinemaView() { DataContext = new ChooseCityCinemaViewModel(tmpStuff.RoleTable.RoleTitle) }; ccv.Show();
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
                                CinemaUser tmpuser = _users.Where(l => l.CinemaUserLogin == Login).Where(p => p.CinemaUserPass == tmpPass).FirstOrDefault();
                                if (tmpuser != null)
                                {
                                    ChooseCityCinemaView ccv = new ChooseCityCinemaView() { DataContext = new ChooseCityCinemaViewModel(tmpuser.RoleTable.RoleTitle) { ViewLogin = tmpuser.CinemaUserLogin} }; ccv.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Wrong Login or Password. Login and password be case sensitive");
                                }
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
