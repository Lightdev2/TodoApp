using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.DAL.Entities
{
    public sealed class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsFinished { get; set; }
        [JsonIgnore]
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }

    public class TodoConfiguration: IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todos");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.HasOne(x => x.Creator)
                .WithMany(x => x.CreatedTodos)
                .HasForeignKey(x => x.CreatorId);
        }
    }
}