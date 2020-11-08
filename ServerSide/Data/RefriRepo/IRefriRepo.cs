using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    public interface IRefriRepo
    {
        /// <summary>
        /// Добавляет или обновляет информацию о сенсоре.
        /// </summary>
        /// <param name="sensorData">Сенсор на блоке холодильника.</param>
        public void AddOrUpdateReftInfo(SensorData sensorData);

        /// <summary>
        /// Возвращает инф. о холодильнике по его UUID.
        /// </summary>
        /// <param name="refrigeratorUUID">UUID холодильника.</param>
        public Refrigerator GetRefrigerator(string refrigeratorUUID);

        /// <summary>
        /// Возвращает UUID блоков холодильника по UUID холодильника.
        /// </summary>
        /// <param name="refrigeratorUUID">UUID холодильника.</param>
        public IList<string> GetRefrigeratorBlocksUUID(string refrigeratorUUID);
    }
}
