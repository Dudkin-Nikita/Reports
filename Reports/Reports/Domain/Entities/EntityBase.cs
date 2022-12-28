using Microsoft.Build.Framework;

namespace Reports.Domain.Entities
{
    public abstract class EntityBase
    {
        [Required]
        public int Id { get; set; }
    }
}
