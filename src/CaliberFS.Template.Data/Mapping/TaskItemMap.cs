﻿using CaliberFS.Template.Data.Context;
using CaliberFS.Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaliberFS.Template.Data.Mapping
{
    public class TaskItemMap : BaseMap<TaskItem>
    {
        public override void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(AppDbContext.Tasks));

            builder.Property(task => task.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(task => task.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(task => task.DueDate)
                .IsRequired(false);

            builder.Property(task => task.CompletedAt)
                .IsRequired(false);

            builder.HasOne(task => task.Board)
                .WithMany(board => board.Tasks)
                .HasForeignKey(task => task.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
