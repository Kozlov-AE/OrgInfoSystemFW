using OrgInfoSystemFW.Command;
using OrgInfoSystemFW.Model.Workers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.View.Dialogs.EmloyeeDialog
{
    public interface IPresonInfo: INotifyPropertyChanged
    {
        /// <summary>Список описаний типов</summary>
        IEnumerable<string> EmployeeClasses { get;}
        /// <summary>Экземпляр класса работника</summary>
        BasePerson Employee { get; set; }
        /// <summary>Выбранный тип в окне</summary>
        string SelectedClassKey { get; set; }
        /// <summary>Зарплата для BaseSubordinates</summary>
        double Salary { get; set; }
        /// <summary>Отработанные часы для Worker</summary>
        double WorkHours { get; set; }
        /// <summary>Коэфициент ЗП для BaseDirector</summary>
        double CoefSalary { get; set; }
        /// <summary>Минимальная ЗП для BaseDirector</summary>
        double LowSalary { get; set; }
        /// <summary>Можно ли выбирать класс из выпадающего списка</summary>
        bool IsNewEmployee { get; set; }
    }
}
