using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Common;
using OrgInfoSystemFW.Model.Departamens;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public abstract class BasePerson : BaseINotify, ICloneable<BasePerson>, ICopy<BasePerson>, IEqualsValue<BasePerson>
    {
        static public Dictionary<string, Type> Classes;
        public static int globalId;
        /// <summary>Класс</summary>
        public string Class => this.GetType().Name;
        /// <summary>Уникальный ID</summary>
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
        /// <summary>Имя</summary>
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
        /// <summary>Фамилия</summary>
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
        /// <summary>День рождения</summary>
        DateTime birthday;
        public DateTime Birthday
        {
            get 
            { 
                return birthday.Date; 
            }
            set
            {
                birthday = value;
                OnPropertyChanged("");
            }
        }
        /// <summary>Адрес проживания</summary>
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
        /// <summary>Департамент в котором он находится</summary>
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
        /// <summary>Занимаемая должность</summary>
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
        /// <summary>Вычисляет возраст на основе даты рождения</summary>
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
            if (Classes == null)
            {
                Classes = new Dictionary<string,Type>();
                Classes.Add("Интерн", typeof(Intern));
                Classes.Add("Работник",typeof(Worker));
                Classes.Add("Руководитель департамента",typeof(DepartmentHead));
                Classes.Add("Руководитель ветки департаментов",typeof(LowDirector));
                Classes.Add("Руководитель сетки департаементов",typeof(MidDirector));
                Classes.Add("Руководитель сектора департаементов",typeof(TopDirector));
                Classes.Add("Директор",typeof(King));
            }
        }
        public BasePerson(string name, string surname, string position, BaseDepartament departament, int id = -1) : base()
        {
            if (id == -1)
                this.id = NextID();
            else this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Position = position;
            this.Departament = departament;
        }
        public BasePerson()
        { 
            Birthday = DateTime.Now.AddYears(-20); 
        }

        /// <summary>
        /// Увеличиваем статичный ID
        /// </summary>
        /// <returns></returns>
        static int NextID()
        {
            globalId++;
            return globalId;
        }

        /// <summary>Статичный метод создания нового человека на основе входящего типа</summary>
        /// <param name="type">Класс требуемого сотрудника</param>
        public static BasePerson CreatePerson(Type type)
        {
            BasePerson e;
            switch (type.Name)
            {
                case "Intern":
                    e = new Intern();
                    e.id = NextID();
                    return e;
                case "Worker":
                    e = new Worker();
                    e.id = NextID();
                    return e;
                case "DepartmentHead":
                    e = new DepartmentHead(); 
                    e.id = NextID();
                    return e;
                case "LowDirector":
                    e = new LowDirector();                    
                    e.id = NextID();
                    return e;
                case "MidDirector":
                    e = new MidDirector();                    
                    e.id = NextID();
                    return e;
                case "TopDirector":
                    e = new TopDirector();                   
                    e.id = NextID();
                    return e;
                case "King":
                    e = new King();                    
                    e.id = NextID();
                    return e;
                default:
                    return null;
            }
        }

        #region Реализация ICloneable<BasePerson>
        public BasePerson Clone() => (BasePerson)MemberwiseClone();
        object ICloneable.Clone() => MemberwiseClone();

        public virtual void CopyTo(BasePerson other)
        {
            if (other == null) throw new ArgumentException(nameof(other));
            other.Id = id;
            other.Name = Name;
            other.Surname = Surname;
            other.Position = Position;
            other.Departament = Departament;
            other.Birthday = Birthday;
            other.Address = Address;
        }

        public virtual void CopyFrom(BasePerson other)
        {
            if (other == null) throw new ArgumentException(nameof(other));
            Id = other.id;
            Name = other.Name;
            Surname = other.Surname;
            Position = other.Position;
            Departament = other.Departament;
            Birthday = other.Birthday;
            Address = other.Address;
        }

        public virtual bool EqualsValue(BasePerson other)=>
            Id == other.Id
            && Name == other.Name
            && Surname == other.Surname
            && Position == other.Position
            && Departament == other.Departament
            && Birthday == other.Birthday
            && Address == other.Address;
        #endregion
    }
}
