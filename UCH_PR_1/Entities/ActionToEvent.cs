using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class ActionToEvent
{
    public int ActionId { get; set; }

    public int EventId { get; set; }

    public int DayOfEvent { get; set; }

    public TimeSpan StartTime { get; set; }

    public int ModeratorId { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User Moderator { get; set; } = null!;
}
