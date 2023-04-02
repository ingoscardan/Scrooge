using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrooge.DbServices.Entities;

/// <summary>
/// The status a notification can have
/// </summary>
public enum NotificationStatus 
{
    Read,
    Unread,
    Deleted
}

/// <summary>
/// Entity to map a Notification with the Database
/// </summary>
public class NotificationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid NotificationId { get; set; }
    public string Message { get; set; }
    public NotificationStatus Status { get; set; }
}