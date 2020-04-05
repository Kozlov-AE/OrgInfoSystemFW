﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    public class Worker : BaseSubordinates
    {
        /// <summary>
        /// Кол-во отработанных часов при почасовой оплате
        /// </summary>
        int workHours;
        public int WorkHours
        {
            get { return workHours; }
            set
            {
                workHours = value;
                OnPropertyChanged("");
            }
        }

        public override double SalaryPayment()
        {
            return WorkHours * Salary;
        }

        public Worker(string name, string surname, string position, int departamentId = 0) : base(name, surname, position, departamentId) { }

    }
}