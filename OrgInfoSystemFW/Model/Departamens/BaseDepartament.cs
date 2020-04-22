using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OrgInfoSystemFW.Model.Workers;
using System.Net.Http.Headers;

namespace OrgInfoSystemFW.Model.Departamens
{
    public abstract class BaseDepartament : BaseINotify
    {
        protected string title;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("");
            }
        }

        /// <summary>
        /// Статичный ID, уникальный
        /// </summary>
        static int globalId;

        protected int id;
        /// <summary>
        /// Уникальный ID
        /// </summary>
        public int Id
        { 
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("");
            }
        }

        protected int parentId;
        /// <summary>
        /// Родительский департамент
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
            set
            {
                parentId = value;
                OnPropertyChanged("");
            }
        }

        /// <summary>
        /// Сотрудники департамента
        /// </summary>
        public ObservableCollection<BasePerson> Employees { get; set; }
        /// <summary>
        /// Подчиненные департаменты
        /// </summary>
        public ObservableCollection<BaseDepartament> SubDepartaments { get; set; }
        
        /// <summary>
        /// Статичный конструктор
        /// </summary>
        static BaseDepartament()
        {
            globalId = 0;
        }
        public BaseDepartament() { }
        /// <summary>
        /// Нормальный конструктор
        /// </summary>
        /// <param name="title">Наименование департамента</param>
        /// <param name="parentId">Родительский департамент</param>
        public BaseDepartament(string title, int parentId, int id = -1)
        {
            //if (id == -1) this.Id = NextID();
            //else this.Id = NextID();
            Id = (id == -1) ? NextID() : id;

            Title = title;
            ParentId = parentId;
            Employees = new ObservableCollection<BasePerson>();
        }

        /// <summary>
        /// Получаем количества работников
        /// </summary>
        /// <returns>Словарь с количествами работников определенных должностей</returns>
        public Dictionary <string, int> GetCountWorkers()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("Intern", 0);
            dic.Add("Worker", 0);
            dic.Add("DepartmentHead", 0);
            dic.Add("MidDirector", 0);
            dic.Add("LowDirector", 0);
            dic.Add("TopDirector", 0);
            dic.Add("King", 0);
            foreach (var w in Employees)
            {
                switch (w)
                {
                    case Worker _:
                        dic["Worker"]++;
                        break;
                    case Intern _:
                        dic["Intern"]++;
                        break;
                    case DepartmentHead _:
                        dic["DepartmentHead"]++;
                        break;
                    case King _:
                        dic["King"]++;
                        break;
                    case TopDirector _:
                        dic["TopDirector"]++;
                        break;
                    case MidDirector _:
                        dic["MidDirector"]++;
                        break;
                    case LowDirector _:
                        dic["LowDirector"]++;
                        break;
                }
                //if (w.GetType() == typeof(Intern)) dic["Intern"]++;
                //if (w.GetType() == typeof(Worker)) dic["Worker"]++;
                //if (w.GetType() == typeof(DepartmentHead)) dic["DepartmentHead"]++;
                //if (w.GetType() == typeof(MidDirector)) dic["MidDirector"]++;
                //if (w.GetType() == typeof(LowDirector)) dic["LowDirector"]++;
                //if (w.GetType() == typeof(TopDirector)) dic["TopDirector"]++;
                //if (w.GetType() == typeof(King)) dic["King"]++;
            }
            return dic;
        }

        /// <summary>
        /// Счетчик ID
        /// </summary>
        static int NextID()
        {
            globalId++;
            return globalId;
        }

    }
}
