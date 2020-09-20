using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Refrigerator.Data;
using Refrigerator.Models;

namespace Refrigerator.Controllers
{
    [Route("api/refrigerator")]
    [ApiController]
    public class RefrigeratorController: ControllerBase
    {
        private readonly MockPlaceRepo _mockPlaceRepo = new MockPlaceRepo();

        // GET api/refrigerator
        [HttpGet]
        public ActionResult<Dictionary<string, Place>> GetAllPlaces()
        {
            var repo = this._mockPlaceRepo.GetPlaceRefrigerator();

            return Ok(repo);
        }
    }
}