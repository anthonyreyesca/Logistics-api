using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Projectopdracht.Models
{
    public class Transport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Het kenteken is verplicht")]
        public string TruckLicensePlate { get; set; } = string.Empty;

        [Required]
        public DateTime AppointmentTime { get; set; }

        public int ContainerId { get; set; }

        [JsonIgnore]
        public Container? Container { get; set; }
    }

}