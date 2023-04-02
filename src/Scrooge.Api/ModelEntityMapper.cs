using AutoMapper;
using Scrooge.Api.DTOs;
using Scrooge.DbServices.Entities;
using Scrooge.Services.Models;

namespace Scrooge.Services;

public class ModelEntityMapper : Profile
{
    public ModelEntityMapper()
    {
        CreateMap<NotificationEntity, NotificationModel>();
        CreateMap<NotificationModel, NotificationDtoResponse>().ForMember(dest =>
                dest.Status,
            opt => opt.MapFrom(e => e.Status.ToString()));
        CreateMap<NotificationEntity, NotificationDtoResponse>().ForMember( dest => 
            dest.Status,
            opt => opt.MapFrom(e => e.Status.ToString()));
    }
}