using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.DTOs
{
    public class DepotUpdateDto
    {
        [Required(ErrorMessage = "De naam is verplicht voor een update")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "De locatie is verplicht")]
        public string Location { get; set; } = null!;
    }
}