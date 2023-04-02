using Scrooge.DbServices.Entities;

namespace Scrooge.Services.Models;

public class NotificationModel
{
    public Guid NotificationId { get; set; }
    public string Message { get; set; }
    public NotificationStatus Status { get; set; }
}