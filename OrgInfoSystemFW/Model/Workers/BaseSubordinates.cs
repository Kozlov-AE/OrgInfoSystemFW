using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    abstract public class BaseSubordinates : BasePerson
    {
        protected double salary;
        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("");
            }
        }

        public BaseSubordinates(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
        }

        public abstract override double SalaryPayment();

    }
}
