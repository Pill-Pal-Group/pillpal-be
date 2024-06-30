﻿namespace PillPal.Core.Models;

public class PackageCategory : BaseAuditableEntity
{
    public string? PackageName { get; set; }
    public string? PackageDescription { get; set; }
    public int PackageDuration { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<CustomerPackage> CustomerPackages { get; set; } = default!;
}
