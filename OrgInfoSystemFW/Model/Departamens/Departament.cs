using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Departamens
{
    public class Departament : BaseDepartament
    {
        public Departament() : base() {}
        public Departament(string title, int parentId = 0) : base(title, parentId) {}

        public override void Edit(BaseDepartament editedDepartament)
        {
            if (editedDepartament.GetType() == this.GetType()) Title = editedDepartament.Title;
        }
    }
}
