using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace Tools
{
    public interface IConverterData<OType>
    {
        /// <summary>
        /// Возвращает десериализованный объект текущего класса из формата <typeparamref name="T"/>.
        /// </summary>
        public OType DeserializeData<T>(T objData);

        /// <summary>
        /// Возвращает данные в формате <typeparamref name="T"/>.
        /// </summary>
        public T SerializeData<T>();

        /// <summary>
        /// Возвращает данные в формате <see cref="XmlDocument"/>.
        /// </summary>
        public XmlDocument ToXml();

        /// <summary>
        /// Возвращает дессириализованный из <see cref="XmlDocument"/> объект текущего класса.
        /// </summary>
        /// <param name="xml">Данные в <see cref="XmlDocument"/> формате.</param>
        public OType FromXml(XmlDocument xml);

        /// <summary>
        /// Возвращает данные в формате <see cref="JObject"/>.
        /// </summary>
        public JObject ToJson();

        /// <summary>
        /// Десериализует json в объект текущего класса. 
        /// </summary>
        /// <param name="json">Json для десериализации.</param>
        public OType FromJson(JObject json);

        /// <summary>
        /// Возвращает данные в формате <see cref="ContentResult"/>.
        /// </summary>
        public ContentResult ToHtml();

        /// <summary>
        /// Десериализует HTML в объект текущего класса. 
        /// </summary>
        /// <param name="ContentResult">HTML для десериализации.</param>
        public OType FromHtml(ContentResult html);
    }
}
