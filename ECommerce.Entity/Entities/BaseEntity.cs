using ECommerce.Entity.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.API.Entities
{
    public class BaseEntity: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ActiveFlag { get; set; }

        [NotMapped]
        public string ActiveStatusText => ActiveFlag == 0 ? "Active" : "Inactive";
    }
}
