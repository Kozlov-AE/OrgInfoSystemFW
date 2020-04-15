using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public class Intern : BaseSubordinates
    {
        public Intern(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament) {}
            
        public override double SalaryPayment => Salary;
    }
}
