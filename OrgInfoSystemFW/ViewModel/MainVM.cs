using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Command;
using OrgInfoSystemFW.Model.Departamens;
using OrgInfoSystemFW.Model.Workers;
using OrgInfoSystemFW.View;
using OrgInfoSystemFW.View.Dialogs.DepartamentDialog;
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
        public BasePerson SelectedEmployee { get; set; }

        public MainVM()
        {
            Deps = new ObservableCollection<BaseDepartament>() { BaseDepartament.Md };
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
                      (eDepartament = new RelayCommand(o =>
                      {
                          DepartamentView ed = new DepartamentView(o as BaseDepartament);
                          if (ed.ShowDialog() == true) 
                              (o as BaseDepartament).Edit(ed.ReturnDepartament);
                      }, _ => SelectedDepartament != null));
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
                  (addDepartament = new RelayCommand(o =>
                  {
                      DepartamentView ed = new DepartamentView();
                      if (ed.ShowDialog() == true)
                          (o as BaseDepartament).AddSubDepartament(ed.ReturnDepartament);
                  }, _ => SelectedDepartament != null));
            }
        }


        RelayCommand removeDepartament;
            /// <summary>Удалить выделенный депатамент</summary>
            public RelayCommand RemoveDepartament => removeDepartament ??
                      (removeDepartament = new RelayCommand(o => SelectedDepartament.Remove(),
                       _ => SelectedDepartament != null));
        #endregion

        #region Меню Сотрудники
        RelayCommand addEmployee;
        /// <summary>
        /// Новый интерн
        /// </summary>
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                  (addEmployee = new RelayCommand(o =>
                  {
                      BaseDepartament dep = o as BaseDepartament;
                      if (dep != null)
                      {
                          View.Dialogs.EmloyeeDialog.EmployeeDialogView ed = new View.Dialogs.EmloyeeDialog.EmployeeDialogView();
                          ed.Title = "Новый сотрудник";
                          ed.ShowDialog();
                            if (ed.DialogResult == true)
                              {
                                  dep.Employees.Add(ed.person);
                              }
                      }
                  }));
            }
        }
        #endregion

        #region Меню Файл
        RelayCommand generate;
        /// <summary>Генерировать новую структуру</summary>
        public RelayCommand Generate => generate ?? (generate = new RelayCommand(_ => Deps[0] = new GeneratorCommands().MainGeneratorV1()));
        #endregion


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
