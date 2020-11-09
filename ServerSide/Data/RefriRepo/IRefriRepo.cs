using RefrigeratorServerSide.Dtos;
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
        /// Сохраняет изменения в БД.
        /// </summary>
        /// <returns>true - сли изменения успешно сохранились и false - в ином случаи.</returns>
        public bool SaveChanges();

        #region Refrigerator
        /// <summary>
        /// Добавляет в БД инф. о холодильнике.
        /// </summary>
        /// <param name="refrigerator">Данные холодильника.</param>
        public void CreateRefrigerator(Refrigerator refrigerator);

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

        /// <summary>
        /// Обновляем инф. по холодильнику.
        /// </summary>
        /// <param name="refrigerator">Обновленные данные о холодильника.</param>
        /// <param name="refrigeratorUUID">UUID холодильника.</param>
        public void UpdateRefriData(RefriReeadDto refrigerator, string refrigeratorUUID);
        #endregion

        #region RegrigeratorBlock
        /// <summary>
        /// Возвращает инф. о блоке холодильнике по его UUID.
        /// </summary>
        /// <param name="blockUUID">UUID блока холодильника.</param>
        public RefrigeratorBlock GetRefriBlock(string blockUUID);

        /// <summary>
        /// Обновление указателя на холодильник заданных блоков.
        /// </summary>
        /// <param name="blocksUUID">Список ID блоков, находящихся в холодильнике.</param>
        /// <param name="refrigerator">Холодильник, в которую переместили блоки.</param>
        public void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator);

        /// <summary>
        /// Создает в БД инф. о блоке холодильника.
        /// </summary>
        /// <param name="refriBlock">Инф. о блоке холодильника.</param>
        /// <param name="blockUUID">Идентификатор блока.</param>
        public void CreateRefriBlock(RefrigeratorBlock refriBlock, out string blockUUID);

        /// <summary>
        /// Обновляем инф. по блоку холодильника.
        /// </summary>
        /// <param name="refriBlock">Обновленные данные о блоке холодильника.</param>
        public void UpdateRefriBlockData(RefrigeratorBlock refriBlock);

        /// <summary>
        /// Возвращает UUID сенсоров блока холодильника по UUID блока холодильника.
        /// </summary>
        /// <param name="blockUUID">UUID блока холодильника.</param>
        public IList<string> GetRefriBlockSensorsUUID(string blockUUID);
        #endregion

        #region Sensor
        /// <summary>
        /// Обновление указателя на блок холодильника заданных сенсоров.
        /// </summary>
        /// <param name="sensorsIDS">Список ID сенсоров на блоке холодильника.</param>
        /// <param name="refriBlock">Блок холодильника.</param>
        public void UpdSensorsData(IList<string> sensorsIDS, RefrigeratorBlock refriBlock);
        #endregion
    }
}
