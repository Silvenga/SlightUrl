namespace SlightUrl.Components.Commands
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using SlightUrl.Data;
    using SlightUrl.Data.Entities;

    public class CreateShortUrl
    {
        private readonly SlightContext _context;

        public class Command
        {
            [Required]
            public string LongUrl { get; set; }

            public string Alias { get; set; }
        }

        public class Result
        {
            public string DrivedId { get; set; }

            public string Alias { get; set; }

            public int CreatedId { get; set; }
        }

        public CreateShortUrl(SlightContext context)
        {
            _context = context;
        }

        public async Task<Result> HandleAsync(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var shortenedUrl = new ShortenedLink
            {
                Alias = command.Alias?.ToLower(),
                Url = command.LongUrl
            };

            _context.ShortenedLinks.Add(shortenedUrl);
            await _context.SaveChangesAsync();

            return new Result
            {
                Alias = shortenedUrl.Alias,
                CreatedId = shortenedUrl.Id,
                DrivedId = shortenedUrl.Id.ToString() // TODO get drived id
            };
        }
    }
}