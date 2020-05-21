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

        public EmployeeDialogView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ClassCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var SelectedType = Classes[ClassCB.SelectedValue.ToString()];
            //switch (SelectedType.Name)
            switch (Classes[ClassCB.SelectedValue.ToString()].Name)
            {
                case "Intern":
                    InternControl ic = new InternControl();
                    ic.DataContext = DataContext;
                    contentPresenter.Content = ic;
                    break;
                case "Worker":
                    WorkerControl wc = new WorkerControl();
                    wc.DataContext = DataContext;
                    contentPresenter.Content = wc;
                    break;
                default:
                    DirectorControl dc = new DirectorControl();
                    dc.DataContext = DataContext;
                    contentPresenter.Content = dc;
                    break;
            }
        }
    }
}
