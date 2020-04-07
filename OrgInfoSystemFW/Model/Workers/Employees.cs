using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    /// <summary>
    /// Хранит коллекцию работников
    /// </summary>
    public class Employees
    {

        static ObservableCollection<BasePerson> EmployeesList;

        public BasePerson this[int id]
        {
            get
            {
                BasePerson pers = null;
                foreach (var person in EmployeesList)
                {
                    if (person.Id == id) pers = person;
                    break;
                }
                return pers;
            }
        }
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person">Экземпляр сотрудника</param>
        public void AddEmployee (BasePerson person)
        {
            EmployeesList.Add(person);
        }
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="person">Экземпляр сотрудника</param>
        public void RemoveEmployee (BasePerson person)
        {
            EmployeesList.Remove(person);
        }

        /// <summary>
        /// Руководители основных узлов иерархии выбранного департамента и входящих в него департаментов
        /// </summary>
        /// <param name="departament">Экземпляр департамента</param>
        static public ObservableCollection<BaseDirector> DirectorsOfDepartament(Departamens.BaseDepartament departament)
        {
            ObservableCollection<BaseDirector> workers = new ObservableCollection<BaseDirector>();

            if (departament.SubDepartaments.Count == 0)
            {
                foreach (BasePerson w in EmployeesList)
                {
                    if (w.DepartamentId == departament.Id && w is DepartmentHead) workers.Add(w as DepartmentHead);
                }
            }
            if (departament.SubDepartaments.Count > 0)
            {
                foreach (var d in departament.SubDepartaments)
                {
                    if (d.SubDepartaments.Count == 0) workers.Concat(DirectorsOfDepartament(d));
                    else
                    {
                        foreach (var w in EmployeesList)
                        {
                            if (w.DepartamentId == departament.Id && w is LowDirector) workers.Add(w as LowDirector);

                        }
                        workers.Add
                    }
                }
            }    

            foreach (BasePerson w in EmployeesList)
            {
                if (w.DepartamentId == departament.Id && w is BaseSubordinates) workers.Add(w);
            }
            foreach (var d in departament.SubDepartaments)
            {
                workers.Concat(WorkersOfDepartament(d));
            }
            return workers;
        }

        /// <summary>
        /// Получаем список простых работников определенного департамента
        /// </summary>
        /// <param name="departament"></param>
        /// <returns></returns>
        static public ObservableCollection<BaseSubordinates> WorkersOfDepartament(Departamens.BaseDepartament departament)
        {
            ObservableCollection<BaseSubordinates> workers = new ObservableCollection<BaseSubordinates>();
            foreach (BasePerson w in EmployeesList)
            {
                if (w.DepartamentId == departament.Id && w is BaseSubordinates) workers.Add(w as BaseSubordinates);
            }
            return workers;
        }

        /// <summary>
        /// Список руководителей верхних уровней в ветке заданного департамента
        /// </summary>
        /// <param name="departament">Экземпляр департамента</param>
        static public ObservableCollection<MidDirector> LowDirectorsOfDepartament(Departamens.BaseDepartament departament)
        {
            ObservableCollection<MidDirector> workers = new ObservableCollection<MidDirector>();

            if (departament.SubDepartaments.Count == 0)
            {
                foreach (BasePerson w in EmployeesList)
                {
                    if (w.DepartamentId == departament.Id && w is MidDirector) workers.Add(w as MidDirector);
                }
            }
            if (departament.SubDepartaments.Count > 0)
            {
                foreach (var d in departament.SubDepartaments)
                {
                    if (d.SubDepartaments.Count == 0) workers.Concat(DirectorsOfDepartament(d));
                    else
                    {
                        foreach (var w in EmployeesList)
                        {
                            if (w.DepartamentId == departament.Id && w is LowDirector) workers.Add(w as LowDirector);

                        }
                        workers.Add
                    }
                }
            }

            foreach (BasePerson w in EmployeesList)
            {
                if (w.DepartamentId == departament.Id && w is BaseSubordinates) workers.Add(w);
            }
            foreach (var d in departament.SubDepartaments)
            {
                workers.Concat(WorkersOfDepartament(d));
            }
            return workers;
        }

    }
}
