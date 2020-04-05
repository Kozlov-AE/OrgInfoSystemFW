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
        /// Получить всех сотрудников департамента и вложенных департаментов)
        /// </summary>
        /// <param name="departament">Экземпляр департамента</param>
        /// <returns></returns>
        static public ObservableCollection<BasePerson> PersonalsOfDepartament (Departamens.BaseDepartament departament)
        {
            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
            foreach (BasePerson w in EmployeesList)
            {
                if (w.DepartamentId == departament.Id) workers.Add(w);
            }
            foreach (var d in departament.SubDepartaments)
            {
                workers.Concat(PersonalsOfDepartament(d));
            }
            return workers;
        }
        static public ObservableCollection<BasePerson> WorkersOfDepartament(Departamens.BaseDepartament departament)
        {
            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
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
        static public ObservableCollection<BasePerson> HeadsOfDepartament(Departamens.BaseDepartament departament)
        {
            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
            foreach (BasePerson w in EmployeesList)
            {
                if (w.DepartamentId == departament.Id && w is DepartmentHead) workers.Add(w);
            }
            foreach (var d in departament.SubDepartaments)
            {
                workers.Concat(HeadsOfDepartament(d));
            }
            return workers;
        }

    }
}
