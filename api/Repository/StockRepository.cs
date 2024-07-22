using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDBContext _context;
        public StockRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Stocks> CreateAsync(Stocks stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stocks?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<List<Stocks>> GetAllAsync(QueryObject query)
        {
            var stocks =  _context.Stocks.AsQueryable();

            if(query.includeComments)
            {
                stocks = stocks.Include(c => c.comments);
            }

            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrEmpty(query.Symbol)){
                stocks = stocks.Where(x => x.Symbol.Contains(query.Symbol));
            }

            //sorting
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("CompanyName",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.isDecending ? stocks.OrderByDescending(x => x.CompanyName) : stocks.OrderBy(x => x.CompanyName);
                }
            }
            
            //pagination
            var SkipNumber = (query.PageNumber - 1) * query.PageSize;


            return await stocks.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stocks?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stocks> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stocks?> UpdateAsync(int id, UpdateStockRequestDto UpdateStockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            { return null;}

            existingStock.Symbol = UpdateStockDto.Symbol;
            existingStock.CompanyName = UpdateStockDto.CompanyName;
            existingStock.Purchase = UpdateStockDto.Purchase;
            existingStock.LastDiv = UpdateStockDto.LastDiv;
            existingStock.CompanyName = UpdateStockDto.CompanyName;
            existingStock.Industry = UpdateStockDto.Industry;
            existingStock.MarketCap =  UpdateStockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}