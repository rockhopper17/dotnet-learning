using FinShark.api.Models;
using FinShark.api.Interfaces;
using FinShark.api.Data;
using Microsoft.EntityFrameworkCore;
using FinShark.api.Dtos.Stock;

namespace FinShark.api.Repository;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _dbContext;

    public StockRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;        
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _dbContext.Stocks.AddAsync(stockModel);
        await _dbContext.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        
        if (stockModel == null)
        {
            return null; 
        }

        _dbContext.Stocks.Remove(stockModel);
        await _dbContext.SaveChangesAsync();

        return stockModel;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _dbContext.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _dbContext.Stocks.FindAsync(id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel == null)
        {
            return null;
        }

        stockModel.Symbol = stockDto.Symbol;
        stockModel.CompanyName = stockDto.CompanyName;
        stockModel.Purchase = stockDto.Purchase;
        stockModel.LastDiv = stockDto.LastDiv;
        stockModel.Industry = stockDto.Industry;
        stockModel.MarketCap = stockDto.MarketCap;

        await _dbContext.SaveChangesAsync();

        return stockModel;
    }
}