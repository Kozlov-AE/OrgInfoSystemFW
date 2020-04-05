using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public class Intern : BaseSubordinates
    {
        public Intern(string name, string surname, string position, int departamentId = 0) :base(name, surname, position, departamentId) {}
            
        public override double SalaryPayment()
        {
            return Salary;
        }
    }
}
