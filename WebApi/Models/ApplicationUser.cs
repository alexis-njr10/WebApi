using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {

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
        public DocumentType DocumentType { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [Range(1000000, 9999999999, ErrorMessage = "el campo {0} no cumple con la longitud minina")]
        [Display(Name = "Numero de identificacion")]
        public long DocumentNumber { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [Display(Name = "Genero")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(128)]
        [Display(Name = "Direccion")]
        public string? Address { get; set; }

        public string? Image { get; set; }

    }




}
