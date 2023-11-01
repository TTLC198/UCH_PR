using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class Action
{
    public int Id { get; set; }

    public string Action1 { get; set; } = null!;

    public virtual ICollection<ActionToEvent> ActionToEvents { get; set; } = new List<ActionToEvent>();

    public virtual ICollection<EventToActionToJury> EventToActionToJuries { get; set; } = new List<EventToActionToJury>();
}
