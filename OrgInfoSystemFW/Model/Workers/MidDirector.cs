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
        public MidDirector()
        {
        }

        public MidDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        { }
        protected override double GetAllDepSalaryes(double start)
        {
            double sal = start;
            foreach (var e in Departament.Employees)
            {
                if (Departament.SubDepartaments.Count == 0)
                    if (e is DepartmentHead) sal += e.SalaryPayment;
                else
                    if (e.GetType() == typeof(LowDirector)) sal += e.SalaryPayment;
            }
            foreach (var d in Departament.SubDepartaments)
            {
                if(d.SubDepartaments.Count == 0)
                    foreach (var e in d.Employees)
                    {
                        if (e is DepartmentHead) sal += e.SalaryPayment;
                    }

                if (d.GetCountWorkers()["MidDirector"] > 0)
                {
                    foreach (var e in d.Employees)
                    {
                        if (e is MidDirector) sal += e.SalaryPayment;
                    }
                }
                else
                {
                    foreach (var e in d.Employees)
                    {
                        if (e is LowDirector) sal += e.SalaryPayment;
                    }
                }
            }
            return sal;
        }

    }
}
