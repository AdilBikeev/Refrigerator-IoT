using System;
using System.Collections.Generic;
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

        // GET api/refrigerator
        [HttpGet]
        public ActionResult<Dictionary<string, List<Place>>> GetAllPlaces()
        {
            var repo = this._placeRepo.GetPlaceRefrigerator();

            return Ok(repo);
        }

        // POST api/refrigerator
        [HttpPost]
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