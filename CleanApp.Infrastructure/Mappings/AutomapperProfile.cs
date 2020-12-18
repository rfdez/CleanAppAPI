using AutoMapper;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;

namespace CleanApp.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Year, YearDto>().ReverseMap();
            CreateMap<Cleanliness, CleanlinessDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Month, MonthDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Tenant, TenantDto>().ReverseMap();
            CreateMap<Week, WeekDto>().ReverseMap();
            CreateMap<Authentication, AuthenticationDto>().ReverseMap();
            CreateMap<HomeDto, Home>().ReverseMap();
            CreateMap<HomeTenantDto, HomeTenant>().ReverseMap();
        }
    }
}
