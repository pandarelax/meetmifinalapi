using meetmifinal.models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.data.Context
{
    public class MeetmiDbContext : DbContext
    {
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<User> Users { get; set; }

        public MeetmiDbContext(DbContextOptions<MeetmiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Password).HasMaxLength(256).IsRequired();
                entity.Property(e => e.PhotoUrl).HasMaxLength(1000);
                entity.Property(e => e.RefreshToken).HasMaxLength(1000);
                entity.Property(e => e.Token).HasMaxLength(1000);
                entity.HasMany(e => e.Meetings).WithOne(e => e.Creator).HasForeignKey(e => e.UserId);

            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(5000);
                entity.Property(e => e.UserId).IsRequired(); 
                entity.HasOne(e => e.Creator).WithMany(e => e.Meetings).HasForeignKey(e => e.UserId);
            });


        }

    }
}
