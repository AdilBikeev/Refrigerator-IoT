using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RefrigeratorServerSide.Data;
using RefrigeratorServerSide.Data.RefriRepo;
using RefrigeratorServerSide.Models;
using RemoteProvider.Models;

namespace RefrigeratorServerSide.Controllers
{
    [Route("api/refrigerator")]
    [ApiController]
    public class RefrigeratorController: ControllerBase
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IRefriRepo _refriRepo;

        public RefrigeratorController(IPlaceRepo placeRepo, IRefriRepo refriRepo)
        {
            this._placeRepo = placeRepo;
            this._refriRepo = refriRepo;
        }

        /// <summary>
        /// Возвращает все места в холодильниках.
        /// </summary>
        /// <response code="200">Данные успешно возвращены.</response>
        /// <response code="500">Отсутствуют данные в БД.</response>
        // GET api/refrigerator
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Dictionary<string, List<Place>>> GetAllPlaces()
        {
            var repo = this._placeRepo.GetPlaceRefrigerator();

            return Ok(repo);
        }

        /// <summary>
        /// Обновляет данные сенсоров холодильника.
        /// </summary>
        /// <param name="sensorData">Данные сенсора.</param>
        /// <response code="200">Данные успешно обновлены в БД.</response>
        /// <response code="403">Процесс онбволения данных с холодильника в БД завершились исключением.</response>
        // POST api/refrigerator
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<string> UpdateRefrigeratorData(SensorData sensorData)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(UpdateRefrigeratorData)}: {JObject.FromObject(sensorData).ToString()}");

                _refriRepo.AddOrUpdateReftInfo(sensorData);
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
    }
}