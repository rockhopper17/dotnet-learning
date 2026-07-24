using FluentValidation;
using LSC.SmartCertify.Application.DTOs;
using LSC.SmartCertify.Application.Interfaces.Courses;
using Microsoft.AspNetCore.Mvc;

namespace LSC.SmartCertify.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;
    private readonly IValidator<CreateCourseDto> validator;
    private readonly IValidator<UpdateCourseDto> updateValidator;

    public CoursesController(
        ICourseService service,
        IValidator<CreateCourseDto> validator,
        IValidator<UpdateCourseDto> updateValidator)
    {
        _service = service;
        this.validator = validator;
        this.updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        return Ok(await _service.GetAllCoursesAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _service.GetCourseByIdAsync(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCourseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
    {
        var validationResult = await validator.ValidateAsync(createCourseDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _service.AddCourseAsync(createCourseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = createCourseDto.Title}, createCourseDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)
    {
        var validationResult = await updateValidator.ValidateAsync(updateCourseDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _service.UpdateCourseAsync(id, updateCourseDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _service.DeleteCourseAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDescription([FromRoute] int id, [FromBody] CourseUpdateDescriptionDto model)
    {
        await _service.UpdateDescriptionAsync(id, model.Description);
        return NoContent();
    }
}