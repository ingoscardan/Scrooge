using Microsoft.EntityFrameworkCore;
using Scrooge.DbServices.Entities;

namespace Scrooge.DbServices.DBContext;

public class ScroogeDbContext : DbContext
{
    public ScroogeDbContext(DbContextOptions<ScroogeDbContext> options)
        : base(options)
    {
    }

    private DbSet<NotificationEntity> Notifications { get; set; }
    private DbSet<BillToPay> BillsToPay { get; set; }
}
