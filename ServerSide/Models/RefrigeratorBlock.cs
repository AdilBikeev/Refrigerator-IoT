using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Refrigerator.Models
{
    /// <summary>
    /// Класс, описывающий данные блока холодильника.
    /// </summary>
    public class RefrigeratorBlock
    {
        [Required]
        /// <summary>
        /// ID блока.
        /// </summary>
        public string blockUUID { get; set; }

        [Required]
        /// <summary>
        /// Список ID сенсоров в холодильнике.
        /// </summary>
        public List<string> sensorsIDs { get; set; }

        [Required]
        /// <summary>
        /// Название блока в холодильнике.
        /// </summary>
        public string name { get; set; }
    }
}
