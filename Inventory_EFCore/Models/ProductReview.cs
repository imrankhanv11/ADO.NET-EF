using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class ProductReview
{
    public int ReviewId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateOnly? ReviewDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
