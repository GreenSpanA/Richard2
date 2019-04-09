using System.ComponentModel.DataAnnotations;

namespace Richard2.Models
{
    public class sampleMenu : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Dish { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Veg_Comment { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public string Size_Comment { get; set; }


    }
}
