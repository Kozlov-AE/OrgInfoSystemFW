using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public abstract class BasePerson : BaseINotify
    {
        static int globalId;

        int id;
        public int Id => id;

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

        DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged("");
            }
        }

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

        int departamentId;
        public int DepartamentId
        {
            get { return departamentId; }
            set
            {
                departamentId = value;
                OnPropertyChanged("");
            }
        }

        double salary;
        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("");
            }
        }

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

        static BasePerson()
        {
            globalId = 1;
        }

        public BasePerson (string name, string surname, string position, int departamentId = 0)
        {
            this.id = NextID();
            this.Name = name;
            this.Surname = surname;
            this.Position = position;
            this.DepartamentId = departamentId;
        }

        /// <summary>
        /// Метод начисления зарплаты
        /// </summary>
        /// <returns>Зарплата за период</returns>
        public abstract double SalaryPayment();
        /// <summary>
        /// Увеличиваем статичный ID
        /// </summary>
        /// <returns></returns>
        static int NextID()
        {
            globalId++;
            return globalId;
        }
    }
}
