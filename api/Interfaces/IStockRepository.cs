using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stocks>> GetAllAsync(QueryObject query);
        Task<Stocks?> GetByIdAsync(int id); //firstOrDefualt can return null

        Task<Stocks> GetBySymbolAsync(string symbol);

        Task<Stocks> CreateAsync(Stocks stock);

        Task<Stocks?> UpdateAsync(int id, UpdateStockRequestDto stockdto);

        Task<Stocks> DeleteAsync(int id);

        Task<bool> StockExists(int id);


    }
}