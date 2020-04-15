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
                if (value <= 0)
                    coefSalary = 1;
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

        protected virtual double GetAllDepSalaryes(BaseDepartament StartDepartament, double start)
        {
            double sal = start;
            foreach (var e in StartDepartament.Employees)
            {
                if (e is BaseDirector) sal += e.SalaryPayment;
            }
            foreach (var d in StartDepartament.SubDepartaments)
            {
                GetAllDepSalaryes(d, sal);
            }
            return sal;
        }


        public BaseDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }
    }
}
