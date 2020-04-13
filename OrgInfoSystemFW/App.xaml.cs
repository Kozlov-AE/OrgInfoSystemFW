using OrgInfoSystemFW.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OrgInfoSystemFW
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            View.OrgInfo oi = new View.OrgInfo();
            oi.DataContext = new MainVM();
            oi.Show();
        }
    }
}
