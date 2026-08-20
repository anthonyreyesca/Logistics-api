using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.DTOs
{
    public class TransportCreateDto
    {
        [Required(ErrorMessage = "Het kenteken van de vrachtwagen is verplicht")]
        public string TruckLicensePlate { get; set; } = null!;

        [Required(ErrorMessage = "De afspraaktijd is verplicht")]
        public DateTime AppointmentTime { get; set; }

        [Required(ErrorMessage = "Een container-ID is verplicht")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecteer een geldige container")]
        public int ContainerId { get; set; }
    }
}