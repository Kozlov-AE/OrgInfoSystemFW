using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    abstract public class BaseSubordinates : BasePerson
    {
        public BaseSubordinates(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
        }

        public abstract override double SalaryPayment();

    }
}
