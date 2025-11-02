using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace LaptopShopWebsite.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Brand { get; set; } = string.Empty;
        
        [Required]
        public decimal Price { get; set; }
        
        public decimal? OriginalPrice { get; set; }
        
        [Required]
        public string Image { get; set; } = string.Empty;
        
        public string ImagesJson { get; set; } = "[]";
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        public string Processor { get; set; } = string.Empty;
        
        [Required]
        public string Ram { get; set; } = string.Empty;
        
        [Required]
        public string Storage { get; set; } = string.Empty;
        
        [Required]
        public string Graphics { get; set; } = string.Empty;
        
        [Required]
        public string Display { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string SpecificationsJson { get; set; } = "{}";
        
        [Required]
        public int InStock { get; set; }
        
        [Range(0, 5)]
        public double Rating { get; set; }
        
        public int Reviews { get; set; }
        
        public bool Featured { get; set; }
        
        // Navigation properties
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> Images
        {
            get => JsonSerializer.Deserialize<List<string>>(ImagesJson) ?? new List<string>();
            set => ImagesJson = JsonSerializer.Serialize(value);
        }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public Dictionary<string, string> Specifications
        {
            get => JsonSerializer.Deserialize<Dictionary<string, string>>(SpecificationsJson) ?? new Dictionary<string, string>();
            set => SpecificationsJson = JsonSerializer.Serialize(value);
        }
    }
}