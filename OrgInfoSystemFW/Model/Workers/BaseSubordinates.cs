﻿using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    abstract public class BaseSubordinates : BasePerson
    {
        protected double salary;
        private BaseDepartament departament;

        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("");
            }
        }

        public BaseSubordinates(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }
    }
}
