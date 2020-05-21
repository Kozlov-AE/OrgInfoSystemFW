using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Common
{
    /// <summary>Интерфейс копирования значений между экземплярами</summary>
    /// <typeparam name="T">Тип экземпляра</typeparam>
    interface ICopy<T>
    {
        /// <summary>Копирует значения в другой экземпляр</summary>
        /// <param name="other">Другой экземпляр</param>
        void CopyTo(T other);

        /// <summary>Копирует значения из другого экземпляра</summary>
        /// <param name="other">Другой экземпляр</param>
        void CopyFrom(T other);
    }
}
