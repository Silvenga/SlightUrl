namespace SlightUrl.Components.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Effort;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using SlightUrl.Components.Commands;
    using SlightUrl.Components.Validations;
    using SlightUrl.Data;
    using SlightUrl.Data.Entities;

    using Xunit;

    public class CreateShortUrlFacts
    {
        private static readonly Fixture AutoFixture = new Fixture();

        private readonly SlightContext _context;
        private readonly CreateShortUrl _command;

        public CreateShortUrlFacts()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new SlightContext(connection);

            _command = new CreateShortUrl(_context);
        }

        [Fact]
        public async Task When_command_is_valid_create_short_url()
        {
            var input = AutoFixture.Create<CreateShortUrl.Command>();

            // Act
            var result = await _command.HandleAsync(input);

            // Assert
            result.Should().NotBeNull();
            var url = _context.ShortenedLinks.Find(result.CreatedId);
            url.Should().NotBeNull();

            url.Alias.Should().Be(input.Alias.ToLower());
            url.Url.Should().Be(input.LongUrl);
        }

        [Fact]
        public async Task Command_should_make_alias_lower()
        {
            var input = AutoFixture.Create<CreateShortUrl.Command>();
            input.Alias = input.Alias.ToUpper();

            // Act
            var result = await _command.HandleAsync(input);

            // Assert
            var url = _context.ShortenedLinks.Find(result.CreatedId);
            url.Alias.Should().Be(input.Alias.ToLower());
        }

        [Fact]
        public void Creating_duplicate_alias_should_fail()
        {
            var existing = AutoFixture.Build<ShortenedLink>()
                                       .Without(x => x.Id)
                                       .Create();
            existing.Alias = existing.Alias.ToLower();
            _context.ShortenedLinks.Add(existing);
            _context.SaveChanges();

            CreateShortUrl.Command input = new CreateShortUrl.Command
            {
                Alias = existing.Alias,
                LongUrl = existing.Url
            };

            // Act
            Action action = () => _command.HandleAsync(input).Wait();

            // Assert
            action.ShouldThrow<SlightValidationException>();
        }
    }
}