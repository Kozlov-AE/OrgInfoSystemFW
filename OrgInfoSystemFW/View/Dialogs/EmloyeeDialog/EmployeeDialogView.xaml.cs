using OrgInfoSystemFW.Model.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrgInfoSystemFW.View.Dialogs.EmloyeeDialog
{
    /// <summary>
    /// Логика взаимодействия для EmployeeDialogView.xaml
    /// </summary>
    public partial class EmployeeDialogView : Window
    {
        Dictionary<string, Type> Classes => BasePerson.Classes;
        public BasePerson person;
        Type SelectedType = null;

        public EmployeeDialogView()
        {
            InitializeComponent();
            ClassCB.ItemsSource = Classes.Keys;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ClassCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedType = Classes[ClassCB.SelectedValue.ToString()];
            switch (SelectedType.Name)
            {
                case "Intern":
                    InternControl ic = new InternControl();
                    contentPresenter.Content = ic;
                    Intern p = new Intern();
                    p.Name = NameTB.Text;
                    p.Surname = SurnameTB.Text;
                    p.Birthday = BirthDayTB.DisplayDate;
                    p.Address = AddressTB.Text;
                    p.Position = PositionTB.Text;
                    p.Salary = ConvertTbToDouble(ic.SalaryTB.Text);
                    person = p;
                    break;
                case "Worker":
                    WorkerControl wc = new WorkerControl();
                    contentPresenter.Content = wc;
                    Worker w = new Worker();
                    w.Name = NameTB.Text;
                    w.Surname = SurnameTB.Text;
                    w.Birthday = BirthDayTB.DisplayDate;
                    w.Address = AddressTB.Text;
                    w.Position = PositionTB.Text;
                    w.WorkHours = ConvertTbToDouble(wc.workHoursTB.Text);
                    w.Salary = ConvertTbToDouble(wc.SalaryTB.Text);
                    person = w;
                    break;
                default:
                    DirectorControl dc = new DirectorControl();
                    contentPresenter.Content = dc;
                    switch (SelectedType.Name)
                    {
                        case "DepartmentHead":
                            DepartmentHead dh = new DepartmentHead();
                            dh.Name = NameTB.Text;
                            dh.Surname = SurnameTB.Text;
                            dh.Birthday = BirthDayTB.DisplayDate;
                            dh.Address = AddressTB.Text;
                            dh.Position = PositionTB.Text;
                            dh.CoefSalary  = ConvertTbToDouble(dc.coefSalaryTB.Text);
                            dh.LowSalary = ConvertTbToDouble(dc.lowSalaryTB.Text);
                            person = dh;
                            break;
                        case "LowDirector":
                            LowDirector ld = new LowDirector();
                            ld.Name = NameTB.Text;
                            ld.Surname = SurnameTB.Text;
                            ld.Birthday = BirthDayTB.DisplayDate;
                            ld.Address = AddressTB.Text;
                            ld.Position = PositionTB.Text;
                            ld.CoefSalary = ConvertTbToDouble(dc.coefSalaryTB.Text);
                            ld.LowSalary = ConvertTbToDouble(dc.lowSalaryTB.Text);
                            person = ld;
                            break;
                        case "MidDirector":
                            MidDirector md = new MidDirector();
                            md.Name = NameTB.Text;
                            md.Surname = SurnameTB.Text;
                            md.Birthday = BirthDayTB.DisplayDate;
                            md.Address = AddressTB.Text;
                            md.Position = PositionTB.Text;
                            md.CoefSalary = ConvertTbToDouble(dc.coefSalaryTB.Text);
                            md.LowSalary = ConvertTbToDouble(dc.lowSalaryTB.Text);
                            person = md;
                            break;
                        case "TopDirector":
                            TopDirector td = new TopDirector();
                            td.Name = NameTB.Text;
                            td.Surname = SurnameTB.Text;
                            td.Birthday = BirthDayTB.DisplayDate;
                            td.Address = AddressTB.Text;
                            td.Position = PositionTB.Text;
                            td.CoefSalary = ConvertTbToDouble(dc.coefSalaryTB.Text);
                            td.LowSalary = ConvertTbToDouble(dc.lowSalaryTB.Text);
                            person = td;
                            break;
                        case "King":
                            King k = new King();
                            k.Name = NameTB.Text;
                            k.Surname = SurnameTB.Text;
                            k.Birthday = BirthDayTB.DisplayDate;
                            k.Address = AddressTB.Text;
                            k.Position = PositionTB.Text;
                            k.CoefSalary = ConvertTbToDouble(dc.coefSalaryTB.Text);
                            k.LowSalary = ConvertTbToDouble(dc.lowSalaryTB.Text);
                            person = k;
                            break;
                    }
                    break;
            }
        }



        /// <summary>
        /// Возвращает из строки либо дабл, либо ноль
        /// </summary>
        /// <param name="value">Входная строка</param>
        double ConvertTbToDouble (string value)
        {
            double result = double.TryParse(value, out result) ? result : 0;
            return result;
        }
    }
}
