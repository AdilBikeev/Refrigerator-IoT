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
        public  int placeId { get; set; }

        /// <summary>
        /// Название места.
        /// </summary>
        public  string name { get; set; }

        /// <summary>
        /// Идентификатор местаположения.
        /// </summary>
        public  int locationId { get; set; }

        /// <summary>
        /// Давление
        /// </summary>
        /// <value>From 0 to 1</value>
        public  float pressure { get; set; }

        /// <summary>
        /// Тип еды.
        /// </summary>
        public  string foodType { get; set; }
    }
}