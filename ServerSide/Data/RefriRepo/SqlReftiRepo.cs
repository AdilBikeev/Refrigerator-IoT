using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RefrigeratorServerSide.Dtos;
using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    /// <summary>
    /// Класс для взаимодействия с БД холодильника с помощью EF Core.
    /// </summary>
    public class SqlReftiRepo : BaseRefriRepo
    {
        public SqlReftiRepo(RefrigeratorContext context, IMapper mapper) : base(context, mapper) { }
    }
}
