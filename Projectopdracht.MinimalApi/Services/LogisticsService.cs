using AutoMapper;
using Projectopdracht.MinimalApi.Interface;
using Projectopdracht.Models;
using Projectopdracht.MinimalApi.DTOs;

namespace Projectopdracht.MinimalApi.Services
{
    public class LogisticsService : ILogisticsService
    {
        private readonly IMapper _mapper;

        private static List<Depot> _depots = new() { new Depot { Id = 1, Name = "Main Hub", Location = "Antwerpen" } };
        private static List<Container> _containers = new() { new Container { Id = 1, ContainerNumber = "ABCD1234567", Type = "40HC", DepotId = 1 } };
        private static List<Transport> _transports = new();

        public LogisticsService(IMapper mapper) => _mapper = mapper;

        // DEPOT METHODS

        public Task<List<DepotReadDto>> GetAllDepotsAsync() =>
            Task.FromResult(_depots.Select(d => _mapper.Map<DepotReadDto>(d)).ToList());

        public Task<DepotReadDto?> GetDepotByIdAsync(int id) =>
            Task.FromResult(_mapper.Map<DepotReadDto?>(_depots.Find(d => d.Id == id)));

        public Task<DepotReadDto> AddDepotAsync(DepotCreateDto dto)
        {
            var depot = _mapper.Map<Depot>(dto);
            depot.Id = _depots.Count > 0 ? _depots.Max(d => d.Id) + 1 : 1;
            _depots.Add(depot);
            return Task.FromResult(_mapper.Map<DepotReadDto>(depot));
        }

        public Task<bool> UpdateDepotAsync(int id, DepotUpdateDto dto)
        {
            var existing = _depots.Find(d => d.Id == id);
            if (existing == null) return Task.FromResult(false);
            _mapper.Map(dto, existing);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteDepotAsync(int id) =>
            Task.FromResult(_depots.RemoveAll(d => d.Id == id) > 0);

        // CONTAINER METHODS

        public Task<List<ContainerReadDto>> GetAllContainersAsync() =>
            Task.FromResult(_containers.Select(c => {
                var dto = _mapper.Map<ContainerReadDto>(c);
                dto.DepotName = _depots.Find(d => d.Id == c.DepotId)?.Name ?? "Onbekend";
                return dto;
            }).ToList());

        public Task<ContainerReadDto?> GetContainerByIdAsync(int id)
        {
            var c = _containers.Find(x => x.Id == id);
            if (c == null) return Task.FromResult<ContainerReadDto?>(null);
            var dto = _mapper.Map<ContainerReadDto>(c);
            dto.DepotName = _depots.Find(d => d.Id == c.DepotId)?.Name ?? "Onbekend";
            return Task.FromResult<ContainerReadDto?>(dto);
        }

        public Task<ContainerReadDto> AddContainerAsync(ContainerCreateDto dto)
        {
            var c = _mapper.Map<Container>(dto);
            c.Id = _containers.Count > 0 ? _containers.Max(x => x.Id) + 1 : 1;
            _containers.Add(c);
            return GetContainerByIdAsync(c.Id)!;
        }

        public Task<bool> UpdateContainerAsync(int id, ContainerUpdateDto dto)
        {
            var existing = _containers.Find(c => c.Id == id);
            if (existing == null) return Task.FromResult(false);
            _mapper.Map(dto, existing);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteContainerAsync(int id) =>
            Task.FromResult(_containers.RemoveAll(c => c.Id == id) > 0);

        // TRANSPORT METHODS

        public Task<List<TransportReadDto>> GetAllTransportsAsync() =>
            Task.FromResult(_transports.Select(t => {
                var dto = _mapper.Map<TransportReadDto>(t);
                dto.ContainerNumber = _containers.Find(c => c.Id == t.ContainerId)?.ContainerNumber ?? "N/A";
                return dto;
            }).ToList());

        public Task<TransportReadDto?> GetTransportByIdAsync(int id)
        {
            var t = _transports.Find(x => x.Id == id);
            if (t == null) return Task.FromResult<TransportReadDto?>(null);
            var dto = _mapper.Map<TransportReadDto>(t);
            dto.ContainerNumber = _containers.Find(c => c.Id == t.ContainerId)?.ContainerNumber ?? "N/A";
            return Task.FromResult<TransportReadDto?>(dto);
        }

        public Task<TransportReadDto> AddTransportAsync(TransportCreateDto dto)
        {
            var t = _mapper.Map<Transport>(dto);
            t.Id = _transports.Count > 0 ? _transports.Max(x => x.Id) + 1 : 1;
            _transports.Add(t);
            return GetTransportByIdAsync(t.Id)!;
        }

        public Task<bool> UpdateTransportAsync(int id, TransportUpdateDto dto)
        {
            var existing = _transports.Find(t => t.Id == id);
            if (existing == null) return Task.FromResult(false);
            _mapper.Map(dto, existing);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteTransportAsync(int id) =>
            Task.FromResult(_transports.RemoveAll(t => t.Id == id) > 0);
    }
}