using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.DAL.Entities
{
    public sealed class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Sex { get; set; }
        public List<User> Friends { get; set; }
        public List<Project> Projects { get; set; }
        public List<UserSession> Sessions { get; set; }
        public List<Todo> CreatedTodos { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email)
                .IsRequired();
            builder.Property(x => x.Password)
                .IsRequired();
            builder.HasIndex(x => x.Email)
                .IsUnique(true);
            builder.HasMany(x => x.Sessions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Projects)
                .WithOne(x => x.Creator)
                .HasForeignKey(x => x.CreatorId);
        }
    }
}