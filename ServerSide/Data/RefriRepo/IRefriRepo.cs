using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    public interface IRefriRepo
    {
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
        #endregion

        #region RegrigeratorBlocks
        /// <summary>
        /// Обновление указателя на холодильник заданных блоков.
        /// </summary>
        /// <param name="blocksUUID">Список ID блоков, находящихся в холодильнике.</param>
        /// <param name="refrigerator">Холодильник, в которую переместили блоки.</param>
        public void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator);
        #endregion
    }
}
