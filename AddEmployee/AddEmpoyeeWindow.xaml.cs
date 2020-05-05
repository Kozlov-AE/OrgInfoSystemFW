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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddEmployee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, Type> Classes => BasePerson.Classes;
        public BasePerson person;
        Type SelectedType;
        public MainWindow()
        {
            InitializeComponent();
            ClassCB.ItemsSource = Classes.Keys;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ClassCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedType = Classes[ClassCB.SelectedValue.ToString()];
            switch (SelectedType.Name)
            {
                case  "Intern":
                    AddInternControl ic = new AddInternControl();
                    contentPresenter.Content = ic;
                    Intern p = new Intern();
                    p.Name = NameTB.Text;
                    p.Surname = SurnameTB.Text;
                    p.Birthday = BirthDayTB.DisplayDate;
                    p.Address = AddressTB.Text;
                    p.Position = PositionTB.Text;
                    p.Salary = double.Parse(ic.SalaryTB.Text);
                    person = p;
                    break;
                case "Worker":
                    contentPresenter.Content = new AddWorkerControl();
                    break;
                default:
                    contentPresenter.Content = new DirectorControl();
                    break;
            }
        }
    }
}
