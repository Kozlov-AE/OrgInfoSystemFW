using OrgInfoSystemFW.Model.Departamens;
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
        public King(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }

        protected override double GetAllDepSalaryes(BaseDepartament dep, double start)
        {
            double sal = start;
            foreach (var e in dep.Employees)
            {
                if (e is BaseSubordinates) sal += e.SalaryPayment;
            }
            foreach (var d in dep.SubDepartaments)
            {
                sal = GetAllDepSalaryes(d, sal);
            }
            return sal;
        }
    }
}
