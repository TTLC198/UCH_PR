using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class Event
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public int NumberOfDays { get; set; }

    public int? MemberId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ActionToEvent> ActionToEvents { get; set; } = new List<ActionToEvent>();

    public virtual ICollection<EventToActionToJury> EventToActionToJuries { get; set; } = new List<EventToActionToJury>();

    public virtual ICollection<EventToEventTypeToCourse> EventToEventTypeToCourses { get; set; } = new List<EventToEventTypeToCourse>();

    public virtual User? Member { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
