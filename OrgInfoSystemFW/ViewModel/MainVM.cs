using OrgInfoSystemFW.Model.Departamens;
using OrgInfoSystemFW.Model.Workers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        static Random rnd = new Random(Guid.NewGuid().GetHashCode());
        MainDeportament md;
        public MainVM()
        {
            md = new MainDeportament("CoolProgrammersOrg");
            md.Address = "Россия, Москва, Ленинский просп., 6, стр. 20,";
            md.BirthDay = new DateTime(2017, 6, 7);


            md.SubDepartaments = new ObservableCollection<BaseDepartament>();
            for (int i = 0; i < rnd.Next(1,5); i++)
            {
                md.SubDepartaments.Add(GenerateDepartament(md.Id, rnd.Next(0, 5), rnd, rnd.Next(2, 5)));
            }
            md.Employees = Second_LevelMakerEmployees(rnd.Next(10,20), md);
            //var k = md.Employees.First(_ => _ is King);
            //Console.WriteLine(k.SalaryPayment());
        }

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


        ///// <summary>
        ///// Генерирует работников департамента
        ///// </summary>
        ///// <param name="count">Количество работников</param>
        ///// <param name="dep">Экземпляр департамента</param>
        ///// <returns>Коллекцию работников</returns>
        //void Second_LevelMakerEmployees(int count, BaseDepartament dep)
        //{
        //    ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
        //    if (dep.SubDepartaments.Count == 0)
        //    {
        //        dep.Employees = First_LevelMakerEmployees(count, dep);
        //    }
        //    if (dep.SubDepartaments.Count > 0)
        //    {
        //        dep.Employees = First_LevelMakerEmployees(count, dep);
        //        bool isHasMidDirector = false;
        //        bool isHasLowDirector = false;
        //        foreach (var d in dep.SubDepartaments)
        //        {
        //            if (d.EmployeeCounts["MidDirector"] > 0)
        //            {
        //                isHasMidDirector = true;
        //            }
        //            if (d.EmployeeCounts["LowDirector"] > 0)
        //            {
        //                isHasLowDirector = true;
        //            }
        //        }
        //        if (isHasMidDirector == false)
        //        {
        //            dep.Employees.Add(new Intern($"Директорик", $"Департамента_{dep.Id}", "Директор ветки департаментов", dep.Id));
        //        }
        //        if (isHasMidDirector == true || isHasLowDirector == true)
        //        {
        //            dep.Employees.Add(new Intern($"Директор", $"Департамента_{dep.Id}", "Директор сектора департаментов", dep.Id));
        //        }
        //    }
        //    if (dep.ParentId == 1)
        //    {
        //        dep.Employees = First_LevelMakerEmployees(count, dep);
        //        for (int i = 0; i < dep.SubDepartaments.Count; i++)
        //        {
        //            dep.Employees.Add(new TopDirector($"ТОПДиректор", $"Департамента_{dep.Id}", "Самый главный директор, подчинен царю", dep.Id));
        //        }
        //    }
        //    if (dep.ParentId == 0)
        //    {
        //        dep.Employees = First_LevelMakerEmployees(count, dep);
        //        dep.Employees.Add(new TopDirector($"Его Величество", dep.Title, "Повелитель всей организации!", dep.Id));
        //    }
        //    //return workers;
        //}


        /// <summary>
        /// Генерирует младший состав работников департамента (Вспомогательный метод)
        /// </summary>
        /// <param name="count">Количество работников</param>
        /// <param name="dep">Экземпляр департамента</param>
        /// <returns>Коллекцию работников</returns>
        ObservableCollection<BasePerson> First_LevelMakerEmployees(int count, BaseDepartament dep)
        {
            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
            int interns = rnd.Next(0, count / 3);
            int wks = count - interns;
            int hw = count / (rnd.Next(5, 10));
                for (int i = 0; i < interns; i++)
                {
                    Intern wkr = new Intern($"Интерн_{i}", $"Департамента_{dep.Id}", "Интерн", dep.Id);
                    wkr.Address = "Какойто адрес в каком то городе";
                    wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-40), (DateTime.Now).AddYears(-19));
                    wkr.Salary = 500;
                    workers.Add(wkr);
                }
            for (int i = 0; i < wks; i++)
                {
                    Worker wkr = new Worker($"Работяга{i}", $"Департамента_{dep.Id}", "Сотрудник", dep.Id);
                    wkr.Address = "Какойто адрес в каком то городе";
                    wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-40), (DateTime.Now).AddYears(-19));
                    wkr.Salary = 10;
                    wkr.WorkHours = rnd.Next(0, 176);
                    workers.Add(wkr);
                }
            for (int i = 0; i < hw; i++)
                {
                    DepartmentHead wkr = new DepartmentHead($"Начальничек{i}", $"Департамента_{dep.Id}", "Руководитель отдела", dep.Id);
                    wkr.Address = "Какойто адрес в каком то городе";
                    wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                    wkr.CoefSalary = 0.15;
                    wkr.LowSalary = 2000;
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
            ObservableCollection<BasePerson> workers = new ObservableCollection<BasePerson>();
            if (dep.SubDepartaments.Count == 0)
            {
                workers = First_LevelMakerEmployees(count, dep);
            }
            if (dep.SubDepartaments.Count > 0)
            {
                bool isAllDepsHaveEmpls = true;
                bool isHasMidDirector = false;
                bool isHasLowDirector = false;
                foreach (var d in dep.SubDepartaments)
                {
                    if (d.Employees.Count == 0)
                    {
                        isAllDepsHaveEmpls = false;
                        break;
                    }
                    if (d.EmployeeCounts["MidDirector"] > 0)
                    {
                        isHasMidDirector = true;
                    }
                    if (d.EmployeeCounts["LowDirector"] > 0)
                    {
                        isHasLowDirector = true;
                    }
                }
                //Если все вложенные департаменты имеют работников, то заполняем текущий департамент работниками
                if (isAllDepsHaveEmpls)
                {
                    workers = First_LevelMakerEmployees(count, dep);
                    if (isHasMidDirector == false)
                    {
                        LowDirector wkr = new LowDirector($"Директорик", $"Департамента_{dep.Id}", "Директор ветки департаментов", dep.Id);
                        wkr.Address = "Какойто адрес в каком то городе";
                        wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                        wkr.CoefSalary = 0.25;
                        wkr.LowSalary = 4000;
                        workers.Add(wkr);
                    }
                    if (isHasMidDirector == true || isHasLowDirector == true)
                    {
                        MidDirector wkr = new MidDirector($"Директорик", $"Департамента_{dep.Id}", "Директор сектора департаментов", dep.Id);
                        wkr.Address = "Какойто адрес в каком то городе";
                        wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                        wkr.CoefSalary = 0.4;
                        wkr.LowSalary = 10000;
                        workers.Add(wkr);
                    }
                }
                //Иначе повторяем все вышесделанное для каждого вложенного департамента
                else
                {
                    foreach (var d in dep.SubDepartaments)
                    {
                        d.Employees = Second_LevelMakerEmployees(count, d);
                    }
                }
            }
            //Заполняем верхний уровень департаментов
            if (dep.ParentId == 1)
            {
                //Обычный персонал. Секретарши и т.п.
                workers = First_LevelMakerEmployees(rnd.Next(3,6), dep);
                for (int i = 0; i < dep.SubDepartaments.Count; i++)
                {
                    TopDirector wkr = new TopDirector($"ТОПДиректор", $"Департамента_{dep.Id}", "Самый главный директор, подчинен царю", dep.Id);
                    wkr.Address = "Какойто адрес в каком то городе";
                    wkr.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                    wkr.CoefSalary = 0.20;
                    wkr.LowSalary = 20000;
                    workers.Add(wkr);
                }
            }
            //Генерим самого главного директора
            if (dep.ParentId == 0)
            {
                workers = First_LevelMakerEmployees(count, dep);
                King king = new King($"Его Величество", dep.Title, "Повелитель всей организации!");
                king.Address = "Какойто адрес в каком то городе";
                king.Birthday = GetRandomDay((DateTime.Now).AddYears(-69), (DateTime.Now).AddYears(-19));
                king.CoefSalary = 0.35;
                king.LowSalary = 100000;
                workers.Add(king);
            }
            return workers;
        }

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
            return start.AddDays(rnd.Next(countDays));
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
