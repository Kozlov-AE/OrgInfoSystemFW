using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Departamens
{
    public class Departament : BaseDepartament
    {
        public Departament(string title, int parentId = 0) : base(title, parentId)
        {}
    }
}
