using Entities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("personaje")]
    public class Character
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

        [DisplayName("Edad")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [Range(0, Int32.MaxValue)] // Como desconozco cual es la edad del personaje más viejo de disney, el valor máximo es lo que acepta un int32
        public int Age { get; set; }

        [DisplayName("Peso")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Se requiere un {0}")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El {0} debe estar entre 0, 01 y 100, 00")] // No se el peso maximo de los personaje de disney, se asume el maximo valor del doble como infinito
        public double Weight { get; set; }

        [DisplayName("Historia")]
        [Required(ErrorMessage = "Se requiere una {0}")]
        [DataType(DataType.Text)]
        public string Story { get; set; }

        [DisplayName("PeliculasSeries")]
        public virtual ICollection<Content>? Contents { get; set; }

    }
}
