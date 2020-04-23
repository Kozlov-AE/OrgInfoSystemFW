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
            GetLastIds(Md);
            Console.WriteLine("111");
        }

        #region Методы возвращающие последниq ID после десериализации
        /// <summary>
        /// Присваивает глобальным статическим переменным последние ID (потребуется для создания новых экземпляров)
        /// </summary>
        /// <param name="md">Департамент самого высокого уровня</param>
        static void GetLastIds(BaseDepartament md)
        {
                if (md.Id >= BaseDepartament.globalId) BaseDepartament.globalId = md.Id;
                var eid = BiggerEmplId(md);
                if (eid > BasePerson.globalId) BasePerson.globalId = eid;
            foreach (var i in md.SubDepartaments)
            {
                GetLastIds(i);
            }
        }
        /// <summary>
        /// Возвращает наибольший ID сотрудника депратамента
        /// </summary>
        /// <param name="dep">Департамент</param>
        /// <returns></returns>
        static int BiggerEmplId(BaseDepartament dep)
        {
            int result = 0;
            foreach (var e in dep.Employees)
            {
                if (e.Id > result) result = e.Id;
            }
            return result;
        }
        #endregion


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
                            d.Title = ed.NewTitle;
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

        RelayCommand addDepartament;
        /// <summary>
        /// Редактировать выделенный депатамент
        /// </summary>
        public RelayCommand AddDepartament
        {
            get
            {
                return addDepartament ??
                  (addDepartament = new RelayCommand(_ =>
                  {
                      EditDepartament nd = new EditDepartament("Новый департамент");
                      if (nd.ShowDialog() == true)
                      {
                          SelectedDepartament.AddSubDepartament(nd.NewTitle);
                      }
                  }, _ => SelectedDepartament != null));
            }
        }

        RelayCommand remDepartament;
        /// <summary>
        /// Редактировать выделенный депатамент
        /// </summary>
        public RelayCommand RemDepartament
        {
            get
            {
                return remDepartament ??
                  (remDepartament = new RelayCommand(_ =>
                  {
                      Md.Remove(SelectedDepartament.Id);
                  }, _ => SelectedDepartament != null));
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
