using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public class LowDirector : BaseDirector
    {
        public LowDirector(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {}

        public override ObservableCollection<DepartmentHead> Subordinates
        {
            get
            {
                ObservableCollection<BasePerson> sub = new ObservableCollection<BasePerson>();
                foreach (var d in SubordinateDepartment)
                {
                    sub.Concat(Employees.HeadsOfDepartament(d));
                }
                return sub;
            }
        }

        public override double SalaryPayment()
        {
            throw new NotImplementedException();
        }
    }
}
