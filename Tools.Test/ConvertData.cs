using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RemoteProvider.Models;
using SimulatedDevice.Mock;
using System.Xml;

namespace Tools.Test
{
    public class ConvertData
    {
        BaseSensorData remoteClient;

        [SetUp]
        public void Setup()
        {
            remoteClient = new SensorDataMock(
                0,
                new RefrigeratorBlockMock(
                   0,
                   new RefrigeratorMock(0)
                )
            );
        }

        [Test]
        [Description("Проверяем корректность работы сериализации")]
        public void SerializeTest()
        {
            var xml = remoteClient.SensorData.SerializeData<XmlDocument>();
            var json = remoteClient.SensorData.SerializeData<JObject>();
            var html = remoteClient.SensorData.SerializeData<ContentResult>();
                
            Assert.Pass();
        }

        [Test]
        [Description("Проверяем корректность работы десериализации")]
        public void DeserializeTest()
        {
            var xml = remoteClient.SensorData.SerializeData<XmlDocument>();
            var json = remoteClient.SensorData.SerializeData<JObject>();
            var html = remoteClient.SensorData.SerializeData<ContentResult>();

            var xmlObj = remoteClient.SensorData.DeserializeData(xml);
            var jsonObj = remoteClient.SensorData.DeserializeData(json);
            var htmlObj = remoteClient.SensorData.DeserializeData(html);

            Assert.Pass();
        }
    }
}