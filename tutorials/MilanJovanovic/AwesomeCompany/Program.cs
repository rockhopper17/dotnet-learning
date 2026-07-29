using AwesomeCompany;
using AwesomeCompany.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    var baseConnection = builder.Configuration.GetConnectionString("BaseSqlServer");
    var dbName = builder.Configuration["DatabaseName"];
    opt.UseSqlServer($"{baseConnection};Database={dbName}");
});

var app = builder.Build();

app.UseHttpsRedirection();

// -----------------------------------------------------------------------------------------
// GET /employees
// -----------------------------------------------------------------------------------------
app.MapGet("/employees", async (DatabaseContext dbContext) =>
    await dbContext.Set<Employee>().ToListAsync());

// -----------------------------------------------------------------------------------------
// PUT increase-salaries
// -----------------------------------------------------------------------------------------
app.MapPut("increase-salaries", async (int companyId, DatabaseContext dbContext) =>
{
    // IQueryable<Company> query = dbContext.Set<Company>();
    // query = query.Include(c => c.Employees);
    // Company? company = await query.FirstOrDefaultAsync(c => c.Id == companyId);

    var company = await dbContext
        .Set<Company>()
        .Include(c => c.Employees)
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound($"company with id {companyId} was not found");
    }

    foreach (var employee in company.Employees)
    {
        employee.Salary *= 1.1m;
    }

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

// -----------------------------------------------------------------------------------------
// PUT increase-salaries-sql
// -----------------------------------------------------------------------------------------
app.MapPut("increase-salaries-sql", async (int companyId, DatabaseContext dbContext) =>
{
    // IQueryable<Company> query = dbContext.Set<Company>();
    // query = query.Include(c => c.Employees);
    // Company? company = await query.FirstOrDefaultAsync(c => c.Id == companyId);

    var company = await dbContext
        .Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound($"company with id {companyId} was not found");
    }

    await dbContext.Database.BeginTransactionAsync();

    await dbContext.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE Employees SET Salary = Salary * 1.1 WHERE CompanyId = {company.Id}"
    );

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    await dbContext.Database.CommitTransactionAsync();

    return Results.NoContent();
});

// -----------------------------------------------------------------------------------------
// PUT increase-salaries-sql-dapper
// -----------------------------------------------------------------------------------------
app.MapPut("increase-salaries-sql-dapper", async (int companyId, DatabaseContext dbContext) =>
{
    // IQueryable<Company> query = dbContext.Set<Company>();
    // query = query.Include(c => c.Employees);
    // Company? company = await query.FirstOrDefaultAsync(c => c.Id == companyId);

    var company = await dbContext
        .Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound($"company with id {companyId} was not found");
    }

    // var transaction = await dbContext.Database.BeginTransactionAsync();

    await dbContext.Database.GetDbConnection().ExecuteAsync(
        $"UPDATE Employees SET Salary = Salary * 1.1 WHERE CompanyId = @CompanyId",
        new { CompanyId = company.Id }
        // transaction.GetDbTransaction()
    );

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    // await dbContext.Database.CommitTransactionAsync();

    return Results.NoContent();
});

app.Run();