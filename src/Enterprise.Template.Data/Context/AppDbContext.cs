using Enterprise.Template.Data.Mapping;
using Enterprise.Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Template.Data.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sample");

            modelBuilder.ApplyConfiguration(new BoardMap());
            modelBuilder.ApplyConfiguration(new TaskItemMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(9, 2);
            configurationBuilder.Properties<string>().HaveColumnType("varchar(100)");
            configurationBuilder.Properties(typeof(Enum)).HaveConversion<string>().HaveColumnType("varchar(50)");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FormatEntity();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            FormatEntity();
            return base.SaveChanges();
        }

        private void FormatEntity()
        {
            var entities = ChangeTracker.Entries()
                .Where(entity => entity is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                    (entity.Entity as BaseEntity)?.CreateAtNow();
                else if (entity.State == EntityState.Modified)
                    (entity.Entity as BaseEntity)?.UpdateAtNow();
            }
        }
    }
}
