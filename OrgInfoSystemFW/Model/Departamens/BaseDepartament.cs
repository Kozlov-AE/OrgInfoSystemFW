using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OrgInfoSystemFW.Model.Workers;

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

        protected Dictionary <string,int> employeeCounts;
        public Dictionary<string, int> EmployeeCounts
        {
            get => employeeCounts;
            set
            {
                employeeCounts = GetCountWorkers();
            }
        }


        public ObservableCollection<BasePerson> Employees { get; set; }

        public ObservableCollection<BaseDepartament> SubDepartaments { get; set; }

        static BaseDepartament()
        {
            globalId = 0;
        }

        public BaseDepartament(string title, int parentId = 0)
        {
            this.Title = title;
            this.id = NextID();
            this.ParentId = parentId;
            Employees = new ObservableCollection<BasePerson>();
            EmployeeCounts = new Dictionary<string, int>();
            EmployeeCounts.Add("Intern", 0);
            EmployeeCounts.Add("Worker", 0);
            EmployeeCounts.Add("DepartmentHead", 0);
            EmployeeCounts.Add("MidDirector", 0);
            EmployeeCounts.Add("LowDirector", 0);
            EmployeeCounts.Add("TopDirector", 0);
            EmployeeCounts.Add("King", 0);
        }

        Dictionary <string, int> GetCountWorkers()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            //dic.Add("Intern", 0);
            //dic.Add("Worker", 0);
            //dic.Add("DepartmentHead", 0);
            //dic.Add("MidDirector", 0);
            //dic.Add("LowDirector", 0);
            //dic.Add("TopDirector", 0);
            //dic.Add("King", 0);
            foreach (var w in Employees)
            {
                if (w is Intern) dic["Intern"]++;
                if (w is Worker) dic["Worker"]++;
                if (w is DepartmentHead) dic["DepartmentHead"]++;
                if (w is MidDirector) dic["MidDirector"]++;
                if (w is LowDirector) dic["LowDirector"]++;
                if (w is TopDirector) dic["TopDirector"]++;
                if (w is King) dic["King"]++;
            }
            return dic;
        }

        static int NextID()
        {
            globalId++;
            return globalId;
        }

    }
}
