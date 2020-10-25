using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteProvider.Models
{
    /// <summary>
    /// Класс, описывающий данные сенсора.
    /// </summary>
    public class SensorData : BaseRemoteClient
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
