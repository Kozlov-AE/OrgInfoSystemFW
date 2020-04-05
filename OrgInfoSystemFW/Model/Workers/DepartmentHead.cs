using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace OrgInfoSystemFW.Model.Workers
{
    public class DepartmentHead : BaseDirector
    {
        /// Подчиненный персонал (работники департаментов)
        /// </summary>
        public override ObservableCollection<BasePerson> Subordinates
        {
            get
            {
                ObservableCollection<BasePerson> sub = new ObservableCollection<BasePerson>();
                foreach (var d in SubordinateDepartment)
                {
                    sub.Concat(Employees.PersonalsOfDepartament(d));
                }
                return sub;
            }
        }

        public override string Error
        {
            get { return error; }
            set
            {
                if (SubordinateDepartment.Count > 1)
                    error = "В управлении более одного департамента!";
                if (SubordinateDepartment.Count < 1)
                    error = "В управлении отсутствуют департаменты!";
                OnPropertyChanged("");
            }
        }

        public DepartmentHead(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
            error = "";
        }

        public override double SalaryPayment()
        {
            double sal = 0;
            foreach (var w in Subordinates)
            {
                if (w is BaseSubordinates)
                    sal += w.Salary * CoefSalary;
            }
            if (sal <= LowSalary && LowSalary != 0) sal = LowSalary;
            return sal;
        }
    }
}
