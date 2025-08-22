using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class SupplierPayment
{
    public int PaymentId { get; set; }

    public int? PurchaseId { get; set; }

    public decimal? AmountPaid { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public virtual Purchase? Purchase { get; set; }
}
