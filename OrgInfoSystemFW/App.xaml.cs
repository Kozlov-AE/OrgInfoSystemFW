using Newtonsoft.Json;
using OrgInfoSystemFW.Model.Workers;
using OrgInfoSystemFW.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        MainVM mv;
        View.OrgInfo oi;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            oi = new View.OrgInfo();
            mv = new MainVM();
            oi.DataContext = mv;
            oi.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            File.WriteAllText("DB.json", JsonWorker.SerializeDepartamentWithSub(mv.Md).ToString());
        }
    }
}
