using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteProvider.Models
{
    public abstract class BaseRemoteClient : IConverterData
    {
        public XmlDocument ConvertData()
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
