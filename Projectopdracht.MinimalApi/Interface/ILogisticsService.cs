using Projectopdracht.Models;
using Projectopdracht.MinimalApi.DTOs;

namespace Projectopdracht.MinimalApi.Interface
{
    public interface ILogisticsService
    {
        // --- DEPOT ---
        Task<List<DepotReadDto>> GetAllDepotsAsync();
        Task<DepotReadDto?> GetDepotByIdAsync(int id);
        Task<DepotReadDto> AddDepotAsync(DepotCreateDto depotDto);
        Task<bool> UpdateDepotAsync(int id, DepotUpdateDto depotDto);
        Task<bool> DeleteDepotAsync(int id);

        // --- CONTAINER ---
        Task<List<ContainerReadDto>> GetAllContainersAsync();
        Task<ContainerReadDto?> GetContainerByIdAsync(int id);
        Task<ContainerReadDto> AddContainerAsync(ContainerCreateDto newContainer);
        Task<bool> UpdateContainerAsync(int id, ContainerUpdateDto updatedContainer);
        Task<bool> DeleteContainerAsync(int id);

        // --- TRANSPORT ---
        Task<List<TransportReadDto>> GetAllTransportsAsync();
        Task<TransportReadDto?> GetTransportByIdAsync(int id);
        Task<TransportReadDto> AddTransportAsync(TransportCreateDto newTransport);
        Task<bool> UpdateTransportAsync(int id, TransportUpdateDto transportDto);
        Task<bool> DeleteTransportAsync(int id);
    }
}