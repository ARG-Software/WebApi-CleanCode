using System;

namespace RF.Domain.Interfaces.Common
{
    public interface IRFBaseEntity
    {
        int Id { get; set; }

        DateTime CreatedOn { get;}

        DateTime? ModifiedOn { get;}

        public string CreatedByUserLogin { get; }

        public string ModifiedByUserLogin { get; }

        public string Description { get; set; }
    }
}