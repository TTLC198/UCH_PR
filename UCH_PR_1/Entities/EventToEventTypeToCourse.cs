using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class EventToEventTypeToCourse
{
    public int EventId { get; set; }

    public int EventTypeId { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual EventType EventType { get; set; } = null!;
}
