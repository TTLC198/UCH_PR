using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class Gender
{
    public int Id { get; set; }

    public string Gender1 { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
