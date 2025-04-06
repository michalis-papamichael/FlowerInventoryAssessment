using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Flower
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int TotalInventory { get; set; }

    public int CategoryId { get; set; }

    public string? ImageUri { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset? DeletionTimestamp { get; set; }

    public DateTimeOffset? LastUpdateTimestamp { get; set; }

    public virtual Category Category { get; set; } = null!;
}
