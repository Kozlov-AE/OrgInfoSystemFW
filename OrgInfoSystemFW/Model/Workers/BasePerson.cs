using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public abstract class BasePerson : BaseINotify
    {
        public static int globalId;
        /// <summary>
        /// Класс
        /// </summary>
        public string Class => this.GetType().Name;
        /// <summary>
        /// Уникальный ID
        /// </summary>
        int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Имя
        /// </summary>
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Фамилия
        /// </summary>
        string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// День рождения
        /// </summary>
        DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value.Date;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Адрес проживания
        /// </summary>
        string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Департамент в котором он находится
        /// </summary>
        BaseDepartament departament;
        public BaseDepartament Departament
        {
            get { return departament; }
            set
            {
                departament = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Занимаемая должность
        /// </summary>
        string position;
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Вычисляет возраст на основе даты рождения
        /// </summary>
        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - Birthday.Year;
                if (DateTime.Now.DayOfYear < Birthday.DayOfYear) age--; //на случай, если день рождения ещё не наступил
                return age;   
            }
        }


        /// <summary>
        /// Метод начисления зарплаты
        /// </summary>
        /// <returns>Зарплата за период</returns>
        public abstract double SalaryPayment { get; }

        static BasePerson()
        {
            globalId = 1;
        }
        public BasePerson(string name, string surname, string position, BaseDepartament departament, int id = -1)
        {
            if (id == -1)
                this.id = NextID();
            else this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Position = position;
            this.Departament = departament;
        }
        public BasePerson() { }
        /// <summary>
        /// Увеличиваем статичный ID
        /// </summary>
        /// <returns></returns>
        static int NextID()
        {
            globalId++;
            return globalId;
        }


        #region Методы взаимодействия с департаментами
        /*
         * Устроить человека на работу
         * Уволить сотрудника
         * Повысить сотрудника
         */
        /// <summary>
        /// Добавить сотрудника в департамент
        /// </summary>
        public void AddToDepartament (BaseDepartament departament)
        {
            this.departament = departament;
            departament.Employees.Add(this);
        }

        public void Remove(BaseDepartament departament, BaseDepartament archive)
        {
            archive.Employees.Add(this);
            departament.Employees.Remove(this);
        }
        #endregion
    }
}
