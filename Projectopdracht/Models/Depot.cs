using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.Models
{
    public class Depot
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "De naam van het depot is verplicht")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "De locatie is verplicht")]
        public string Location { get; set; } = string.Empty;
    }
}