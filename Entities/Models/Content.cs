using Entities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("pelicula-serie")]
    public abstract class Content
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Imagen")]
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", "jpeg", "bmp", "gif" })]
        public string Image { get; set; }

        [DisplayName("Título")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Se requiere un {0}")]
        [StringLength(64, ErrorMessage = "La longitud de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Title { get; set; }

        [DisplayName("Fecha de creación")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una {0}")]
        public int ReleaseDate { get; set; }

        [DisplayName("Calificación")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [Range(1.0, 5.0, ErrorMessage = "El {0} debe estar entre 1,0 y 5,0")]
        public double Rating { get; set; }

        [DisplayName("Personajes")]
        public virtual ICollection<Character>? Characters { get; set; }

        [DisplayName("Género")]
        public Guid? GenderId { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }
    }
}
