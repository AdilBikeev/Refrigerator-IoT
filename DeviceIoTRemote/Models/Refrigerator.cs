using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorRemote.Models
{
    /// <summary>
    /// Класс, описывающий данные холодильника.
    /// </summary>
    public class Refrigerator
    {
        [Key]
        /// <summary>
        /// ID холодильника.
        /// </summary>
        public string RefrigeratorUUID { get; set; }

        [Required]
        /// <summary>
        /// Название холодильника.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Название сенсора.
        /// </summary>
        public string Description { get; set; }
    }
}
