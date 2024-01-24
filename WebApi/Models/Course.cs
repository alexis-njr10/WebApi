using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [ForeignKey("Teacher")]
        [Display(Name = "Docente")]
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        [ForeignKey("User")]
        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }

        [ForeignKey("User")]
        public string? UpdatedByUserId { get; set; }
        public ApplicationUser? UpdatedByUser { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DeletedAt { get; set; }
    }
}
