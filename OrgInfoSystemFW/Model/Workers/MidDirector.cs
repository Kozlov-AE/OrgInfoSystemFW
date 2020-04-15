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
    /// Руководит самым верхиним уровнем иерархии департаментов, что то типа губернатора
    /// </summary>
    public class MidDirector : LowDirector
    {
        public MidDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {}

        protected override double GetAllDepSalaryes(BaseDepartament dep, double start)
        {
            double sal = start;
            if (dep.SubDepartaments.Count == 0)
            {
                foreach (var e in dep.Employees)
                {
                    if (e is DepartmentHead) sal += e.SalaryPayment;
                }
            }
            else
            {
                foreach (var e in dep.Employees)
                {
                    if (e is LowDirector || e is MidDirector) sal += e.SalaryPayment;
                }
                foreach (var d in dep.SubDepartaments)
                {
                    GetAllDepSalaryes(d, sal);
                }
            }
            return sal;
        }

    }
}
