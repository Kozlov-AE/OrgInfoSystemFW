using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public class Worker : BaseSubordinates
    {
        /// <summary>
        /// Кол-во отработанных часов при почасовой оплате
        /// </summary>
        double workHours;
        public double WorkHours
        {
            get { return workHours; }
            set
            {
                workHours = value;
                OnPropertyChanged("");
            }
        }

        public override double SalaryPayment => WorkHours * Salary;

        public Worker(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament) { }

        public Worker()
        {
        }
    }
}
