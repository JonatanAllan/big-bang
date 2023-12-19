using CaliberFS.Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
