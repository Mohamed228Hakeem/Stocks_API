using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepoistory _portfolioRepoistory;
        public PortfolioController(UserManager<AppUser> userManager,
        IStockRepository stockRepository,IPortfolioRepoistory portfolioRepoistory)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepoistory = portfolioRepoistory;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var AppUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepoistory.GetUserPortfolio(AppUser);

            return Ok(userPortfolio);
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortoflio(string symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepoistory.GetUserPortfolio(appUser);

            if(userPortfolio.Any(e=>e.Symbol.ToLower() == symbol.ToLower())) return BadRequest ("Cannot Add duplicate Stocks");

            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id

            };
            await _portfolioRepoistory.CreateAsync(portfolioModel);

            if (portfolioModel == null) 
            {
                return StatusCode(500,"Cannot Create the Portfolio");
            }

            else 
            {
                return StatusCode(201,"Portfolio Created");
            }


        }


         [HttpDelete]
         [Authorize]
         public async Task<IActionResult> DeletePortfolio(string symbol)
         {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepoistory.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(s=>s.Symbol.ToLower() == symbol.ToLower());

            if(filteredStock.Count() == 1) 
            {
                await _portfolioRepoistory.DeleteAsync(appUser,symbol);

            }else
            {
                return BadRequest("stock not in your Portfolio");
            }
            return Ok($"Stock with the symbol {symbol} is Deleted Successfully");

         }
    }
}