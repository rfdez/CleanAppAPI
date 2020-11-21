using AutoMapper;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Year, YearDto>();
            CreateMap<YearDto, Year>();
            CreateMap<Cleanliness, CleanlinessDto>();
            CreateMap<CleanlinessDto, Cleanliness>();
            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>();
            CreateMap<Month, MonthDto>();
            CreateMap<MonthDto, Month>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
            CreateMap<Tenant, TenantDto>();
            CreateMap<TenantDto, Tenant>();
            CreateMap<Week, WeekDto>();
            CreateMap<WeekDto, Week>();
        }
    }
}
