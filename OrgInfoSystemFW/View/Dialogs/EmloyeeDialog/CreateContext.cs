using OrgInfoSystemFW.Command;
using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.View.Dialogs.EmloyeeDialog
{
    class CreateContext : INotifyPropertyChanged
    {
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

        #region Работники
        /// <summary>
        /// Зарплата
        /// </summary>
        double salary;
        public string Salary
        {
            get { return salary.ToString(); }
            set
            {
                salary = ConvertTbToDouble(value);
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Зарплата
        /// </summary>
        /// <summary>
        /// Кол-во отработанных часов при почасовой оплате
        /// </summary>
        double workHours;
        public string WorkHours
        {
            get { return workHours.ToString(); }
            set
            {
                workHours = ConvertTbToDouble(value);
                OnPropertyChanged("");
            }
        }
        #endregion

        #region Для управленцев
        /// <summary>
        /// Зарплатный коэфициент
        /// </summary>
        protected double coefSalary;
        public string CoefSalary
        {
            get { return coefSalary.ToString(); }
            set
            {
                coefSalary = ConvertTbToDouble(value);
                OnPropertyChanged("");
            }
        }
        /// <summary>
        /// Минимальная ЗП
        /// </summary>
        public double lowSalary = 0;
        public string LowSalary
        {
            get { return lowSalary.ToString(); }
            set
            {
                lowSalary = ConvertTbToDouble(value);
                OnPropertyChanged("");
            }
        }
        #endregion

        
        
        public CreateContext(BaseDepartament departament)
        {
            Departament = departament;
        }

        /// <summary>
        /// Возвращает из строки либо дабл, либо ноль
        /// </summary>
        /// <param name="value">Входная строка</param>
        double ConvertTbToDouble(string value)
        {
            double result = double.TryParse(value, out result) ? result : 0;
            return result;
        }

        RelayCommand okBtnClick;


        /// <summary>
        /// Редактировать выделенный депатамент
        /// </summary>
        public RelayCommand OkBtnClick
        {
            get
            {
                return okBtnClick ??
                  (okBtnClick = new RelayCommand(obj =>
                  {
                      if (Departament.GetType() == typeof(Departament))
                      {
                          Departament d = SelectedDepartament as Departament;
                          EditDepartament ed = new EditDepartament(d.Title);
                          if (ed.ShowDialog() == true)
                          {
                              d.Title = ed.NewTitle;
                          }
                      }
                      else
                      {
                          MainDeportament md = SelectedDepartament as MainDeportament;
                          EditMainDepartament emd = new EditMainDepartament(md);
                          if (emd.ShowDialog() == true)
                          {
                              md.Title = emd.Titl;
                              md.Address = emd.Addr;
                              md.BirthDay = emd.Birthday;
                          }
                      }
                  }, _ => SelectedDepartament != null));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
