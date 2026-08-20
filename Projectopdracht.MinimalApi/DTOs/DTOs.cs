using System.ComponentModel.DataAnnotations;

namespace Projectopdracht.MinimalApi.DTOs
{
    // --- DEPOT DTO'S ---
    public class DepotCreateDto
    {
        [Required(ErrorMessage = "De naam van het depot is verplicht")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 100 tekens lang zijn")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "De locatie van het depot is verplicht")]
        public string Location { get; set; } = null!;
    }

    public class DepotReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
    }

    public class DepotUpdateDto
    {
        [Required(ErrorMessage = "De naam is verplicht bij een update")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "De locatie is verplicht")]
        public string Location { get; set; } = null!;
    }

    // --- CONTAINER DTO'S ---
    public class ContainerCreateDto
    {
        [Required(ErrorMessage = "Het containernummer is verplicht")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{7}$", ErrorMessage = "Formaat: 4 letters en 7 cijfers (bijv. MSCU1234567)")]
        public string ContainerNumber { get; set; } = null!;

        [Required(ErrorMessage = "Het type is verplicht")]
        [RegularExpression("^(40HC|20DV|40HR)$", ErrorMessage = "Kies uit: 40HC, 20DV of 40HR")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Selecteer een depot")]
        public int DepotId { get; set; }
    }

    public class ContainerReadDto
    {
        public int Id { get; set; }
        public string ContainerNumber { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string DepotName { get; set; } = null!;
    }

    public class ContainerUpdateDto
    {
        [Required]
        [RegularExpression(@"^[A-Z]{4}[0-9]{7}$")]
        public string ContainerNumber { get; set; } = null!;
        [Required]
        [RegularExpression("^(40HC|20DV|40HR)$")]
        public string Type { get; set; } = null!;
        [Required]
        public int DepotId { get; set; }
    }

    // --- TRANSPORT DTO'S ---
    public class TransportCreateDto
    {
        [Required(ErrorMessage = "Het kenteken is verplicht")]
        public string TruckLicensePlate { get; set; } = null!;

        [Required(ErrorMessage = "De afspraaktijd is verplicht")]
        public DateTime AppointmentTime { get; set; }

        [Required(ErrorMessage = "Selecteer een container")]
        public int ContainerId { get; set; }
    }

    public class TransportReadDto
    {
        public int Id { get; set; }
        public string TruckLicensePlate { get; set; } = null!;
        public DateTime AppointmentTime { get; set; }
        public string ContainerNumber { get; set; } = null!;
    }

    public class TransportUpdateDto
    {
        [Required]
        public string TruckLicensePlate { get; set; } = null!;
        [Required]
        public DateTime AppointmentTime { get; set; }
        [Required]
        public int ContainerId { get; set; }
    }
}