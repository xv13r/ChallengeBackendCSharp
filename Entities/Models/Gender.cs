using Entities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("genero")]
    public class Gender
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Imagen")]
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", "jpeg", "bmp", "gif" })]
        public string Image { get; set; }

        [DisplayName("Nombre")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Se requiere un {0}")]
        [StringLength(64, ErrorMessage = "La longitud de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Contenidos")]
        public virtual ICollection<Content>? Contents { get; set; }
    }
}
