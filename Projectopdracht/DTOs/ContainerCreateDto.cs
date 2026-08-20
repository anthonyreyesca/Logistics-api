using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.DTOs
{
    public class ContainerCreateDto
    {
        [Required(ErrorMessage = "Het containernummer is verplicht")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{7}$", ErrorMessage = "Ongeldig formaat: gebruik 4 letters en 7 cijfers (bijv. MSCU1234567)")]
        public string ContainerNumber { get; set; } = null!;

        [Required(ErrorMessage = "Het containertype is verplicht")]
        [RegularExpression("^(40HC|20DV|40HR)$", ErrorMessage = "Toegestane types: 40HC, 20DV of 40HR")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Een depot-ID is verplicht")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecteer een geldig depot")]
        public int DepotId { get; set; }
    }
}