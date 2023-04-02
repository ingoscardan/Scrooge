using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Scrooge.DbServices.Entities;

public enum BillToPayStatus
{
    Pending = 1,
    Paid
}

public class BillToPay
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid BillToPayId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public BillToPayStatus Status { get; set; }
    public DateOnly StartPeriod { get; set; }
    public DateOnly EndPeriod { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly ApplicationDate { get; set; }
    public string Category { get; set; }
}