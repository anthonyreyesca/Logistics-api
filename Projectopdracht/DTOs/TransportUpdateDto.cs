using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.DTOs
{
    public class TransportUpdateDto
    {
        [Required(ErrorMessage = "Het kenteken is verplicht")]
        public string TruckLicensePlate { get; set; } = null!;

        [Required(ErrorMessage = "De afspraaktijd is verplicht")]
        public DateTime AppointmentTime { get; set; }

        [Required]
        public int ContainerId { get; set; }
    }
}