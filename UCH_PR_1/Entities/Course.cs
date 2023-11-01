using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class Course
{
    public int Id { get; set; }

    public string Course1 { get; set; } = null!;

    public virtual ICollection<EventToEventTypeToCourse> EventToEventTypeToCourses { get; set; } = new List<EventToEventTypeToCourse>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
