using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Dtos
{
    /// <summary>
    /// Данные о блоке холодильника от клиента.
    /// </summary>
    public class RefriBlockReadDto
    {
        /// <summary>
        /// ID сенсоров на блоке холодильника.
        /// </summary>
        public IList<string> SensorsIDS{ get; set; }

        /// <summary>
        /// Название блока.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание блока.
        /// </summary>
        public string Description { get; set; }
    }
}
