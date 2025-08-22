using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? ContactInfo { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
