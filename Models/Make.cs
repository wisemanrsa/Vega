using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace vega_be.Models {
    public class Make {
        public int Id { get; set; }

        [Required]
        [StringLength (255)]
        public string Name { get; set; }
    }
}