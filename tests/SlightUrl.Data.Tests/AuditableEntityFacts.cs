namespace SlightUrl.Data.Tests
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;

    using Effort;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using SlightUrl.Data.Interfaces;

    using Xunit;

    [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
    public class AuditableEntityFacts
    {
        private static readonly Fixture AutoFixture = new Fixture();

        private readonly AuditableSlightContext _context;

        public AuditableEntityFacts()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new AuditableSlightContext(connection);
        }

        [Fact]
        public void When_new_entity_is_saved_update_created_date_and_updated_date()
        {
            var entity = new AuditableEntity();

            // Act
            _context.AuditableEntities.Add(entity);
            _context.SaveChanges().Should().Be(1);

            // Assert
            entity.CreatedOn.Should().HaveValue();
            entity.ModifiedOn.Should().HaveValue();
        }

        [Fact]
        public void When_updated_entity_is_saved_update_updated_date()
        {
            var oldEntity = new AuditableEntity();
            _context.AuditableEntities.Add(oldEntity);
            _context.SaveChanges();

            var oldCreatedOn = oldEntity.CreatedOn;
            var oldModifiedOn = oldEntity.ModifiedOn.Value;

            Thread.Sleep(10);

            // Act
            var entity = _context.AuditableEntities.Find(oldEntity.Id);
            entity.Value = AutoFixture.Create<string>();
            _context.SaveChanges().Should().Be(1);

            // Assert
            entity.CreatedOn.Should().Be(oldCreatedOn);
            entity.ModifiedOn.Should().BeAfter(oldModifiedOn);
        }

        [Fact]
        public async Task When_new_entity_is_saved_update_created_date_and_updated_date_async()
        {
            var entity = new AuditableEntity();

            // Act
            _context.AuditableEntities.Add(entity);
            (await _context.SaveChangesAsync()).Should().Be(1);

            // Assert
            entity.CreatedOn.Should().HaveValue();
            entity.ModifiedOn.Should().HaveValue();
        }

        [Fact]
        public async Task When_updated_entity_is_saved_update_updated_date_async()
        {
            var oldEntity = new AuditableEntity();
            _context.AuditableEntities.Add(oldEntity);
            await _context.SaveChangesAsync();

            var oldCreatedOn = oldEntity.CreatedOn;
            var oldModifiedOn = oldEntity.ModifiedOn.Value;

            await Task.Delay(10);

            // Act
            var entity = _context.AuditableEntities.Find(oldEntity.Id);
            entity.Value = AutoFixture.Create<string>();
            (await _context.SaveChangesAsync()).Should().Be(1);

            // Assert
            entity.CreatedOn.Should().Be(oldCreatedOn);
            entity.ModifiedOn.Should().BeAfter(oldModifiedOn);
        }
    }

    public class AuditableSlightContext : SlightContext
    {
        public DbSet<AuditableEntity> AuditableEntities { get; set; }

        public AuditableSlightContext(DbConnection connection) : base(connection)
        {
        }
    }

    public class AuditableEntity : IAuditableEntity
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}