using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorRemote.Models
{
    /// <summary>
    /// Класс, описывающий данные сенсора.
    /// </summary>
    public class SensorData
    {
        [Key]
        /// <summary>
        /// ID сенсора.
        /// </summary>
        public string SensorUUID { get; set; }

        [Required]
        /// <summary>
        /// Значение сенсора.
        /// </summary>
        public string Value { get; set; }

        [Required]
        /// <summary>
        /// Название сенсора.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Указываем принадлежность сенсора отдельному блоку.
        /// </summary>
        [ForeignKey("BlockUUID")]
        public RefrigeratorBlock RefrigeratorBlock { get; set; }
    }
}
