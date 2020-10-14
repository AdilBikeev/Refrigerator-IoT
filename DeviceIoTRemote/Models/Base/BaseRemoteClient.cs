using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        public T SerializeData<T>()
        {
            if (typeof(T) == typeof(XmlDocument))
            {
                return (T)Convert.ChangeType(this.ToXml(), typeof(T));
            }
            else if(typeof(T) == typeof(JObject))
            {
                return (T)Convert.ChangeType(this.ToJson(), typeof(T));
            }
            else if (typeof(T) == typeof(ContentResult))
            {
                return (T)Convert.ChangeType(this.ToHtml(), typeof(T));
            }
            else
            {
                throw new Exception($"Нельзя привести данные к типу {typeof(T)}");
            }
        }

        public BaseRemoteClient DeserializeData<T>(T objData)
        {
            if (typeof(T) == typeof(XmlDocument))
            {
                return this.FromXml(
                    (XmlDocument)Convert.ChangeType(objData, typeof(XmlDocument))
                );
            }
            else if (typeof(T) == typeof(JObject))
            {
                return this.FromJson(
                    (JObject)Convert.ChangeType(objData, typeof(JObject))
                );
            }
            else if (typeof(T) == typeof(ContentResult))
            {
                return this.FromJson(
                    (JObject)Convert.ChangeType(objData, typeof(ContentResult))
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

        public JObject ToJson() => JObject.Parse(JsonConvert.SerializeObject(this));

        public BaseRemoteClient FromJson(JObject json) => (BaseRemoteClient)JsonConvert.DeserializeObject(json.ToString(), this.GetType());

        public ContentResult ToHtml()
        {
            string body = this.ToXml().InnerXml;

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = $"<html><body>{body}</body></html>"
            };
        }

        public BaseRemoteClient FromHtml(ContentResult html)
        {
            var xml = new XmlDocument();
            var regexp = new Regex("<[^<html><body>].+[^</body></html>]>");
            xml.LoadXml(
                regexp.Match(html.Content).Value
            );

            return this.FromXml(xml);
        }
    }
}
