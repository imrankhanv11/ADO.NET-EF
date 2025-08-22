using System;
using System.Collections.Generic;

namespace EFCore_DBFirstApp.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string? PermissionName { get; set; }
}
