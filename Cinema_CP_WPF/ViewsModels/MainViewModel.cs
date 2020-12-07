using Cinema_CP_WPF.Views;
using MVVMHelper.Commands;
using MVVMHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema_CP_WPF.ViewsModels
{
    internal class MainViewModel:BaseViewModel
    {
        private ICommand openCahierView;

        public ICommand OpenCahierView
        {
            get
            {
                return openCahierView ?? (openCahierView = new RelayCommand(
                    x => { СashierView cv = new СashierView(); cv.Show(); }
                    ));
            }
        }
    }
}
