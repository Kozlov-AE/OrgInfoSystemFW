using Newtonsoft.Json.Linq;
using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Model.Workers
{
    class JsonWorker
    {
        /// <summary>
        /// Возвращает экземпляр класса ввиде объекта JSON
        /// </summary>
        public static JObject GetJObjectPerson(BasePerson person)
        {
            JObject result = new JObject();
            result["Id"] = person.Id;
            result["Class"] = person.Class;
            result["Name"] = person.Name;
            result["Surname"] = person.Surname;
            result["Birthday"] = person.Birthday.Date.ToShortDateString();
            result["Address"] = person.Address;
            result["Departament"] = person.Departament.Id;
            result["Position"] = person.Position;

            //Заполняем различающиеся поля
            if (person is BaseSubordinates)
            {
                result["Salary"] = (person as BaseSubordinates).Salary;
                if (person.GetType() == typeof(Worker))
                {
                    result["WorkHours"] = (person as Worker).WorkHours;
                }
            }
            if (person is BaseDirector)
            {
                result["CoefSalary"] = (person as BaseDirector).CoefSalary;
                result["LowSalary"] = (person as BaseDirector).LowSalary;
            }
            return result;
        }
        /// <summary>
        /// Извлекаем работника из JToken
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        public static BasePerson DeserializePersonFromJSON(JToken jToken)
        {
            string cls = jToken["Class"].ToString();
            int id = int.Parse(jToken["Id"].ToString());
            string name = jToken["Name"].ToString();
            string surname = jToken["Surname"].ToString();
            string birthday = jToken["Birthday"].ToString();
            string position = jToken["Position"].ToString();
            string address = jToken["Address"].ToString();
            switch (cls)
            {
                case "Intern":
                    return new Intern()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Position = position,
                        Address = address,
                        Salary = double.Parse(jToken["Salary"].ToString())
                    };
                case "Worker":
                    return new Worker()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Address = address,
                        Position = position,
                        WorkHours = double.Parse(jToken["WorkHours"].ToString()),
                        Salary = double.Parse(jToken["Salary"].ToString())
                    };
                case "DepartmentHead":
                    return new DepartmentHead()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Address = address,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                case "LowDirector":
                    return new LowDirector()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Address = address,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                case "MidDirector":
                    return new MidDirector()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Address = address,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                case "TopDirector":
                    return new TopDirector()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Address = address,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                case "King":
                    return new King()
                    {
                        Id = id,
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        Address = address,
                        Position = position,
                        CoefSalary = double.Parse(jToken["CoefSalary"].ToString()),
                        LowSalary = double.Parse(jToken["LowSalary"].ToString())
                    };
                default:
                    return new Intern()
                    {
                        Name = name,
                        Surname = surname,
                        Position = position,
                        Salary = 0
                    };
            }
        }

        /// <summary>
       /// Преобразует экземпляр калсса BaseDepartament в JObject
       /// </summary>
       /// <param name="departament">Сериализуемый департамент</param>
        public static JObject GetJObjectDepartament(BaseDepartament departament)
        {
            JObject result = new JObject();
            result["Id"] = departament.Id;
            result["ParentId"] = departament.ParentId;
            result["Title"] = departament.Title;
            //Заполняем различающиеся поля
            if (departament is Departament)
            {
                result["Class"] = "Departament";
            }
            if (departament is MainDeportament)
            {
                result["BirthDay"] = (departament as MainDeportament).BirthDay;
                result["Address"] = (departament as MainDeportament).Address;
            }
            JArray employees = new JArray();
            foreach (var e in departament.Employees)
            {
                employees.Add(GetJObjectPerson(e));
            }
            result["Employees"] = employees;
            return result;
        }
        /// <summary>
        /// извлекает Департамент с сотрудниками из JToken
        /// </summary>
        public static BaseDepartament DeserializeDepartamentFromJSON(JToken jToken)
        {
            int id = int.Parse(jToken["Id"].ToString());
            string cls = jToken["Class"].ToString();
            string title = jToken["Title"].ToString();
            switch (cls)
            {
                case "Departament":
                    var d = new Departament();
                    d.Id = id;
                    d.Title = title;
                    d.Employees = new ObservableCollection<BasePerson>();
                    foreach (var e in jToken["Employees"].ToArray())
                    {
                        var w = DeserializePersonFromJSON(e);
                        w.Departament = d;
                        d.Employees.Add(w);
                    }
                    return d;
                case "MainDeportament":
                    var md = new MainDeportament();
                    md.Id = id;
                    md.Title = title;
                    md.BirthDay = DateTime.Parse(jToken["BirthDay"].ToString());
                    md.Address = jToken["Address"].ToString();
                    md.Employees = new ObservableCollection<BasePerson>();
                    foreach (var e in jToken["Employees"].ToArray())
                    {
                        var w = DeserializePersonFromJSON(e);
                        w.Departament = md;
                        md.Employees.Add(w);
                    }

                    return md;
                default:
                    return new Departament()
                    {
                        Title = title,
                        ParentId = 0
                    };
            }
        }

        /// <summary>
        /// Сериализует департамент вместе с вложенными департаментами
        /// </summary>
        public static JObject SerializeDepartamentWithSub(BaseDepartament departament)
        {
            JObject result = new JObject();
            result["Id"] = departament.Id;
            result["Class"] = departament.GetType().Name;
            result["ParentId"] = departament.ParentId;
            result["Title"] = departament.Title;
            //Заполняем различающиеся поля
            if (departament.GetType() == typeof(MainDeportament))
            {
                result["BirthDay"] = (departament as MainDeportament).BirthDay;
                result["Address"] = (departament as MainDeportament).Address;
                result["Class"] = "MainDeportament";
            }
            if (departament.GetType() == typeof(Departament))
            {
                result["Class"] = "Departament";
            }
            JArray employees = new JArray();
            foreach (var e in departament.Employees)
            {
                employees.Add(GetJObjectPerson(e));
            }
            result["Employees"] = employees;
            JArray departaments = new JArray();
            if (departament.SubDepartaments.Count > 0)
            {
                foreach (var d in departament.SubDepartaments)
                {
                    departaments.Add(SerializeDepartamentWithSub(d));
                }
            }
            result["SubDepartaments"] = departaments;
            return result;
        }


        public static BaseDepartament DeserealizeDepartamentWithSub(JToken jToken)
        {
            string cls = jToken["Class"].ToString();
            int id = int.Parse(jToken["Id"].ToString());
            string title = jToken["Title"].ToString();
            switch (cls)
            {
                case "Departament":
                    var d = new Departament();
                    d.Id = id;
                    d.Title = title;
                    d.Employees = new ObservableCollection<BasePerson>();
                    //d.SubDepartaments.Add(DeserializeDepartamentFromJSON(dep));
                    foreach (var e in jToken["Employees"].ToArray())
                    {
                        var w = DeserializePersonFromJSON(e);
                        w.Departament = d;
                        d.Employees.Add(w);
                    }
                    d.SubDepartaments = new ObservableCollection<BaseDepartament>();
                    if (jToken["SubDepartaments"].ToArray().Length > 0)
                    {
                        foreach (var dep in jToken["SubDepartaments"].ToArray())
                        {
                            d.SubDepartaments.Add(DeserealizeDepartamentWithSub(dep));
                        }
                    }
                    return d;
                case "MainDeportament":
                    var md = new MainDeportament();
                    md.Id = id;
                    md.Title = title;
                    md.BirthDay = DateTime.Parse(jToken["BirthDay"].ToString());
                    md.Address = jToken["Address"].ToString();
                    md.ParentId = 0;
                    md.Employees = new ObservableCollection<BasePerson>();
                    foreach (var e in jToken["Employees"].ToArray())
                    {
                        var w = DeserializePersonFromJSON(e);
                        w.Departament = md;
                        md.Employees.Add(w);
                    }
                    md.SubDepartaments = new ObservableCollection<BaseDepartament>();
                    if (jToken["SubDepartaments"].ToArray().Length > 0)
                    {
                        foreach (var dep in jToken["SubDepartaments"].ToArray())
                        {
                            md.SubDepartaments.Add(DeserealizeDepartamentWithSub(dep));
                        }
                    }
                    return md;
                default:
                    return new Departament()
                    {
                        Title = title,
                        ParentId = 0
                    };
            }

        }


    }
}
