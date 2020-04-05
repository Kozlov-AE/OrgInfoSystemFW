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
        /// <summary>
        /// Департаменты в управлении (задается отдельно)
        /// </summary>
        public ObservableCollection<Departamens.BaseDepartament> SubordinateDepartment { get; set; }
        /// <summary>
        /// Подчиненный персонал (работники департаментов)
        /// </summary>
        protected ObservableCollection<BasePerson> subordinates;
        public abstract ObservableCollection<BasePerson> Subordinates
        {
            get;
        }

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


        public BaseDirector(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
        }

        protected string error;
        virtual public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged("");
            }
        }

        public abstract override double SalaryPayment();
    }
}
