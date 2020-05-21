using OrgInfoSystemFW.Command;
using OrgInfoSystemFW.Model.Workers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.View.Dialogs.EmloyeeDialog
{
    class EmployeeVM : IPresonInfo
    {
        private BasePerson employee;
        private string selectedClassKey;
        private double salary;
        private double coefSalary;
        private double workHours;
        private double lowSalary;
        private bool isNewEmployee;

        /// <summary>
        /// Смотрим или редактируем сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public EmployeeVM(BasePerson employee) : base()
        {
            Employee = employee;
            isNewEmployee = false;
            ChangeSelectedClass(employee.GetType());
        }

        public EmployeeVM()
        {
            isNewEmployee = true;
        }

        public IEnumerable<string> EmployeeClasses => BasePerson.Classes.Keys;
        public BasePerson Employee 
        { 
            get => employee; 
            set
            {
                employee = value;
                OnPropertyChanged();
            }
        }

        public string SelectedClassKey 
        { 
            get => selectedClassKey;
            set
            {
                selectedClassKey = value;
                if (IsNewEmployee)
                    Employee = BasePerson.CreatePerson(GetEmployeeType(value));
                OnPropertyChanged();
            }
        }

        private void ChangeSelectedClass(Type type)
        {
            SelectedClassKey = BasePerson.Classes.First(o => o.Value == type).Key;
        }

        public bool IsNewEmployee 
        { 
            get => isNewEmployee; 
            set
            {
                isNewEmployee = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Возвращает выбранный тип из коллекции.</summary>
        private Type GetEmployeeType(string value) => BasePerson.Classes[value];

        public double Salary 
        {
            get
            {
                if (Employee is BaseSubordinates emp) return emp.Salary;
                else return salary;
            }
            set
            {
                if (Employee is BaseSubordinates emp) emp.Salary = value;
                else salary = value;
                OnPropertyChanged();
            }
        }
        public double WorkHours
        {
            get
            {
                if (Employee is Worker emp) return emp.WorkHours;
                else return workHours;
            }
            set
            {
                if (Employee is Worker emp) emp.WorkHours = value;
                else workHours = value;
                OnPropertyChanged();
            }
        }
        public double CoefSalary
        {
            get
            {
                if (Employee is BaseDirector emp) return emp.CoefSalary;
                else return coefSalary;
            }
            set
            {
                if (Employee is BaseDirector emp) emp.CoefSalary = value;
                else coefSalary = value;
                OnPropertyChanged();
            }
        }
        public double LowSalary
        {
            get
            {
                if (Employee is BaseDirector emp) return emp.LowSalary;
                else return lowSalary;
            }
            set
            {
                if (Employee is BaseDirector emp) emp.LowSalary = value;
                else lowSalary = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
