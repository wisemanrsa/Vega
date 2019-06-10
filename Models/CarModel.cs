using System.ComponentModel.DataAnnotations;

namespace vega_be.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Make Make { get; set; }
        public int MakeId { get; set; }
    }
}