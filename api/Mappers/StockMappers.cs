using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto toStockDto(this Stocks stockModel){
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol
                ,Industry = stockModel.Industry,
                CompanyName = stockModel.CompanyName
                ,MarketCap = stockModel.MarketCap,
                comments = stockModel.comments.Select(c => c.ToCommentDto()).ToList()
            
            };
        }
        public static Stocks ToStockFromCreateDTO(this CreateStockRequest stockDto)
        {
            
            return new Stocks
            {
                Symbol = stockDto.Symbol,
                Industry = stockDto.Industry,
                CompanyName = stockDto.CompanyName,
                MarketCap = stockDto.MarketCap
                
            };
        
        }

    }
}