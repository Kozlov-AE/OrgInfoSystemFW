using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OrgInfoSystemFW.Model.Departamens;
using System.Security.Cryptography;

namespace OrgInfoSystemFW.Model.Workers
{
    public class DepartmentHead : BaseDirector
    {
        public DepartmentHead()
        {
        }

        public DepartmentHead(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }

        protected override double GetAllDepSalaryes() => Departament.Employees.Where(_ => _ is BaseSubordinates).Sum(_ => _.SalaryPayment);

    }
}
