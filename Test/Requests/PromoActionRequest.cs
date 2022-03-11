using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class PromoActionRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = null;
    }
}