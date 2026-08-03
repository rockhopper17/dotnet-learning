using FinShark.api.Data;
using FinShark.api.Dtos.Stock;
using FinShark.api.Interfaces;
using FinShark.api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinShark.api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IStockRepository _stockRepo;

    public StockController(AppDbContext dbContext, IStockRepository stockRepo)
    {
        _dbContext = dbContext;
        _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepo.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());

        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepo.GetByIdAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
        var stockModel = await _stockRepo.UpdateAsync(id, stockDto);

        if (stockModel == null)
        {
            return NotFound();
        }

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _stockRepo.DeleteAsync(id);
        
        if (stockModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}