using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class EventType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<EventToEventTypeToCourse> EventToEventTypeToCourses { get; set; } = new List<EventToEventTypeToCourse>();
}
