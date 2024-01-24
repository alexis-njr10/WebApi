using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class DocumentType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(128)]
        [Display(Name = "Tipo de documento")]
        public string? Name { get; set; }
    }
}
