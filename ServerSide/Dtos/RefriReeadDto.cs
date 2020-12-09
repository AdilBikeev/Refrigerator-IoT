using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Dtos
{
    /// <summary>
    /// Данные холодильника для отправки клиенту.
    /// </summary>
    public class RefriReeadDto
    {
        /// <summary>
        /// ID блоков холодильника.
        /// </summary>
        public IList<string> blockIDS { get; set; }

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
