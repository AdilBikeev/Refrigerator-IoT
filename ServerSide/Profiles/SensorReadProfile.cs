using AutoMapper;
using RefrigeratorServerSide.Dtos;
using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Profiles
{
    /// <summary>
    /// Класс для маппинга данных сенсора блока холодильника - клиенту.
    /// </summary>
    public class SensorReadProfile : Profile
    {
        public SensorReadProfile()
        {
            CreateMap<SensorData, SensorReeadDto>();
            CreateMap<SensorReeadDto, SensorData>();
        }
    }
}
