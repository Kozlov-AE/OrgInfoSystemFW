using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Command;
using OrgInfoSystemFW.Model.Departamens;
using OrgInfoSystemFW.Model.Workers;
using OrgInfoSystemFW.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public ObservableCollection<BaseDepartament> Deps { get; set; }
        public MainDeportament Md { get; set; }


        public BaseDepartament SelectedDepartament { get; set; }
        public BasePerson SelectedEmployee { get; set; }

        public MainVM()
        {
            //Md = new GeneratorCommands().MainGeneratorV1();

            string json = File.ReadAllText("DB.json");
            var md = JToken.Parse(json);
            Md = JsonWorker.DeserealizeDepartamentWithSub(md) as MainDeportament;

            Deps = new ObservableCollection<BaseDepartament>() { Md };
            Console.WriteLine("111");
        }

        RelayCommand eDepartament;
        /// <summary>
        /// Редактировать выделенный депатамент
        /// </summary>
        public RelayCommand EDepartament
        {
            get
            {
                return eDepartament ??
                  (eDepartament = new RelayCommand(obj =>
                  {
                      if (SelectedDepartament.GetType() == typeof(Departament))
                      {
                          Departament d = SelectedDepartament as Departament;
                          EditDepartament ed = new EditDepartament(d.Title);
                          if (ed.ShowDialog() == true)
                          {
                            d.Title = ed.Txt;
                          }
                      }
                      else
                      {
                          MainDeportament md = SelectedDepartament as MainDeportament;
                          EditMainDepartament emd = new EditMainDepartament(md);
                          if (emd.ShowDialog() == true)
                          {
                              md.Title = emd.Titl;
                              md.Address = emd.Addr;
                              md.BirthDay = emd.Birthday;
                          }
                      }
                  }, _=> SelectedDepartament != null));
            }
        }



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
