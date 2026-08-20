namespace Projectopdracht.DTOs
{
    public class ContainerReadDto
    {
        public int Id { get; set; }
        public string ContainerNumber { get; set; } = default;
        public string Type { get; set; } = default;
        public string DepotName { get; set; } = default;
    }
}
