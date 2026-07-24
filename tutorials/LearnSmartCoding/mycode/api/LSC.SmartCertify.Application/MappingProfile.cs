using AutoMapper;
using LSC.SmartCertify.Application.DTOs;
using LSC.SmartCertify.Domain.Entities;

namespace LSC.SmartCertify.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>().ForAllMembers(opts =>
            opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}