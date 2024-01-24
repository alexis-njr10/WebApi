using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(20)]
        [Display(Name = "Nombres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(20)]
        [Display(Name = "Apellidos")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [ForeignKey("DocumentType")]
        [Display(Name = "Tipo de documento")]
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [Range(1000000, 9999999999, ErrorMessage = "el campo {0} no cumple con la longitud minina")]
        [Display(Name = "Numero de identificacion")]
        public long DocumentNumber { get; set; }

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
