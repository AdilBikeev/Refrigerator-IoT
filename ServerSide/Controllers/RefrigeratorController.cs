using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RefrigeratorServerSide.Data;
using RefrigeratorServerSide.Models;

namespace RefrigeratorServerSide.Controllers
{
    [Route("api/refrigerator")]
    [ApiController]
    public class RefrigeratorController: ControllerBase
    {
        private readonly IPlaceRepo _placeRepo;

        public RefrigeratorController(IPlaceRepo placeRepo)
        {
            this._placeRepo = placeRepo;
        }

        // GET api/refrigerator
        [HttpGet]
        public ActionResult<Dictionary<string, List<Place>>> GetAllPlaces()
        {
            var repo = this._placeRepo.GetPlaceRefrigerator();

            return Ok(repo);
        }
    }
}