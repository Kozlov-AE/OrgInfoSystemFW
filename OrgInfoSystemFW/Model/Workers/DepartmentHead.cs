﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OrgInfoSystemFW.Model.Departamens;

namespace OrgInfoSystemFW.Model.Workers
{
    public class DepartmentHead : BaseDirector
    {
        public DepartmentHead(string name, string surname, string position, BaseDepartament departament) : base(name, surname, position, departament)
        {
        }

        public override double SalaryPayment
        {
            get
            {
                double sal = 0;
                foreach (var s in Departament.Employees)
                {
                    if (s is BaseSubordinates)
                        sal += (s as BaseSubordinates).Salary * CoefSalary;
                }
                if (sal <= LowSalary && LowSalary != 0) sal = LowSalary;
                return sal;
            }
        }

    }
}
