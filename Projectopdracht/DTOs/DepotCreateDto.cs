using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.DTOs
{
    public class DepotCreateDto
    {
        [Required(ErrorMessage = "De naam van het depot is verplicht")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 100 tekens lang zijn")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "De locatie van het depot is verplicht")]
        public string Location { get; set; } = null!;
    }
}