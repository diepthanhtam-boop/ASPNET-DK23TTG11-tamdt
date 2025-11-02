using System.ComponentModel.DataAnnotations;

namespace LaptopShopWebsite.Models
{
    public class ContactRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Phone { get; set; } = string.Empty;
        
        public string? ProductId { get; set; }
        
        [Required]
        public string Message { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public string Status { get; set; } = "New";
        
        // Navigation properties
        public virtual Product? Product { get; set; }
    }
}