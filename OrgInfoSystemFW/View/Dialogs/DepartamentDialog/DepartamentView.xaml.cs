using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace OrgInfoSystemFW.View.Dialogs.DepartamentDialog
{
    /// <summary>
    /// Логика взаимодействия для EditMainDepartament.xaml
    /// </summary>
    public partial class DepartamentView : Window, INotifyPropertyChanged
    {
        public BaseDepartament ReturnDepartament { get; set; }
        private string titl;
        private string addr;
        private DateTime birthday;

        #region Поля департамента
            public string Titl
            {
                get => titl;
                set
                {
                    titl = value;
                    OnPropertyChanged();
                }
            }
            public string Addr
            {
                get => addr;
                set
                {
                    addr = value;
                    OnPropertyChanged();
                }
            }
            public DateTime Birthday
            {
                get => birthday;
                set
                {
                    birthday = value;
                    OnPropertyChanged();
                }
            }
        #endregion

        public DepartamentView()
        {
            InitializeComponent();
        }

        public DepartamentView(BaseDepartament dep) : this()
        {
            InitializeControls(dep);
        }

        void InitializeControls(BaseDepartament dep)
        {
            TitleTB.Text = dep.Title;
            if (dep is MainDeportament)
            {
                ReturnDepartament = new MainDeportament();
                BirthDayTB.Visibility = Visibility.Visible;
                AddressTB.Visibility = Visibility.Visible;
                BirthDayTB.Text = (dep as MainDeportament).BirthDay.ToShortDateString();
                AddressTB.Text = (dep as MainDeportament).Address;
            }
            else ReturnDepartament = new Departament();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReturnDepartament == null) ReturnDepartament = new Departament();
            ReturnDepartament.Title = TitleTB.Text;
            if (ReturnDepartament is MainDeportament)
            {
                (ReturnDepartament as MainDeportament).Address = AddressTB.Text;
                (ReturnDepartament as MainDeportament).BirthDay = DateTime.Parse(BirthDayTB.Text);
            }
            this.DialogResult = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
