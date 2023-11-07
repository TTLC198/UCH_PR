using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UCH_PR_1.Entities;
using Action = UCH_PR_1.Entities.Action;

namespace UCH_PR_1.Context;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<ActionToEvent> ActionToEvents { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventToActionToJury> EventToActionToJuries { get; set; }

    public virtual DbSet<EventToEventTypeToCourse> EventToEventTypeToCourses { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Action_pk");

            entity.ToTable("Action");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Action1)
                .HasMaxLength(300)
                .HasColumnName("Action");
        });

        modelBuilder.Entity<ActionToEvent>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.ActionId }).HasName("ActionToEvent_pk");

            entity.ToTable("ActionToEvent");

            entity.HasOne(d => d.Action).WithMany(p => p.ActionToEvents)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionToEvent_Action_Id_fk");

            entity.HasOne(d => d.Event).WithMany(p => p.ActionToEvents)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionToEvent_Event_Id_fk");

            entity.HasOne(d => d.Moderator).WithMany(p => p.ActionToEvents)
                .HasForeignKey(d => d.ModeratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActionToEvent_User_UserID_fk");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("City_pk");

            entity.ToTable("City");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Hyperlink).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("Country_pk");

            entity.ToTable("Country");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.CharCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.EnglishName).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(300);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Course_pk");

            entity.ToTable("Course");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Course1)
                .HasMaxLength(200)
                .HasColumnName("Course");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Event_pk");

            entity.ToTable("Event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Id");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Status).HasMaxLength(200);

            entity.HasOne(d => d.Member).WithMany(p => p.Events)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("Event_User_UserID_fk");

            entity.HasMany(d => d.Cities).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventToCity",
                    r => r.HasOne<City>().WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("EventToCity_City_Id_fk"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("EventToCity_Event_Id_fk"),
                    j =>
                    {
                        j.HasKey("EventId", "CityId").HasName("EventToCity_pk");
                        j.ToTable("EventToCity");
                    });
        });

        modelBuilder.Entity<EventToActionToJury>(entity =>
        {
            entity.HasKey(e => new { e.ActionId, e.EventId, e.JuryUserId }).HasName("EventToActionToJury_pk");

            entity.ToTable("EventToActionToJury");

            entity.HasOne(d => d.Action).WithMany(p => p.EventToActionToJuries)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventToActionToJury_Action_Id_fk");

            entity.HasOne(d => d.Event).WithMany(p => p.EventToActionToJuries)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventToActionToJury_Event_Id_fk");

            entity.HasOne(d => d.JuryUser).WithMany(p => p.EventToActionToJuries)
                .HasForeignKey(d => d.JuryUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventToActionToJury_User_UserID_fk");
        });

        modelBuilder.Entity<EventToEventTypeToCourse>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.CourseId, e.EventTypeId }).HasName("EventToEventTypeToCourse_pk");

            entity.ToTable("EventToEventTypeToCourse");

            entity.HasOne(d => d.Course).WithMany(p => p.EventToEventTypeToCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventToEventTypeToCourse_Course_Id_fk");

            entity.HasOne(d => d.Event).WithMany(p => p.EventToEventTypeToCourses)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventToEventTypeToCourse_Event_Id_fk");

            entity.HasOne(d => d.EventType).WithMany(p => p.EventToEventTypeToCourses)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventToEventTypeToCourse_EventType_Id_fk");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EventType_pk");

            entity.ToTable("EventType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type).IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genders_pk");

            entity.ToTable("Gender");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Gender1)
                .HasMaxLength(50)
                .HasColumnName("Gender");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pk");

            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Role1)
                .HasMaxLength(100)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC740299E7");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Photo).HasMaxLength(200);

            entity.HasMany(d => d.Courses).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserToCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToCourse_Course_Id_fk"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToCourse_User_UserID_fk"),
                    j =>
                    {
                        j.HasKey("UserId", "CourseId").HasName("UserToCourse_pk");
                        j.ToTable("UserToCourse");
                    });

            entity.HasMany(d => d.EventsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserToEvent",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToEvent_Event_Id_fk"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToEvent_User_UserID_fk"),
                    j =>
                    {
                        j.HasKey("UserId", "EventId").HasName("UserToEvent_pk");
                        j.ToTable("UserToEvent");
                    });

            entity.HasMany(d => d.Genders).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserToGender",
                    r => r.HasOne<Gender>().WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToGender_Gender_Id_fk"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToGender_User_UserID_fk"),
                    j =>
                    {
                        j.HasKey("UserId", "GenderId").HasName("UserToGender_pk");
                        j.ToTable("UserToGender");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserToRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToRole_Role_Id_fk"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("UserToRole_User_UserID_fk"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("UserToRole_pk");
                        j.ToTable("UserToRole");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
