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
    /// Логика взаимодействия для EditMainDepartament.xaml
    /// </summary>
    public partial class EditMainDepartament : Window
    {
        public string Titl { get; set; }
        public string Addr { get; set; }
        public DateTime Birthday { get; set; }
        public EditMainDepartament(MainDeportament md)
        {
            InitializeComponent();
            TitleTB.Text = md.Title;
            BirthDayTB.Text = md.BirthDay.ToShortDateString();
            AddressTB.Text = md.Address;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Titl = TitleTB.Text;
            Addr = AddressTB.Text;
            Birthday = DateTime.Parse(BirthDayTB.Text);
            this.DialogResult = true;
        }
    }
}
