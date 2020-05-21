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

        public Worker() : base() { }

        public override void CopyTo(BasePerson other)
        {
            base.CopyTo(other);
            if (other is Worker worker)
                worker.WorkHours = WorkHours;
        }

        public override void CopyFrom(BasePerson other)
        {
            base.CopyFrom(other);
            if (other is Worker worker)
                WorkHours = worker.WorkHours;

        }

        public override bool EqualsValue(BasePerson other) => 
            base.EqualsValue(other) && (!(other is Worker worker) || WorkHours == worker.WorkHours);
    }
}
