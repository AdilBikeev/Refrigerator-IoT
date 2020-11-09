using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Dtos
{
    /// <summary>
    /// Данные сенсора блока холодильника получаемые от клиента.
    /// </summary>
    public class SensorReeadDto
    {
        /// <summary>
        /// Значение сенсора.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Название сенсора.
        /// </summary>
        public string Name { get; set; }
    }
}
