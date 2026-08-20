using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.DTOs
{
    public class ContainerUpdateDto
    {
        [Required(ErrorMessage = "Containernummer is verplicht")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{7}$")]
        public string ContainerNumber { get; set; } = null!;

        [Required]
        [RegularExpression("^(40HC|20DV|40HR)$")]
        public string Type { get; set; } = null!;

        [Required]
        public int DepotId { get; set; }
    }
}