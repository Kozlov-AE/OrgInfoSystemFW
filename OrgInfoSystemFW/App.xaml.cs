using Newtonsoft.Json;
using OrgInfoSystemFW.Model.Workers;
using OrgInfoSystemFW.View;
using OrgInfoSystemFW.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace OrgInfoSystemFW
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainVM mv;
        OrgInfo oi;
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Logger.InitLogger();
            base.OnStartup(e);
            mv = new MainVM(Logger.Logger.Log.Info);

            oi = new OrgInfo();
            oi.DataContext = mv;
            oi.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            File.WriteAllText("DB.json", JsonWorker.SerializeDepartamentWithSub(mv.Deps[0]).ToString());
        }
    }
}
