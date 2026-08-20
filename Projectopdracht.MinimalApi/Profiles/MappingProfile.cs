using AutoMapper;
using Projectopdracht.MinimalApi.DTOs;
using Projectopdracht.Models;

namespace MinimalApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- INCOMING (DTO -> Model) ---
            // Depots
            CreateMap<DepotCreateDto, Depot>();
            CreateMap<DepotUpdateDto, Depot>();

            // Containers
            CreateMap<ContainerCreateDto, Container>();
            CreateMap<ContainerUpdateDto, Container>();

            // Transports
            CreateMap<TransportCreateDto, Transport>();
            CreateMap<TransportUpdateDto, Transport>();

            // --- OUTGOING (Model -> ReadDto) ---
            CreateMap<Depot, DepotReadDto>();

            CreateMap<Container, ContainerReadDto>()
                .ForMember(dest => dest.DepotName,
                    opt => opt.MapFrom(src => src.Depot != null ? src.Depot.Name : "Geen Depot"));

            CreateMap<Transport, TransportReadDto>()
                .ForMember(dest => dest.ContainerNumber,
                    opt => opt.MapFrom(src => src.Container != null ? src.Container.ContainerNumber : "N/A"));
        }
    }
}