using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    /// <summary>
    /// Руководители среднего звена (руководят руководителями одиночных отделов), Что то типа Мэра
    /// </summary>
    public class LowDirector : BaseDirector
    {
        public LowDirector(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {}

        /// <summary>
        /// Подчиненные руководители отделов, включая LowDirector нижестоящих иерархий
        /// </summary>
        public override ObservableCollection<BasePerson> Subordinates
        {
            get
            {
                ObservableCollection<BasePerson> subs = new ObservableCollection<BasePerson>();
                foreach (var i in SubordinateDepartment)
                {
                    List<BasePerson> dh = new List<BasePerson>();
                    List<BasePerson> ld = new List<BasePerson>();
                    foreach (var w in i.Employees)
                    {
                        if (w is DepartmentHead) dh.Add(w);
                        if (w is LowDirector) ld.Add(w);
                    }
                    if (ld.Count > 0) subs.Concat(ld);
                    else subs.Concat(dh);
                }
                return subs;
            }
        }

        public override double SalaryPayment()
        {
            double sal = 0;
            foreach (var w in Subordinates)
            {
                sal += w.SalaryPayment() * CoefSalary;
            }
            if (sal <= LowSalary && LowSalary != 0) sal = LowSalary;
            return sal;
        }
    }
}
