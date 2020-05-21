using OrgInfoSystemFW.Model.Departamens;
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

        public BaseSubordinates() : base() { }
        public BaseSubordinates(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }

        public override void CopyTo(BasePerson other)
        {
            base.CopyTo(other);
            if (other is BaseSubordinates baseSubordinates)
                baseSubordinates.Salary = salary;
        }

        public override void CopyFrom(BasePerson other)
        {
            base.CopyFrom(other);
            if (other is BaseSubordinates baseSubordinates)
                Salary = baseSubordinates.salary;
        }

        public override bool EqualsValue(BasePerson other) => base.EqualsValue(other) &&
                (!(other is BaseSubordinates baseSubordinates) || Salary == baseSubordinates.salary);
    }
}
