using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class EventToActionToJury
{
    public int EventId { get; set; }

    public int ActionId { get; set; }

    public int JuryUserId { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User JuryUser { get; set; } = null!;
}
