namespace RemoteProvider.Models
{
    public abstract class BaseSensorData : IPropertyUpdateChanged
    {
        /// <summary>
        /// Объект сенсора на блоке холодильника.
        /// </summary>
        public SensorData SensorData { get; protected set; }

        public abstract void Update();
    }
}
