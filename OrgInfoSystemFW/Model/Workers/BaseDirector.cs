using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public abstract class BaseDirector : BasePerson
    {
        protected double coefSalary;
        public double CoefSalary
        {
            get { return coefSalary; }
            set
            {
                coefSalary = value;
                OnPropertyChanged("");
            }
        }


        /// <summary>
        /// Минимальная ЗП
        /// </summary>
        public double LowSalary = 0;

        public override double SalaryPayment
        {
            get
            {
                double sal = GetAllDepSalaryes(Departament, 0) * CoefSalary;
                if (sal < LowSalary) sal = LowSalary;
                return sal;
            }
        }

        protected abstract double GetAllDepSalaryes(BaseDepartament StartDepartament, double start);


        public BaseDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }
    }
}
