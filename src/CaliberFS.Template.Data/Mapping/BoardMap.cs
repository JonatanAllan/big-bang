﻿using CaliberFS.Template.Data.Context;
using CaliberFS.Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaliberFS.Template.Data.Mapping
{
    public class BoardMap : BaseMap<Board>
    {
        public override void Configure(EntityTypeBuilder<Board> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(AppDbContext.Boards));

            builder.Property(board => board.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(board => board.Description)
                .IsRequired(false)
                .HasMaxLength(500);
        }
    }
}
