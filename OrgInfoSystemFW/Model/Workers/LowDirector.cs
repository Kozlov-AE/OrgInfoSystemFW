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
    /// Руководители среднего звена (руководят руководителями одиночных отделов), Что то типа Мэра
    /// </summary>
    public class LowDirector : BaseDirector
    {
        public LowDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {}

        protected override double GetAllDepSalaryes(BaseDepartament dep, double start)
        {
            double sal = start;
            foreach (var e in dep.Employees)
            {
                if (e is DepartmentHead) sal += e.SalaryPayment;
            }
            foreach (var d in dep.SubDepartaments)
            {
                sal = GetAllDepSalaryes(d, sal);
            }
            return sal;
        }
    }
}
