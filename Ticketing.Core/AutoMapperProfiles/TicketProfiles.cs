using AutoMapper;
using Ticketing.Core.DTOs;
using Ticketing.DataLayer.Entities.Ticket;

namespace Ticketing.Core.AutoMapperProfiles;

public class TicketProfiles : Profile
{
    public TicketProfiles()
    {
        CreateMap<Ticket, TicketCreationDto>();
        CreateMap<TicketCreationDto, Ticket>();
    }
}