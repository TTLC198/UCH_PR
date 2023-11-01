using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<ActionToEvent> ActionToEvents { get; set; } = new List<ActionToEvent>();

    public virtual ICollection<EventToActionToJury> EventToActionToJuries { get; set; } = new List<EventToActionToJury>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Event> EventsNavigation { get; set; } = new List<Event>();

    public virtual ICollection<Gender> Genders { get; set; } = new List<Gender>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
