using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.DAL.Entities
{
    public sealed class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        [JsonIgnore]
        public List<Todo> AttachedTodos { get; set; } = new List<Todo>();
        [JsonIgnore]
        public List<User> ProjectMembers { get; set; } = new List<User>();
        [JsonIgnore]
        public User Creator { get; set; }

        public string Desc { get; set; }
        public int CreatorId { get; set; }
    }

    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasIndex(x => x.Id);
            builder.HasMany(x => x.AttachedTodos)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}