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
    /// Логика взаимодействия для NewIntern.xaml
    /// </summary>
    public partial class InernView : Window
    {
        Intern intern;
        public Intern Intern => intern;

        BaseDepartament departament;
        public InernView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Вызывается при создании нового интерна
        /// </summary>
        /// <param name="departament"></param>
        public InernView(BaseDepartament departament) : base()
        {
            InitializeComponent();
            this.departament = departament;
        }

        /// <summary>
        /// Вызывается при редактировании сотрудника
        /// </summary>
        /// <param name="intern">Редактируемый Интрен</param>
        public InernView(Intern intern) : base()
        {
            Title = "Редактирование сотрудника";
            NameTB.Text = intern.Name;
            SurnameTB.Text = intern.Surname;
            BirthDayTB.DisplayDate = intern.Birthday;
            AgeTB.Text = intern.Age.ToString();
            AddressTB.Text = intern.Address;
            PositionTB.Text = intern.Position;
            SalaryTB.Text = intern.Salary.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            intern = new Intern(NameTB.Text, SurnameTB.Text, PositionTB.Text, departament);
            intern.Birthday = BirthDayTB.DisplayDate;
            intern.Address = AddressTB.Text;
            intern.Salary = double.Parse(SalaryTB.Text);
            DialogResult = true;
        }
    }
}
