namespace SlightUrl.Components.Commands
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using SlightUrl.Components.Validations;
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
                throw new SlightValidationException("", "Command must have value.");
            }
            var shortenedUrl = new ShortenedLink
            {
                Alias = command.Alias?.ToLower(),
                Url = command.LongUrl
            };

            var aliasExists = _context.ShortenedLinks.Where(x => x.Alias != null).Any(x => x.Alias == shortenedUrl.Alias);
            if (aliasExists)
            {
                throw new SlightValidationException("alias", "Alias already exists.");
            }

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