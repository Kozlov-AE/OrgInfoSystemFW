using OrgInfoSystemFW.Model.Departamens;
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

namespace OrgInfoSystemFW.View
{
    /// <summary>
    /// Логика взаимодействия для WorkerView.xaml
    /// </summary>
    public partial class WorkerView : Window
    {
        Worker worker;
        public Worker Worker => worker;

        BaseDepartament departament;

        public WorkerView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вызывается при создании нового интерна
        /// </summary>
        /// <param name="departament"></param>
        public WorkerView(BaseDepartament departament) : base()
        {
            InitializeComponent();
            this.departament = departament;
        }

        /// <summary>
        /// Вызывается при редактировании сотрудника
        /// </summary>
        /// <param name="worker">Редактируемый Воркер</param>
        public WorkerView(Worker worker) : base()
        {
            Title = "Редактирование сотрудника";
            NameTB.Text = worker.Name;
            SurnameTB.Text = worker.Surname;
            BirthDayTB.DisplayDate = worker.Birthday;
            AgeTB.Text = worker.Age.ToString();
            AddressTB.Text = worker.Address;
            PositionTB.Text = worker.Position;
            SalaryTB.Text = worker.Salary.ToString();
            HourTB.Text = worker.WorkHours.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            worker = new Worker(NameTB.Text, SurnameTB.Text, PositionTB.Text, departament);
            worker.Birthday = BirthDayTB.DisplayDate;
            worker.Address = AddressTB.Text;
            worker.Salary = double.Parse(SalaryTB.Text);
            worker.WorkHours = int.Parse(HourTB.Text);
            DialogResult = true;
        }

    }
}
