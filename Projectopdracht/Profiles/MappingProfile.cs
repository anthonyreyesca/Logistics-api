using AutoMapper;
using Projectopdracht.Models;
using Projectopdracht.DTOs;

namespace Projectopdracht.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- DEPOT MAPPINGS ---
            CreateMap<Depot, DepotReadDto>();
            CreateMap<DepotCreateDto, Depot>();
            CreateMap<DepotUpdateDto, Depot>();

            // --- CONTAINER MAPPINGS ---
            CreateMap<Container, ContainerReadDto>()
                .ForMember(dest => dest.DepotName, opt => opt.MapFrom(src => src.Depot != null ? src.Depot.Name : "Geen Depot"));
            CreateMap<ContainerCreateDto, Container>();
            CreateMap<ContainerUpdateDto, Container>();

            // --- TRANSPORT MAPPINGS ---
            CreateMap<Transport, TransportReadDto>()
                .ForMember(dest => dest.ContainerNumber, opt => opt.MapFrom(src => src.Container != null ? src.Container.ContainerNumber : "N/A"));
            CreateMap<TransportCreateDto, Transport>();
            CreateMap<TransportUpdateDto, Transport>();
        }
    }
}