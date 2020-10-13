using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Tools;

namespace RemoteProvider.Models
{
    /// <summary>
    /// Описывает класс клиентского приложения и действия над его данными.
    /// </summary>
    public abstract class BaseRemoteClient : IConverterData<BaseRemoteClient>
    {
        public BaseRemoteClient DeserializeData<T>(T objData)
        {
            if (typeof(T) == typeof(XmlDocument))
            {
                return this.FromXml(
                    (XmlDocument)Convert.ChangeType(objData, typeof(XmlDocument))
                );
            }
            else
            {
                throw new Exception($"Нельзя привести данные к типу {typeof(T)}");
            }
        }

        public BaseRemoteClient FromXml(XmlDocument xml)
        {
            using (var stringReader = new System.IO.StringReader(xml.InnerXml))
            {
                var serializer = new XmlSerializer(this.GetType());
                return (BaseRemoteClient)serializer.Deserialize(stringReader);
            }
        }

        public T SerializeData<T>()
        {
            if (typeof(T) == typeof(XmlDocument))
            {
                return (T)Convert.ChangeType(this.ToXml(), typeof(T));
            }
            else
            {
                throw new Exception($"Нельзя привести данные к типу {typeof(T)}");
            }
        }

        public XmlDocument ToXml()
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stringwriter, this);

                var xmlData = new XmlDocument();
                xmlData.LoadXml(stringwriter.ToString());

                return xmlData;
            }
        }
    }
}
