using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(20)]
        [Display(Name = "Nombres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(20)]
        [Display(Name = "Apellidos")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [ForeignKey("DocumentType")]
        [Display(Name = "Tipo de documento")]
        public int DocumentTypeId { get; set; }

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
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(20)]
        [Display(Name = "Telefono")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
