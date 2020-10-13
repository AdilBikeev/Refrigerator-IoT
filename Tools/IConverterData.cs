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
        /// Возвращает данные в формате XmlDocument.
        /// </summary>
        public XmlDocument ToXml();

        /// <summary>
        /// Возвращает дессириализованный из <see cref="XmlDocument"/> объект текущего класса.
        /// </summary>
        /// <param name="xml">Данные в <see cref="XmlDocument"/> формате.</param>
        /// <returns></returns>
        public OType FromXml(XmlDocument xml);
    }
}
