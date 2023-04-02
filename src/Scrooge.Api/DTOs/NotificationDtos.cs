using Scrooge.DbServices.Entities;

namespace Scrooge.Api.DTOs;

public class CreateNotification
{
    public string Message { get; set; }
}

public class NotificationDtoResponse
{
    public Guid NotificationId { get; set; }
    public string Message { get; set; }

    public string Status { get; set; }
}