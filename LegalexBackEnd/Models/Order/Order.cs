using System.ComponentModel.DataAnnotations;
using LegalexBackEnd.Models.Order.Types;

namespace LegalexBackEnd.Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public Entity Type { get; set; }
        [Required]
        public Service Service { get; set; }
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
