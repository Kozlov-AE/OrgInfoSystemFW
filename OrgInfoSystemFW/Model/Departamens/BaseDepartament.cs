using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OrgInfoSystemFW.Model.Workers;
using System.Net.Http.Headers;
using OrgInfoSystemFW.View;
using System.Deployment.Internal;
using System.IO;
using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Command;
using System.Runtime.CompilerServices;

namespace OrgInfoSystemFW.Model.Departamens
{
    public abstract class BaseDepartament : BaseINotify
    {
        #region Delegates && Events
        public static event Action<string> OnCreate;
        public abstract event Action<string> OnChange;
        public static event Action<string> OnDelete;
        #endregion

        /// <summary>Структура организации</summary>
        static MainDeportament md;
        public static MainDeportament Md
        {
            get
            { 
                if (md == null)
            {
                md = File.Exists("DB.json") ?
                       JsonWorker.DeserealizeDepartamentWithSub(JToken.Parse(File.ReadAllText("DB.json"))) as MainDeportament :
                       new GeneratorCommands().MainGeneratorV1(); 
            }
                return md;
            }
            set
            {
                md = value;
            }
        }

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
        public static int globalId;

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
            Id = (id == -1) ? NextID() : id;

            Title = title;
            ParentId = parentId;
            Employees = new ObservableCollection<BasePerson>();
            SubDepartaments = new ObservableCollection<BaseDepartament>();

            OnCreate?.Invoke($"Создан департамент \"{Title}\" с Id = {Id}");
        }

        /// <summary>
        /// Получаем частотный массив по должностям работников
        /// </summary>
        /// <returns>Словарь с количествами работников определенных должностей</returns>
        public Dictionary<string, int> GetCountWorkers()
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

        #region Добавить/Удалить департамент
        /// <summary>Редактирование департамента</summary>
        /// <param name="editedDepartament">Отредактированный экземпляр депратамента</param>
        public abstract void Edit(BaseDepartament editedDepartament);

        /// <summary>Добавить дочерний департамент</summary>
        public void AddSubDepartament(BaseDepartament dep)
        {
            SubDepartaments.Add(new Departament(dep.title, Id));
        }

        /// <summary> Удалить текущий департамент из структуры организации </summary>
        public void Remove()
        {
            remove(Md, this.Id);
            OnDelete?.Invoke($"Уничтожен департамент \"{Title}\" с ID = {Id}, включая все дочерние департаменты с сотрудниками");
        }

        void remove(BaseDepartament dep, int id)
        {
            foreach (var d in dep.SubDepartaments)
            {
                if (d.id == id)
                {
                    dep.SubDepartaments.Remove(d);
                    return;
                }
                else
                {
                    remove(d, id);
                }
            }
        }
        #endregion

        #region Работа с сотрудниками
        public void AddEmployee(BasePerson employee)
        {
            if (employee == null) return;
            employee.Departament = this;
            Employees.Add(employee);
        }
        public void RemoveEmployee(BasePerson employee)
        {
            Employees.Remove(employee);
        }
        #endregion
    }
}
