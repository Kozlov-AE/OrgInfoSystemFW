using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrgInfoSystemFW.Common
{
    /// <summary>
    /// Интерфейс сравнения по значению с другим экземпляром
    /// </summary>
    /// <typeparam name="T">Тип экземпляра</typeparam>
    interface IEqualsValue<T>
    {
        /// <summary>Сравнивает значения с другим экземпляром</summary>
        /// <param name="other">Другой экземпляр</param>
        bool EqualsValue(T other);
    }
}
