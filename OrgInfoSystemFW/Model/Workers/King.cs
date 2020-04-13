using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    /// <summary>
    /// Царь всей организации. Руководит ТопДиректорами.
    /// </summary>
    public class King : TopDirector
    {
        public King(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
        }

        public override ObservableCollection<BasePerson> Subordinates
        {
            get
            {
                ObservableCollection<BasePerson> subs = new ObservableCollection<BasePerson>();
                subs.Concat(SubordinateDepartment[0].Employees.Where(e => e is TopDirector));
                return subs;
            }
        }

        public override double SalaryPayment() => GetAllDepSalaryes() * CoefSalary;
        
        double GetAllDepSalaryes(double start = 0)
        {
            double sal = start;
            foreach (var d in SubordinateDepartment)
            {
                foreach (var e in d.Employees)
                {
                    sal += e.SalaryPayment();
                    GetAllDepSalaryes(sal);
                }
            }
            return sal;
        }
    }
}
