using RefrigeratorRemote.Models;
using SimulatedDevice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace SimulatedDevice.Mock
{
    /// <summary>
    /// Объект для Mock данных холодильника.
    /// </summary>
    public class RefrigeratorMock : BaseRefrigerator
    {
        /// <summary>
        /// Список доступных имен холодильников.
        /// </summary>
        private readonly List<string> refriNames = new List<string> { "Атлант", "Stinol", "Nord", "Kavkaz", "Toshiba" };

        /// <summary>
        /// Список доступных описаний холодильников.
        /// </summary>
        private readonly List<string> refriDescr= new List<string> { "Холодильник предназначается для бытового использования", "Холодильник является прибором, который не требует надзора при эксплуатации", "В табличке-паспорте, которая находится в нижней части холодильного отделения в правой его части, указана модель холодильника и параметры электропитания", "По причине постоянного усовершенствования конструкций холодильников, настоящее руководство по эксплуатации может не содержать некоторых отличий, которые будут в новом холодильнике", string.Empty };

        protected MockHelper mockHelper = new MockHelper();

        /// <summary>
        /// Объект для шифрования строк.
        /// </summary>
        protected Encrypt encrypt = new Encrypt();

        public RefrigeratorMock(int index)
        {
            this.Refrigerator = new Refrigerator
            {
                Name = this.mockHelper.GetRandomValue(this.refriNames),
                Description = this.mockHelper.GetRandomValue(this.refriDescr),
                RefrigeratorUUID = this.encrypt.GetSHA512(index.ToString())
            };
        }
    }
}
