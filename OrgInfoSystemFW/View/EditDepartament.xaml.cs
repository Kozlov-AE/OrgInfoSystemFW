using OrgInfoSystemFW.Model.Departamens;
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
    /// Логика взаимодействия для EditDepartament.xaml
    /// </summary>
    public partial class EditDepartament : Window
    {
        public string Txt { get; set; }
        public EditDepartament(string text)
        {
            InitializeComponent();
            TextBox.Text = text;
            Txt = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Txt = TextBox.Text;
            this.DialogResult = true;
        }
    }
}
