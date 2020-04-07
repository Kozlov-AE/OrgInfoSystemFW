using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace OrgInfoSystemFW.Model.Departamens
{
    public abstract class BaseDepartament : BaseINotify
    {
        protected string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("");
            }
        }

        static int globalId;

        protected int id;
        public int Id => id;

        protected int parentId;
        public int ParentId
        {
            get { return parentId; }
            set
            {
                parentId = value;
                OnPropertyChanged("");
            }
        }

        public ObservableCollection<Workers.BasePerson> Employees { get; set; }

        public ObservableCollection<BaseDepartament> SubDepartaments { get; set; }

        static BaseDepartament()
        {
            globalId = 1;
        }

        public BaseDepartament(string title, int parentId = 0)
        {
            this.Title = title;
            this.id = NextID();
            this.ParentId = parentId;
        }

        static int NextID()
        {
            globalId++;
            return globalId;
        }

    }
}
