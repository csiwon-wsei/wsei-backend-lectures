using AutoMapper;
using core.Models;
using web_api_min_ex.Dto;

namespace web_api_min_ex.Mappers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Student, ResponseStudentDto>()
            .ForMember(s => s.Name,
                opt => opt.MapFrom(d => d.FirstName + " " + d.LastName))
            .ForMember(s => s.Group, 
                opt => opt.MapFrom(s => s.StudentGroup.Name));
        CreateMap<Student, RespStudentDto>();
    }
    
    
}