using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Activity> ActivityAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new {aa.AppUserId, aa.ActivityID}));

            builder.Entity<ActivityAttendee>().HasOne(u => u.AppUser).WithMany(a => a.Activities).HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ActivityAttendee>().HasOne(u => u.Activity).WithMany(a => a.Attendees).HasForeignKey(aa => aa.ActivityID);
        }
    }
}