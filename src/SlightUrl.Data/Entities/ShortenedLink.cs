namespace SlightUrl.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SlightUrl.Data.Interfaces;

    public class ShortenedLink : IAuditableEntity
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public string Alias { get; set; }

        [Required, Index]
        public string Url { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}