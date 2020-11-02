namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Entity 
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public Entity()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}
