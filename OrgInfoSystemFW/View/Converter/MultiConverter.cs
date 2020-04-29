using OrgInfoSystemFW.Model.Departamens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OrgInfoSystemFW.View.Converter
{
    class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Tuple<string, BaseDepartament> tuple = new Tuple<string, BaseDepartament>((string)values[0], (BaseDepartament)values[1]);
            return tuple;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
