using AutoMapper;
using Ticketing.Core.DTOs;
using Ticketing.DataLayer.Entities.TicketAnswer;

namespace Ticketing.Core.AutoMapperProfiles;

public class TicketAnswerProfiles:Profile
{
    public TicketAnswerProfiles()
    {
        CreateMap<TicketAnswerDto, TicketAnswer>();
    }
}