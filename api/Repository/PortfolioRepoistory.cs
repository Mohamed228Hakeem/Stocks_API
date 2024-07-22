using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepoistory : IPortfolioRepoistory
    {

        private readonly AppDBContext _context;

        public PortfolioRepoistory(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeleteAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.portfolios.FirstOrDefaultAsync(x=> x.AppUserId == appUser.Id && x.Stocks.Symbol.ToLower()==symbol.ToLower());
            if (portfolioModel == null)return null;

            _context.portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stocks>> GetUserPortfolio(AppUser user)
        {
            return await _context.portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stocks
            {
                Id = stock.StockId,
                Symbol = stock.Stocks.Symbol,
                CompanyName = stock.Stocks.CompanyName,
                Purchase =stock.Stocks.Purchase,
                LastDiv = stock.Stocks.LastDiv,
                Industry = stock.Stocks.Industry,
                MarketCap = stock.Stocks.MarketCap

            }).ToListAsync();
        }
    }
}