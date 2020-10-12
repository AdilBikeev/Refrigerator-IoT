using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteProvider.Models
{
    /// <summary>
    /// Класс, описывающий данные блока холодильника.
    /// </summary>
    public class RefrigeratorBlock
    {
        [Key]
        /// <summary>
        /// ID блока.
        /// </summary>
        public string BlockUUID { get; set; }

        [Required]
        /// <summary>
        /// Название блока в холодильнике.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Указываем принадлежность блока отдельному холодильнику.
        /// </summary>
        [ForeignKey("RefrigeratorUUID")]
        public Refrigerator Refrigerator { get; set; }
    }
}
