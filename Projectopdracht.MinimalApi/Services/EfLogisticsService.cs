using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projectopdracht.Data;
using Projectopdracht.MinimalApi.DTOs;
using Projectopdracht.MinimalApi.Interface;
using Projectopdracht.Models;

namespace Projectopdracht.MinimalApi.Services
{
    public class EfLogisticsService : ILogisticsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EfLogisticsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // DEPOT METHODS

        public async Task<List<DepotReadDto>> GetAllDepotsAsync() =>
            await _context.Depots.Select(d => _mapper.Map<DepotReadDto>(d)).ToListAsync();

        public async Task<DepotReadDto?> GetDepotByIdAsync(int id) =>
            _mapper.Map<DepotReadDto>(await _context.Depots.FindAsync(id));

        public async Task<DepotReadDto> AddDepotAsync(DepotCreateDto dto)
        {
            var depot = _mapper.Map<Depot>(dto);
            _context.Depots.Add(depot);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepotReadDto>(depot);
        }

        public async Task<bool> UpdateDepotAsync(int id, DepotUpdateDto dto)
        {
            var existing = await _context.Depots.FindAsync(id);
            if (existing == null) return false;
            _mapper.Map(dto, existing);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDepotAsync(int id)
        {
            var depot = await _context.Depots.FindAsync(id);
            if (depot == null) return false;
            _context.Depots.Remove(depot);
            return await _context.SaveChangesAsync() > 0;
        }

        // CONTAINER METHODS

        public async Task<List<ContainerReadDto>> GetAllContainersAsync() =>
            await _context.Containers.Include(c => c.Depot)
                .Select(c => _mapper.Map<ContainerReadDto>(c)).ToListAsync();

        public async Task<ContainerReadDto?> GetContainerByIdAsync(int id) =>
            _mapper.Map<ContainerReadDto>(await _context.Containers.Include(c => c.Depot)
                .FirstOrDefaultAsync(c => c.Id == id));

        public async Task<ContainerReadDto> AddContainerAsync(ContainerCreateDto dto)
        {
            var container = _mapper.Map<Container>(dto);
            _context.Containers.Add(container);
            await _context.SaveChangesAsync();
            return _mapper.Map<ContainerReadDto>(container);
        }

        public async Task<bool> UpdateContainerAsync(int id, ContainerUpdateDto dto)
        {
            var existing = await _context.Containers.FindAsync(id);
            if (existing == null) return false;
            _mapper.Map(dto, existing);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteContainerAsync(int id)
        {
            var container = await _context.Containers.FindAsync(id);
            if (container == null) return false;
            _context.Containers.Remove(container);
            return await _context.SaveChangesAsync() > 0;
        }

        // TRANSPORT METHODS

        public async Task<List<TransportReadDto>> GetAllTransportsAsync() =>
            await _context.Transports.Include(t => t.Container)
                .Select(t => _mapper.Map<TransportReadDto>(t)).ToListAsync();

        public async Task<TransportReadDto?> GetTransportByIdAsync(int id) =>
            _mapper.Map<TransportReadDto>(await _context.Transports.Include(t => t.Container)
                .FirstOrDefaultAsync(t => t.Id == id));

        public async Task<TransportReadDto> AddTransportAsync(TransportCreateDto dto)
        {
            var transport = _mapper.Map<Transport>(dto);
            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();

            var result = await _context.Transports.Include(t => t.Container)
                                 .FirstOrDefaultAsync(t => t.Id == transport.Id);
            return _mapper.Map<TransportReadDto>(result);
        }

        public async Task<bool> UpdateTransportAsync(int id, TransportUpdateDto dto)
        {
            var existing = await _context.Transports.FindAsync(id);
            if (existing == null) return false;
            _mapper.Map(dto, existing);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTransportAsync(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport == null) return false;
            _context.Transports.Remove(transport);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}