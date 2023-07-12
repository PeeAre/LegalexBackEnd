using System.ComponentModel.DataAnnotations;

namespace LegalexBackEnd.Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public Entity Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Description { get; set; }
    }
}
