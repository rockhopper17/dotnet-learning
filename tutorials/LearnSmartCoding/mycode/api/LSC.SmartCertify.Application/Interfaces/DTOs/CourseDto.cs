using System.ComponentModel.DataAnnotations;

namespace LSC.SmartCertify.Application.DTOs;

public class CourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}

public class CreateCourseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}

public class UpdateCourseDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public class CourseUpdateDescriptionDto
{
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}
