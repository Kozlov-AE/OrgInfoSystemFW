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

        public BaseDepartament SelectedDepartament { get; set; }
        public bool IsSelectedDepartament { get; set; }
        public BasePerson SelectedEmployee { get; set; }

        public MainVM()
        {
            Deps = new ObservableCollection<BaseDepartament>() 
            { 
                File.Exists("DB.json") ? JsonWorker.DeserealizeDepartamentWithSub(JToken.Parse(File.ReadAllText("DB.json"))) as MainDeportament :
                new GeneratorCommands().MainGeneratorV1()
            };
        }

        #region Меню Департаент
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
                          Deps[0].Remove(SelectedDepartament.Id);
                      }, _ => SelectedDepartament != null));
                }
            }
        #endregion

        #region Меню Сотрудники
        RelayCommand addIntern;
        /// <summary>
        /// Новый интерн
        /// </summary>
        public RelayCommand AddIntern
        {
            get
            {
                return addIntern ??
                  (addIntern = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          InernView iv = new InernView(dep);
                          iv.Title = "Новый интерн";
                          iv.ShowDialog();
                            if (iv.DialogResult == true)
                          {
                              Intern i = iv.Intern;
                              dep.Employees.Add(i);
                          }
                      }
                  }));
            }
        }

        RelayCommand addWorker;
        /// <summary>
        /// Новый Работник
        /// </summary>
        public RelayCommand AddWorker
        {
            get
            {
                return addWorker ??
                  (addWorker = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          WorkerView wv = new WorkerView(dep);
                          wv.Title = "Новый работник";
                          wv.ShowDialog();
                          if (wv.DialogResult == true)
                          {
                              Worker w = wv.Worker;
                              //w.Departament = dep;
                              dep.Employees.Add(w);
                          }
                      }
                  }));
            }
        }

        RelayCommand addDepartamentHead;
        /// <summary>
        /// Новый руководитель департамента
        /// </summary>
        public RelayCommand AddDepartamentHead
        {
            get
            {
                return addDepartamentHead ??
                  (addDepartamentHead = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          DirectorView dir = new DirectorView(dep);
                          dir.Title = "Новый руководитель департамента";
                          dir.ShowDialog();
                          if (dir.DialogResult == true)
                          {
                              DepartmentHead dh = dir.Director as DepartmentHead;
                              dep.Employees.Add(dh);
                          }
                      }
                  }));
            }
        }

        RelayCommand addLowDirector;
        /// <summary>
        /// Новый Руководитель ветки департаментов
        /// </summary>
        public RelayCommand AddLowDirector
        {
            get
            {
                return addLowDirector ??
                  (addLowDirector = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          DirectorView dir = new DirectorView(dep);
                          dir.Title = "Новый руководитель ветки департаментов";
                          dir.ShowDialog();
                          if (dir.DialogResult == true)
                          {
                              LowDirector dh = dir.Director as LowDirector;
                              dep.Employees.Add(dh);
                          }
                      }
                  }));
            }
        }

        RelayCommand addMidDirector;
        /// <summary>
        /// Новый Руководитель сетки департаментов
        /// </summary>
        public RelayCommand AddMidDirector
        {
            get
            {
                return addMidDirector ??
                  (addMidDirector = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          DirectorView dir = new DirectorView(dep);
                          dir.Title = "Новый руководитель сектора департаменов";
                          dir.ShowDialog();
                          if (dir.DialogResult == true)
                          {
                              MidDirector dh = dir.Director as MidDirector;
                              dep.Employees.Add(dh);
                          }
                      }
                  }));
            }
        }

        RelayCommand addTopDirector;
        /// <summary>
        /// Новый головной директор
        /// </summary>
        public RelayCommand AddTopDirector
        {
            get
            {
                return addTopDirector ??
                  (addTopDirector = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          DirectorView dir = new DirectorView(dep);
                          dir.Title = "Новый Топ директор";
                          dir.ShowDialog();
                          if (dir.DialogResult == true)
                          {
                              TopDirector dh = dir.Director as TopDirector;
                              dep.Employees.Add(dh);
                          }
                      }
                  }));
            }
        }

        RelayCommand addKing;
        /// <summary>
        /// Новый генеральный директор
        /// </summary>
        public RelayCommand AddKing
        {
            get
            {
                return addKing ??
                  (addKing = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          DirectorView dir = new DirectorView(dep);
                          dir.Title = "Новый генеральный директор";
                          dir.ShowDialog();
                          if (dir.DialogResult == true)
                          {
                              King dh = dir.Director as King;
                              dep.Employees.Add(dh);
                          }
                      }
                  }));
            }
        }


        #endregion

        #region Меню Файл
        RelayCommand generate;
        /// <summary>
        /// генерировать новую структуру
        /// </summary>
        public RelayCommand Generate
        {
            get
            {
                return generate ??
                  (generate = new RelayCommand(_ =>
                  {
                      Deps[0] = new GeneratorCommands().MainGeneratorV1();
                  }));
            }
        }
        #endregion


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
