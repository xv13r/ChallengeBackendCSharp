using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Correo electrónico")]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
