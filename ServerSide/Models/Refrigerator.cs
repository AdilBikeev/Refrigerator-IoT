using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Refrigerator.Models
{
    /// <summary>
    /// Класс, описывающий данные холодильника.
    /// </summary>
    public class Refrigerator
    {
        [Required]
        /// <summary>
        /// ID холодильника.
        /// </summary>
        public string refrigeratorUUID { get; set; }

        [Required]
        /// <summary>
        /// Список из ID блоков в холодильнике.
        /// </summary>
        public List<string> blockIDs { get; set; }

        [Required]
        /// <summary>
        /// Название холодильника.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Название сенсора.
        /// </summary>
        public string description { get; set; }
    }
}
