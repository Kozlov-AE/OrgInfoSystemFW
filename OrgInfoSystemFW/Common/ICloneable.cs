using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystemFW.Common
{
    /// <summary>Возвращает копию экземпляра заданного типа</summary>
    /// <typeparam name="T">Тип экземпляра</typeparam>
    public interface ICloneable<T> : ICloneable
    {
        /// <summary>Возвращает копию экземпляра</summary>
        /// <returns>Копия заданного типа</returns>
        new T Clone();
    }
}
