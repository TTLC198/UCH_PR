using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Hyperlink { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
