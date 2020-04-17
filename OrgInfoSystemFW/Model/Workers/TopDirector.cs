using OrgInfoSystemFW.Model.Departamens;
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
        public TopDirector(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }
    }
}
