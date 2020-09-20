namespace Refrigerator.Models 
{
    /// <summary>
    /// Место в холодильнике.
    /// </summary>
    public class Place
    {
        /// <summary>
        /// идентификатор места.
        /// </summary>
        private int _placeId { get; set; }

        /// <summary>
        /// Название места.
        /// </summary>
        private string _name { get; set; }

        /// <summary>
        /// Идентификатор местаположения.
        /// </summary>
        private string _locationId { get; set; }

        /// <summary>
        /// Давление
        /// </summary>
        /// <value>From 0 to 1</value>
        private string _pressure { get; set; }

        /// <summary>
        /// Тип еды.
        /// </summary>
        private string _foodType { get; set; }
    }
}