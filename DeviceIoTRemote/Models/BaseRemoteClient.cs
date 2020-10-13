using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteProvider.Models
{
    /// <summary>
    /// Описывает класс клиентского приложения и действия над его данными.
    /// </summary>
    public abstract class BaseRemoteClient : IConverterData
    {
        public T ConvertData<T>()
        {
            if (typeof(T) == typeof(XmlDocument))
            {
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(stringwriter, this);

                    var xmlData = new XmlDocument();
                    xmlData.LoadXml(stringwriter.ToString());

                    return (T)Convert.ChangeType(xmlData, typeof(T));
                }
            }
            else
            {
                throw new Exception($"Нельзя привести данные к типу {typeof(T)}");
            }
        }
    }
}
