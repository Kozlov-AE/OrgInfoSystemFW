using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Departamens
{
    public class MainDeportament : BaseDepartament
    {
        public MainDeportament(string title, int parentId = 0) : base(title, parentId)
        {
        }

        public MainDeportament() : base()
        {
        }

        protected string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("");
            }
        }

        protected DateTime birthDay;
        public DateTime BirthDay
        {
            get { return birthDay; }
            set
            {
                birthDay = value;
                OnPropertyChanged("");
            }
        }

        public override void Edit(BaseDepartament editedDepartament)
        {
            if (editedDepartament.GetType() == this.GetType())
            {
                Title = editedDepartament.Title;
                Address = (editedDepartament as MainDeportament).Address;
                BirthDay = (editedDepartament as MainDeportament).BirthDay;
            }

        }
    }
}
