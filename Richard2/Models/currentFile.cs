using System.ComponentModel.DataAnnotations;

namespace Richard2.Models
{
    public class currentFile : BaseEntity
    {
        [Key]
        public int Id { get; set; }      

        [Required]
        public string File_Name { get; set; }


    }
}
