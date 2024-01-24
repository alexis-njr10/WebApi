using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Qualification
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [ForeignKey("Student")]
        [Display(Name = "Estudiante")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [ForeignKey("Course")]
        [Display(Name = "Curso")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [Range(0, 100, ErrorMessage = "el campo {0} no cumple con la longitud minina")]
        [Display(Name = "Calificacion")]
        public long Score { get; set; }

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
