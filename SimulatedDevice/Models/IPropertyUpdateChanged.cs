using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedDevice.Models
{
    interface IPropertyUpdateChanged
    {
        /// <summary>
        /// Обновляет данные сущности.
        /// </summary>
        public void Update();
    }
}
