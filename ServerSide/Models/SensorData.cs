using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Refrigerator.Models
{
    /// <summary>
    /// Класс, описывающий данные сенсора.
    /// </summary>
    public class SensorData
    {
        [Required]
        /// <summary>
        /// ID сенсора.
        /// </summary>
        public string sensorUUID { get; set; }

        [Required]
        /// <summary>
        /// Значение сенсора.
        /// </summary>
        public string value { get; set; }

        [Required]
        /// <summary>
        /// Название сенсора.
        /// </summary>
        public string name { get; set; }
    }
}
