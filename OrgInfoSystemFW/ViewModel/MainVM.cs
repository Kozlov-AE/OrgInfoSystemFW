using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Command;
using OrgInfoSystemFW.Model.Departamens;
using OrgInfoSystemFW.Model.Workers;
using OrgInfoSystemFW.View;
using OrgInfoSystemFW.View.Dialogs.DepartamentDialog;
using OrgInfoSystemFW.View.Dialogs.EmloyeeDialog;
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
        private Action<string> logger;
        public ObservableCollection<BaseDepartament> Deps { get; set; }

        private BaseDepartament selectedDepartament;

        public BaseDepartament SelectedDepartament
        {
            get => selectedDepartament;
            set
            {
                if (value != null)
                {
                    selectedDepartament = value;
                    selectedDepartament.OnChange += logger;
                }
                if (value == null)
                {
                    selectedDepartament.OnChange -= logger;
                    selectedDepartament = null;
                }
            }
        }


        public BasePerson SelectedEmployee { get; set; }

        public MainVM(Action<string> logger)
        {
            this.logger = logger;
            Deps = new ObservableCollection<BaseDepartament>() { BaseDepartament.Md };
            BaseDepartament.OnCreate += logger;
            BaseDepartament.OnDelete += logger;
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
                      { 
                          (o as BaseDepartament).AddSubDepartament(ed.ReturnDepartament);

                      }
                  }, _ => SelectedDepartament != null));
            }
        }


        RelayCommand removeDepartament;
            /// <summary>Удалить выделенный депатамент</summary>
            public RelayCommand RemoveDepartament => removeDepartament ??
                      (removeDepartament = new RelayCommand(o =>
                      {
                          SelectedDepartament.Remove();
                      },
                       _ => SelectedDepartament != null));
        #endregion

        #region Меню Сотрудники

        RelayCommand killEmployee;
        /// <summary>Увольнение сотрудника</summary>
        public RelayCommand KillEmployee
        {
            get
            {
                return killEmployee ??
                  (killEmployee = new RelayCommand(o =>
                  {
                      (o as BaseDepartament).RemoveEmployee(SelectedEmployee);
                  }, _ => SelectedEmployee != null));
            }
        }


        RelayCommand editEmployee;
        /// <summary>Редактирование сотрудника</summary>
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ??
                  (editEmployee = new RelayCommand(o =>
                  {
                        EmployeeVM vm = new EmployeeVM(SelectedEmployee.Clone());
                        EmployeeDialogView ed = new EmployeeDialogView();
                        ed.DataContext = vm;
                        ed.Title = "Редактирование сотрудника";
                        ed.ShowDialog();
                        if (ed.DialogResult == true)
                        {
                          SelectedEmployee.CopyFrom(vm.Employee);
                        }
                  },_ => SelectedEmployee != null));
            }
        }

        RelayCommand addEmployee;
        /// <summary>Новый сотрудник</summary>
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
                          EmployeeVM vm = new EmployeeVM();
                          EmployeeDialogView ed = new EmployeeDialogView();
                          ed.DataContext = vm;
                          ed.Title = "Новый сотрудник";
                          ed.ShowDialog();
                          if (ed.DialogResult == true)
                          {
                              dep.AddEmployee(vm.Employee);
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
