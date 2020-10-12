namespace RemoteProvider.Models
{
    public abstract class BaseRefrigerator
    {
        /// <summary>
        /// Объект холодильника.
        /// </summary>
        public Refrigerator Refrigerator { get; protected set; }
    }
}
