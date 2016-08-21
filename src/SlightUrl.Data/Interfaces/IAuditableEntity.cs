namespace SlightUrl.Data.Interfaces
{
    using System;

    public interface IAuditableEntity
    {
        DateTimeOffset? CreatedOn { get; set; }

        DateTimeOffset? ModifiedOn { get; set; }
    }
}