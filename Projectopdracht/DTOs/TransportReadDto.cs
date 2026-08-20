namespace Projectopdracht.DTOs
{
    public class TransportReadDto
    {
        public int Id { get; set; }
        public string TruckLicensePlate { get; set; } = default!;
        public DateTime AppointmentTime { get; set; } = default!;
        public string ContainerNumber { get; set; } = default!;
    }
}
