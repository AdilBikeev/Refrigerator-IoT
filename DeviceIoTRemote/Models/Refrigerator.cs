using System.ComponentModel.DataAnnotations;

namespace RemoteProvider.Models
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
