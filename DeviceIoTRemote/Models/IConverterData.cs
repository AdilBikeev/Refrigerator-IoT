using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace RemoteProvider.Models
{
    public interface IConverterData
    {
        /// <summary>
        /// Возвращает данные в формате <see cref="XmlDocument"/>.
        /// </summary>
        public XmlDocument ConvertData(); 
    }
}
