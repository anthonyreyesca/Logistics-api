using Microsoft.EntityFrameworkCore;
using Projectopdracht.Interface;
using Projectopdracht.Models;
using Projectopdracht.DTOs;
using AutoMapper;
using Projectopdracht.Data;

namespace Projectopdracht.Services
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

        // --- DEPOT METHODEN ---

        public async Task<List<DepotReadDto>> GetAllDepotsAsync()
            => await _context.Depots
                .AsNoTracking()
                .Select(d => _mapper.Map<DepotReadDto>(d))
                .ToListAsync();

        public async Task<DepotReadDto?> GetDepotByIdAsync(int id)
        {
            var depot = await _context.Depots.FindAsync(id);
            return depot == null ? null : _mapper.Map<DepotReadDto>(depot);
        }

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
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepotAsync(int id)
        {
            var depot = await _context.Depots.FindAsync(id);
            if (depot == null) return false;
            try
            {
                _context.Depots.Remove(depot);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        // --- CONTAINER METHODEN ---

        public async Task<List<ContainerReadDto>> GetAllContainersAsync()
            => await _context.Containers
                .AsNoTracking()
                .Include(c => c.Depot)
                .Select(c => _mapper.Map<ContainerReadDto>(c))
                .ToListAsync();

        public async Task<ContainerReadDto?> GetContainerByIdAsync(int id)
        {
            var container = await _context.Containers
                .Include(c => c.Depot)
                .FirstOrDefaultAsync(c => c.Id == id);
            return container == null ? null : _mapper.Map<ContainerReadDto>(container);
        }

        public async Task<ContainerReadDto> AddContainerAsync(ContainerCreateDto dto)
        {
            var container = _mapper.Map<Container>(dto);
            _context.Containers.Add(container);
            await _context.SaveChangesAsync();

            var result = await _context.Containers.Include(c => c.Depot).FirstAsync(c => c.Id == container.Id);
            return _mapper.Map<ContainerReadDto>(result);
        }

        public async Task<bool> UpdateContainerAsync(int id, ContainerUpdateDto dto)
        {
            var existing = await _context.Containers.FindAsync(id);
            if (existing == null) return false;
            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteContainerAsync(int id)
        {
            var container = await _context.Containers.FindAsync(id);
            if (container == null) return false;
            _context.Containers.Remove(container);
            await _context.SaveChangesAsync();
            return true;
        }

        // --- TRANSPORT METHODEN ---

        public async Task<List<TransportReadDto>> GetAllTransportsAsync()
            => await _context.Transports
                .AsNoTracking()
                .Include(t => t.Container)
                .Select(t => _mapper.Map<TransportReadDto>(t))
                .ToListAsync();

        public async Task<TransportReadDto?> GetTransportByIdAsync(int id)
        {
            var transport = await _context.Transports
                .Include(t => t.Container)
                .FirstOrDefaultAsync(t => t.Id == id);
            return transport == null ? null : _mapper.Map<TransportReadDto>(transport);
        }

        public async Task<TransportReadDto> AddTransportAsync(TransportCreateDto dto)
        {
            var transport = _mapper.Map<Transport>(dto);
            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();

            var result = await _context.Transports.Include(t => t.Container).FirstAsync(t => t.Id == transport.Id);
            return _mapper.Map<TransportReadDto>(result);
        }

        public async Task<bool> UpdateTransportAsync(int id, TransportUpdateDto dto)
        {
            var existing = await _context.Transports.FindAsync(id);
            if (existing == null) return false;
            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTransportAsync(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport == null) return false;
            _context.Transports.Remove(transport);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}