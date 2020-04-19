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
        /// <summary>
        /// Зарплатный коэфициент
        /// </summary>
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
        /// <summary>
        /// Начисленная ЗП
        /// </summary>
        public override double SalaryPayment
        {
            get
            {
                double sal = GetAllDepSalaryes(0) * CoefSalary;
                if (sal < LowSalary) sal = LowSalary;
                return sal;
            }
        }

        /// <summary>
        /// Просчитывает ЗП сотрудников в подчиненных департаментах
        /// </summary>
        /// <param name="start">Начальная ЗП</param>
        protected abstract double GetAllDepSalaryes(double start);

        public BaseDirector() { }
        public BaseDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }
    }
}
