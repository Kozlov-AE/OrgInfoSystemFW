using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    /// <summary>
    /// Руководит самым верхиним уровнем иерархии департаментов, что то типа губернатора
    /// </summary>
    public class MidDirector : LowDirector
    {
        public MidDirector(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId)
        {}
    }
}
