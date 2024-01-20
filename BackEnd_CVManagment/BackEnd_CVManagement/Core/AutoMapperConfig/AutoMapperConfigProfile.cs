using AutoMapper;
using BackEnd_CVManagement.Core.DTOs.Candidate;
using BackEnd_CVManagement.Core.DTOs.Degree;
using BackEnd_CVManagement.Core.Entities;

namespace BackEnd_CVManagement.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            // Degree 
            CreateMap<DegreeCreateDto, Degree>();
            CreateMap<Degree, DegreeGetDto>();
            CreateMap<DegreeUpdateDto, Degree>();
            CreateMap<Degree, DegreeUpdateDto>();

            // Candidate
            CreateMap<CandidateCreateDto, Candidate>();
            CreateMap<Candidate, CandidateGetDto>()
                .ForMember( dest => dest.DegreeName, opt => opt.MapFrom(src => src.Degree.DegreeName));

            CreateMap<CandidateUpdateDto, Candidate>();
            CreateMap<Candidate, CandidateUpdateDto>();
        }

    }
}