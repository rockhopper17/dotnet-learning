// -----------------------------------------------------------------------------
// aradalis 5 Rules for DTOs https://www.youtube.com/watch?v=W4n9x_qGpT4
// -----------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentValidation;

var person = new PersonDto("Homer", "Simpson");
Console.WriteLine(person);
// person.LastName = "Smith"; // will not compile because init only

string jsonPerson = JsonSerializer.Serialize(person);
Console.WriteLine(jsonPerson);
var transformedPerson = JsonSerializer.Deserialize<PersonDto>(jsonPerson);
Console.WriteLine(transformedPerson);

var customer = new CustomerDto { Id = 7, FirstName = "Zagier" };
// customer.FirstName = "Donald"; // won't work if setter is init
customer.LastName = "Luna";

// 1. should DTOs be immutable?
// it's most useful when consuming DTOs; may not be helpful when creating them to send
// do what makes sense in context

// 2. should not enforce encapsulation (no private members)

// 3. should DTOs use fields or properties? properties
var order = new OrderDTO{ Id=1, OrderNumber="123", Total=100.00m };
Console.WriteLine(order); // no class tostring, just prints class name
string jsonOrder = JsonSerializer.Serialize(order);
Console.WriteLine($"serialized order: {jsonOrder}");  // blank, fields don't get serialized

// 4. what should we name DTOs?
// DTO or Dto
// ViewModel or Request or Response are valid suffixes
// should only use Dto in name as last resort, name them for how they are used

// REPR design pattern - Request, Endpoint, Response

// 5. these things should be modeled as DTOs
// API Request / Response objects
// MVC ViewModel objects
// Database query result objects
// Messages (Commands, Events, Queries)

// validation example w attributes - works for class not for record
var request = new CreateUserRequestWithAttributes("john.doe.com", "123"); // note these values are not valid
var validationResults = new List<ValidationResult>();
var isValid = Validator.TryValidateObject(request, new System.ComponentModel.DataAnnotations.ValidationContext(request),
    validationResults, validateAllProperties: true);
Console.WriteLine(request);
Console.WriteLine("is request valid? {0}",isValid);
foreach (var result in validationResults)
{
    Console.WriteLine(result.ErrorMessage);
}

var requestClass = new CreateUserRequestWithAttributesClass
{
    Email = "john.doe.com", Password = "123" 
}; // note these values are not valid
var validationResults2 = new List<ValidationResult>();
var isValid2 = Validator.TryValidateObject(requestClass, new System.ComponentModel.DataAnnotations.ValidationContext(requestClass),
    validationResults2, validateAllProperties: true);
Console.WriteLine(requestClass);
Console.WriteLine("is request valid? {0}",isValid2);
foreach (var result in validationResults2)
{
    Console.WriteLine(result.ErrorMessage);
}

// FluentValidation
Console.WriteLine("FluentValidation:");
var recordValidator = new CreateUserRequestWithAttributesValidator();
var validationResult = recordValidator.Validate(request);
if (!validationResult.IsValid);
foreach (var error in validationResult.Errors)
{
    Console.WriteLine(error.ErrorMessage);
}

// ====================================================================================

public record CreatePersonRequest(string FirstName, string LastName);

public record PersonDto(string FirstName, string LastName);

public class CustomerDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; set; }
}

// don't use fields for DTO attributes
public class OrderDTO
{
    public int Id;
    public string OrderNumber;
    public decimal Total;
}

// rule 5: these are all DTOs (all used in the create a user request pipeline)
public record CreateUserRequest(string Email, string Password);
public record CreateUserCommand(string Email, string Password);
public record UserExistsQuery(string Email);
public record UserCreatedEvent(int Id, string Email);
public record CreateUserResponse(int Id, string Email);
public record UserDetailsViewModel(int Id, string Email);

// can use attributes for validation
public record CreateUserRequestWithAttributes(
    [EmailAddress] string Email,
    [MinLength(8)] string Password
);

public class CreateUserRequestWithAttributesClass
{
    [EmailAddress]
    public string Email { get; init; }

    [MinLength(8)]
    public string Password { get; init; }
}

// FluentValidation example
public class CreateUserRequestWithAttributesValidator : AbstractValidator<CreateUserRequestWithAttributes>
{
    public CreateUserRequestWithAttributesValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("email is required")
            .EmailAddress().WithMessage("email is not a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("password is required")
            .MinimumLength(8).WithMessage("password must be at least 8 characters");
    }
}