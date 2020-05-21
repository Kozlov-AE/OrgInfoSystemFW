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
        public override double SalaryPayment => ((GetAllDepSalaryes() * CoefSalary) > LowSalary) ? (GetAllDepSalaryes() * CoefSalary) : LowSalary;

        /// <summary>
        /// Просчитывает ЗП сотрудников в подчиненных департаментах
        /// </summary>
        /// <param name="start">Начальная ЗП</param>
        protected abstract double GetAllDepSalaryes();


        public BaseDirector() : base() { }
        public BaseDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }

        public override void CopyTo(BasePerson other)
        {
            base.CopyTo(other);
            if (other is BaseDirector director)
            {
                director.CoefSalary = CoefSalary;
                director.LowSalary = LowSalary;
            }
        }

        public override void CopyFrom(BasePerson other)
        {
            base.CopyFrom(other);
            if (other is BaseDirector director)
            {
                CoefSalary = director.CoefSalary;
                LowSalary = director.LowSalary;
            }

        }

        public override bool EqualsValue(BasePerson other) =>
            base.EqualsValue(other) &&
                (!(other is BaseDirector director) || (CoefSalary == director.CoefSalary && LowSalary == director.LowSalary));

    }
}
