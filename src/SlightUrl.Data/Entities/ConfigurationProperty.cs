namespace SlightUrl.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SlightUrl.Data.Interfaces;

    public class ConfigurationProperty : IAuditableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}