using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Category
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Flower> Flowers { get; set; } = new List<Flower>();
}
