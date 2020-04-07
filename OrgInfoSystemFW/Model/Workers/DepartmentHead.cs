using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace OrgInfoSystemFW.Model.Workers
{
    public class DepartmentHead : BaseDirector
    {
        /// <summary>
        /// Подчиненный персонал (работники департаментов). Что то типа директора завода или мелкого чиновника.
        /// </summary>
        public override ObservableCollection<BasePerson> Subordinates
        {
            get
            {
                ObservableCollection<BasePerson> w = new ObservableCollection<BasePerson>();
                foreach (var item in SubordinateDepartment[0].Employees)
                {
                    if (item is BaseSubordinates) w.Add(item);
                }
                return w;
            }
        }


        public DepartmentHead(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
        }

        public override double SalaryPayment()
        {
            double sal = 0;
            foreach (var w in Subordinates)
            {
                if (w is BaseSubordinates)
                    sal += (w as BaseSubordinates).Salary * CoefSalary;
            }
            if (sal <= LowSalary && LowSalary != 0) sal = LowSalary;
            return sal;
        }
    }
}
