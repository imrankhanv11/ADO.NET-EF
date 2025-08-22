using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? ContactInfo { get; set; }

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
