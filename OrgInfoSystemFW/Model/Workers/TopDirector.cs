using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    /// <summary>
    /// Почти Царь. Руководит директорами верхних уровней. Советник президента.
    /// </summary>
    public class TopDirector : MidDirector
    {
        public TopDirector(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {
        }

        public override ObservableCollection<BasePerson> Subordinates
        {
            get
            {
                ObservableCollection<BasePerson> subs = new ObservableCollection<BasePerson>();
                foreach (var d in SubordinateDepartment)
                {

                    subs.Concat(d.Employees.Where(e => e is MidDirector));
                }
                return subs;
            }
        }
    }
}
