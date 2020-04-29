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
    /// Логика взаимодействия для DirectorView.xaml
    /// </summary>
    public partial class DirectorView : Window
    {
        BaseDepartament departament;
        BaseDirector director;
        public BaseDirector Director => director;

        public DirectorView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вызывается при создании нового интерна
        /// </summary>
        /// <param name="departament"></param>
        public DirectorView(BaseDepartament departament) : base()
        {
            InitializeComponent();
            this.departament = departament;
        }

        /// <summary>
        /// Вызывается при редактировании сотрудника
        /// </summary>
        /// <param name="worker">Редактируемый директор</param>
        public DirectorView(BaseDirector director) : base()
        {
            Title = "Редактирование сотрудника";
            NameTB.Text = director.Name;
            SurnameTB.Text = director.Surname;
            BirthDayTB.DisplayDate = director.Birthday;
            AgeTB.Text = director.Age.ToString();
            AddressTB.Text = director.Address;
            PositionTB.Text = director.Position;
            MinSalaryTB.Text = director.LowSalary.ToString();
            CoefTB.Text = director.CoefSalary.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            director = new DepartmentHead (NameTB.Text, SurnameTB.Text, PositionTB.Text, departament);
            director.Birthday = BirthDayTB.DisplayDate;
            director.Address = AddressTB.Text;
            director.LowSalary = double.Parse(MinSalaryTB.Text);
            director.CoefSalary = double.Parse(CoefTB.Text);
            DialogResult = true;
        }
    }
}
