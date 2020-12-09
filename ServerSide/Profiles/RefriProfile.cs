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
    /// Класс для маппинга данных холодильника - клиенту.
    /// </summary>
    public class RefriProfile : Profile
    {
        public RefriProfile()
        {
            CreateMap<Refrigerator, RefriReeadDto>();
            CreateMap<RefriReeadDto, Refrigerator>();
        }
    }
}
