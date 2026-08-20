using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Projectopdracht.Models
{
    public class Container
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Het containernummer is verplicht")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{7}$", ErrorMessage = "Formaat: 4 letters en 7 cijfers")]
        public string ContainerNumber { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(40HC|20DV|40HR)$", ErrorMessage = "Kies uit: 40HC, 20DV of 40HR")]
        public string Type { get; set; } = string.Empty;

        public int DepotId { get; set; }

        [JsonIgnore]
        public Depot? Depot { get; set; }
    }
}