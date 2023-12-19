using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CaliberFS.Template.Domain.Entities;

namespace CaliberFS.Template.Data.Mapping
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.CreatedAt)
                .IsRequired();

            builder.Property(entity => entity.LastUpdatedAt)
                .IsRequired(false);
        }
    }
}
