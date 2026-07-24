using AutoMapper;
using LSC.SmartCertify.Application.DTOs;
using LSC.SmartCertify.Application.Interfaces.Courses;

namespace LSC.SmartCertify.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public Task AddCourseAsync(CreateCourseDto createCourseDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllCoursesAsync();

        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public Task<CourseDto?> GetCourseByIdAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsTitleDuplicateAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDescriptionAsync(int courseId, string description)
    {
        throw new NotImplementedException();
    }
}