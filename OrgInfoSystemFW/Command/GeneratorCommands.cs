using OrgInfoSystemFW.Model.Departamens;
using OrgInfoSystemFW.Model.Workers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Command
{
    public class GeneratorCommands
    {
        #region Генерация данных
        public MainDeportament MainGeneratorV1()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            var Md = new MainDeportament("CoolProgrammersOrg");
            Md.Address = "Россия, Москва, Ленинский просп., 6, стр. 20,";
            Md.BirthDay = new DateTime(2017, 6, 7);
            Md.SubDepartaments = new ObservableCollection<BaseDepartament>();
            for (int i = 0; i < rnd.Next(1, 5); i++)
            {
                Md.SubDepartaments.Add(GenerateDepartament(Md.Id, rnd.Next(0, 5), rnd, rnd.Next(5, 10)));
            }
            Md.Employees = Second_LevelMakerEmployees(rnd.Next(10, 20), Md);
            return Md;
        }

        /// <summary>
        /// Генерация структуры департаментов
        /// </summary>
        /// <param name="parentId">ID родительского департамента</param>
        /// <param name="countSubDeps">Количство вложенных департаментов</param>
        /// <param name="rnd">Рандомная переменная, для рандомного генерирования</param>
        /// <param name="iterations">Количество итераций всего цикла генерации</param>
        /// <returns>Департамент</returns>
        Departament GenerateDepartament(int parentId, int countSubDeps, Random rnd, int iterations)
        {
            iterations--;
            Departament dep = new Departament("", parentId);
            dep.Title = $"Департамент № {dep.Id}";
            dep.SubDepartaments = new ObservableCollection<BaseDepartament>();
            if (iterations > 0)
            {
                for (int i = 0; i < countSubDeps; i++)
                {
                    dep.SubDepartaments.Add(GenerateDepartament(dep.Id, rnd.Next(0, 5), rnd, iterations));
                }

            }
            return dep;
        }

        /// <summary>
        /// Генерирует младший состав работников департамента (Вспомогательный метод)
        /// </summary>
        /// <param name="count">Количество работников</param>
        /// <param name="dep">Экземпляр департамента</param>
        /// <returns>Коллекцию работников</returns>
        ObservableCollection<BasePerson> First_LevelMakerEmployees(int count, BaseDepartament dep)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
            int interns = rnd.Next(0, count / 3);
            int wks = count - interns;
            int hw = (count / rnd.Next(4, 9)) + 1;
            for (int i = 0; i < interns; i++)
            {
                Intern wkr = new Intern($"Интерн_{i}", $"Департамента_{dep.Id}", "Интерн", dep);
                wkr.Address = "Какойто адрес в каком то городе";
                wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-40), (DateTime.Now).AddYears(-19));
                wkr.Salary = 500;
                workers.Add(wkr);
            }
            for (int i = 0; i < wks; i++)
            {
                Worker wkr = new Worker($"Работяга{i}", $"Департамента_{dep.Id}", "Сотрудник", dep);
                wkr.Address = "Какойто адрес в каком то городе";
                wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-40), (DateTime.Now).AddYears(-19));
                wkr.Salary = 10;
                wkr.WorkHours = rnd.Next(0, 176);
                workers.Add(wkr);
            }
            for (int i = 0; i < hw; i++)
            {
                DepartmentHead wkr = new DepartmentHead($"Начальничек{i}", $"Департамента_{dep.Id}", "Руководитель отдела", dep);
                wkr.Address = "Какойто адрес в каком то городе";
                wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                wkr.CoefSalary = 0.15;
                wkr.LowSalary = 1000;
                workers.Add(wkr);
            }
            return workers;
        }

        /// <summary>
        /// Генерирует работников департамента
        /// </summary>
        /// <param name="count">Количество работников</param>
        /// <param name="dep">Экземпляр департамента</param>
        /// <returns>Коллекцию работников</returns>
        ObservableCollection<BasePerson> Second_LevelMakerEmployees(int count, BaseDepartament dep)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
            if (dep.SubDepartaments.Count == 0)
            {
                workers = First_LevelMakerEmployees(count, dep);
            }
            else
            {
                int ld = 0;
                foreach (var d in dep.SubDepartaments)
                {
                    if (d.Employees.Count == 0)
                    {
                        d.Employees = Second_LevelMakerEmployees(count, d);
                    }
                    foreach (var e in d.Employees)
                    {
                        if (e.GetType() == typeof(LowDirector)) ld++;
                        if (e.GetType() == typeof(MidDirector)) ld++;

                    }
                }

                if (ld > 0)
                {
                    workers = First_LevelMakerEmployees(count, dep);
                    MidDirector wkr = new MidDirector($"Директорик", $"Департамента_{dep.Id}", "Директор сектора департаментов", dep);
                    wkr.Address = "Какойто адрес в каком то городе";
                    wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                    wkr.CoefSalary = 0.4;
                    wkr.LowSalary = 6000;
                    workers.Add(wkr);
                }
                else
                {
                    workers = First_LevelMakerEmployees(count, dep);
                    LowDirector wkr = new LowDirector($"Директорик", $"Департамента_{dep.Id}", "Директор ветки департаментов", dep);
                    wkr.Address = "Какойто адрес в каком то городе";
                    wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                    wkr.CoefSalary = 0.25;
                    wkr.LowSalary = 2000;
                    workers.Add(wkr);
                }
                //Заполняем верхний уровень департаментов
                if (dep.ParentId == 1)
                {
                    //Обычный персонал. Секретарши и т.п.
                    workers = First_LevelMakerEmployees(rnd.Next(3, 6), dep);
                    for (int i = 0; i < dep.SubDepartaments.Count; i++)
                    {
                        TopDirector wkr = new TopDirector($"ТОПДиректор", $"Департамента_{dep.Id}", "Самый главный директор, подчинен царю", dep);
                        wkr.Address = "Какойто адрес в каком то городе";
                        wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                        wkr.CoefSalary = 0.4;
                        wkr.LowSalary = 12000;
                        workers.Add(wkr);
                    }
                }
                //Генерим самого главного директора
                if (dep.ParentId == 0)
                {
                    //Обычный персонал. Секретарши и т.п.
                    workers = First_LevelMakerEmployees(rnd.Next(3, 6), dep);
                    King king = new King($"Его Величество", dep.Title, "Повелитель всей организации!", dep);
                    king.Address = "Какойто адрес в каком то городе";
                    king.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                    king.CoefSalary = 0.35;
                    king.LowSalary = 20000;
                    workers.Add(king);
                }
            }
            return workers;
        }


        #endregion




        /// <summary>
        /// генератор случайной даты в диапазоне дат
        /// </summary>
        /// <param name="start">начальная дата</param>
        /// <param name="end">конечная дата</param>
        /// <returns></returns>
        static DateTime GetRandomDay(DateTime start, DateTime end)
        {
            int countDays = (end - start).Days;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return start.AddDays(rnd.Next(countDays)).Date;
        }

    }
}
