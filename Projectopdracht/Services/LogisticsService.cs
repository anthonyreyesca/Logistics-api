using Projectopdracht.DTOs;
using Projectopdracht.Interface;
using Projectopdracht.Models;
using AutoMapper;

namespace Projectopdracht.Services
{
    public class LogisticsService : ILogisticsService
    {
        private readonly IMapper _mapper;

        public LogisticsService(IMapper mapper)
        {
            _mapper = mapper;
        }

        // In-memory data
        private static List<Depot> _depots = new()
        {
            new Depot { Id = 1, Name = "DR Depots Kaai 502", Location = "Antwerpen" }
        };
        private static List<Container> _containers = new()
        {
            new Container { Id = 10, ContainerNumber = "MSCU1238763", Type = "40HC", DepotId = 1 }
        };
        private static List<Transport> _transports = new()
        {
            new Transport { Id = 500, TruckLicensePlate = "1-ABC-123", ContainerId = 10,
                AppointmentTime = new DateTime(2026, 6, 1, 9, 0, 0) }
        };

        // --- DEPOT METHODEN ---

        public Task<List<DepotReadDto>> GetAllDepotsAsync()
        {
            var dtos = _depots.Select(d => _mapper.Map<DepotReadDto>(d)).ToList();
            return Task.FromResult(dtos);
        }

        public Task<DepotReadDto?> GetDepotByIdAsync(int id)
        {
            var depot = _depots.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(depot == null ? null : _mapper.Map<DepotReadDto>(depot));
        }

        public Task<DepotReadDto> AddDepotAsync(DepotCreateDto dto)
        {
            var newDepot = _mapper.Map<Depot>(dto);
            newDepot.Id = _depots.Any() ? _depots.Max(d => d.Id) + 1 : 1;
            _depots.Add(newDepot);
            return Task.FromResult(_mapper.Map<DepotReadDto>(newDepot));
        }

        public Task<bool> UpdateDepotAsync(int id, DepotUpdateDto dto)
        {
            var existing = _depots.FirstOrDefault(d => d.Id == id);
            if (existing == null) return Task.FromResult(false);
            _mapper.Map(dto, existing);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteDepotAsync(int id)
        {
            var depot = _depots.FirstOrDefault(d => d.Id == id);
            if (depot == null) return Task.FromResult(false);

            if (_containers.Any(c => c.DepotId == id)) return Task.FromResult(false);

            _depots.Remove(depot);
            return Task.FromResult(true);
        }

        // --- CONTAINER METHODEN ---

        public Task<List<ContainerReadDto>> GetAllContainersAsync()
        {
            var dtos = _containers.Select(c =>
            {
                var dto = _mapper.Map<ContainerReadDto>(c);
                dto.DepotName = _depots.FirstOrDefault(d => d.Id == c.DepotId)?.Name ?? "Onbekend";
                return dto;
            }).ToList();
            return Task.FromResult(dtos);
        }

        public Task<ContainerReadDto?> GetContainerByIdAsync(int id)
        {
            var container = _containers.FirstOrDefault(c => c.Id == id);
            if (container == null) return Task.FromResult<ContainerReadDto?>(null);

            var dto = _mapper.Map<ContainerReadDto>(container);
            dto.DepotName = _depots.FirstOrDefault(d => d.Id == container.DepotId)?.Name ?? "Onbekend";
            return Task.FromResult<ContainerReadDto?>(dto);
        }

        public Task<ContainerReadDto> AddContainerAsync(ContainerCreateDto dto)
        {
            var newContainer = _mapper.Map<Container>(dto);
            newContainer.Id = _containers.Any() ? _containers.Max(c => c.Id) + 1 : 1;
            _containers.Add(newContainer);

            var readDto = _mapper.Map<ContainerReadDto>(newContainer);
            readDto.DepotName = _depots.FirstOrDefault(d => d.Id == newContainer.DepotId)?.Name ?? "Onbekend";
            return Task.FromResult(readDto);
        }

        public Task<bool> UpdateContainerAsync(int id, ContainerUpdateDto dto)
        {
            var existing = _containers.FirstOrDefault(c => c.Id == id);
            if (existing == null) return Task.FromResult(false);
            _mapper.Map(dto, existing);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteContainerAsync(int id)
        {
            var container = _containers.FirstOrDefault(c => c.Id == id);
            if (container == null) return Task.FromResult(false);
            _containers.Remove(container);
            return Task.FromResult(true);
        }

        // --- TRANSPORT METHODEN ---

        public Task<List<TransportReadDto>> GetAllTransportsAsync()
        {
            var dtos = _transports.Select(t =>
            {
                var dto = _mapper.Map<TransportReadDto>(t);
                dto.ContainerNumber = _containers.FirstOrDefault(c => c.Id == t.ContainerId)?.ContainerNumber ?? "N/A";
                return dto;
            }).ToList();
            return Task.FromResult(dtos);
        }

        public Task<TransportReadDto?> GetTransportByIdAsync(int id)
        {
            var transport = _transports.FirstOrDefault(t => t.Id == id);
            if (transport == null) return Task.FromResult<TransportReadDto?>(null);

            var dto = _mapper.Map<TransportReadDto>(transport);
            dto.ContainerNumber = _containers.FirstOrDefault(c => c.Id == transport.ContainerId)?.ContainerNumber ?? "N/A";
            return Task.FromResult<TransportReadDto?>(dto);
        }

        public Task<TransportReadDto> AddTransportAsync(TransportCreateDto dto)
        {
            var newTransport = _mapper.Map<Transport>(dto);
            newTransport.Id = _transports.Any() ? _transports.Max(t => t.Id) + 1 : 1;
            _transports.Add(newTransport);

            var readDto = _mapper.Map<TransportReadDto>(newTransport);
            readDto.ContainerNumber = _containers.FirstOrDefault(c => c.Id == newTransport.ContainerId)?.ContainerNumber ?? "N/A";
            return Task.FromResult(readDto);
        }

        public Task<bool> UpdateTransportAsync(int id, TransportUpdateDto dto)
        {
            var existing = _transports.FirstOrDefault(t => t.Id == id);
            if (existing == null) return Task.FromResult(false);
            _mapper.Map(dto, existing);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteTransportAsync(int id)
        {
            var transport = _transports.FirstOrDefault(t => t.Id == id);
            if (transport == null) return Task.FromResult(false);
            _transports.Remove(transport);
            return Task.FromResult(true);
        }
    }
}