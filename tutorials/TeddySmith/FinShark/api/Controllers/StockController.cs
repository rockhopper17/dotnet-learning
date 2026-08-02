using FinShark.api.Data;
using FinShark.api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public StockController(AppDbContext dbContext)
    {
        _dbContext = dbContext;        
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _dbContext.Stocks
            .ToList()
            .Select(s => s.ToStockDto());

        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock.ToStockDto());
    }
}