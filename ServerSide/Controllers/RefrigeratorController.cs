using System;
using System.Collections.Generic;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RefrigeratorServerSide.Data;
using RefrigeratorServerSide.Data.RefriRepo;
using RefrigeratorServerSide.Dtos;
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
        private readonly IMapper _mapper;

        public RefrigeratorController(IPlaceRepo placeRepo, IRefriRepo refriRepo, IMapper mapper)
        {
            this._placeRepo = placeRepo;
            this._refriRepo = refriRepo;
            this._mapper = mapper;
        }
        
        #region HttpGet
        /// <summary>
        /// ���������� ��� ����� � �������������.
        /// </summary>
        /// <response code="200">������ ������� ����������.</response>
        /// <response code="500">����������� ������ � ��.</response>
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
        /// ���������� ������ ������������.
        /// </summary>
        /// <param name="refrigeratorUUID">UUID ������������.</param>
        /// <response code="200">������ ������� ���������� �������.</response>
        /// <response code="403">������� ������ ������ �� UUID ������������ ���������� �������.</response>
        // GET api/refrigerator/{refrigeratorUUID}
        [HttpGet]
        [Route("{refrigeratorUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<RefriReeadDto> GetRefrigerator(string refrigeratorUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(GetRefrigerator)}: refrigeratorUUID={refrigeratorUUID}");

                var refrigerator = _refriRepo.GetRefrigerator(refrigeratorUUID);
                var refriModel = _mapper.Map<RefriReeadDto>(refrigerator);
                refriModel.blockIDS = _refriRepo.GetRefrigeratorBlocksUUID(refrigeratorUUID);

                return Ok(refriModel);
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion

        #region HttpPost
        /// <summary>
        /// ��������� � �� ���. � ����� ������������.
        /// </summary>
        /// <param name="refrigerator">������ ������������.</param>
        /// <response code="200">���. � ������������ ������� ��������� � ��.</response>
        /// <response code="403">������� ���������� ������ � ������������ � �� ����������� �������.</response>
        // POST api/refrigerator
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult CreateRefrigerator(RefriReeadDto refrigerator)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(CreateRefrigerator)}: {JObject.FromObject(refrigerator).ToString()}");
                var modelRefri = _mapper.Map<Refrigerator>(refrigerator);
                _refriRepo.CreateRefrigerator(modelRefri);
                _refriRepo.SaveChanges();
                _refriRepo.UpdBlocksRefriData(refrigerator.blockIDS, modelRefri);
                _refriRepo.SaveChanges();
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }

        /// <summary>
        /// ��������� ������ ������������.
        /// </summary>
        /// <param name="refriDto">����������� ������ ������������.</param>
        /// <param name="refrigeratorUUID">UUID ������������.</param>
        /// <response code="200">������ ������� ���������.</response>
        /// <response code="403">������� ���������� ������ ������������ ���������� �������.</response>
        // POST api/refrigerator/{refrigeratorUUID}
        [HttpPost]
        [Route("{refrigeratorUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult UpdateRefrigerator([FromBody] RefriReeadDto refriDto, [FromRoute] string refrigeratorUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(UpdateRefrigerator)}: refriDto={JObject.FromObject(refriDto)}, refrigeratorUUID={refrigeratorUUID}");

                _refriRepo.UpdateRefriData(refriDto, refrigeratorUUID);
                _refriRepo.SaveChanges();
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion
    }
}