using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace LaptopShopWebsite.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        public string ItemsJson { get; set; } = "[]";
        
        [Required]
        public decimal Total { get; set; }
        
        [Required]
        public string Status { get; set; } = "pending";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [Required]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [Required]
        public string CustomerAddress { get; set; } = string.Empty;
        
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual User User { get; set; } = null!;
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<CartItem> Items
        {
            get => JsonSerializer.Deserialize<List<CartItem>>(ItemsJson) ?? new List<CartItem>();
            set => ItemsJson = JsonSerializer.Serialize(value);
        }
    }
    

}