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
        protected override double GetAllDepSalaryes()
        {
            // Тут в одну строчку не получается((

            //"Выдано исключение типа "System.StackOverflowException"."
            //var sal = (Departament.SubDepartaments.Count > 0) ?
            //Departament.Employees.OfType<LowDirector>().Sum(_ => _.SalaryPayment) :
            //Departament.Employees.OfType<DepartmentHead>().Sum(_ => _.SalaryPayment);

            double sal = 0;
            foreach (var e in Departament.Employees)
            {
                if (Departament.SubDepartaments.Count == 0)
                    if (e is DepartmentHead) sal += e.SalaryPayment;
                    else
                    if (e.GetType() == typeof(LowDirector)) sal += e.SalaryPayment;
            }

            foreach (var d in Departament.SubDepartaments)
            {
                sal += (d.SubDepartaments.Count == 0) ?
                        d.Employees.OfType<DepartmentHead>().Sum(_ => _.SalaryPayment) :
                            (d.GetCountWorkers()["MidDirector"] > 0) ?
                            (d.Employees.OfType<MidDirector>().Sum(_ => _.SalaryPayment)) :
                            (d.Employees.OfType<LowDirector>().Sum(_ => _.SalaryPayment));

                
                //if (d.SubDepartaments.Count == 0)
                //    foreach (var e in d.Employees)
                //    {
                //        if (e is DepartmentHead) sal += e.SalaryPayment;
                //    }

                //if (d.GetCountWorkers()["MidDirector"] > 0)
                //{
                //    foreach (var e in d.Employees)
                //    {
                //        if (e is MidDirector) sal += e.SalaryPayment;
                //    }
                //}
                //else
                //{
                //    foreach (var e in d.Employees)
                //    {
                //        if (e is LowDirector) sal += e.SalaryPayment;
                //    }
                //}
            }
            return sal;
        }

    }
}
