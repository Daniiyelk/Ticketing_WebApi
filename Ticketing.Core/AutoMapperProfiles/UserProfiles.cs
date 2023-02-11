using AutoMapper;
using Ticketing.Core.DTOs;
using Ticketing.DataLayer.Entities.User;

namespace Ticketing.Core.AutoMapperProfiles;

public class UserProfiles:Profile
{
    public UserProfiles()
    {
        CreateMap<User,UserCreationDto>();
        CreateMap<UserCreationDto,User>();
    }
}