using AutoMapper;
using Ticketing.Core.DTOs;
using Ticketing.DataLayer.Entities.Admin;

namespace Ticketing.Core.AutoMapperProfiles;

public class AdminProfiles :Profile
{
    public AdminProfiles()
    {
        CreateMap<AdminDto, Admin>();
    }
}