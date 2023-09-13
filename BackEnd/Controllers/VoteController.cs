using BackEnd.Data;
using BackEnd.Models;
using BackEnd.ViewModels;
using BackEnd.ViewModels.Choose;
using BackEnd.ViewModels.Vote;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BackEnd.Controllers;
[ApiController]
public class VoteController :ControllerBase
{
    [HttpPost("v1/vote")]
    public async Task<IActionResult> PostVoteAsync(
        [FromBody] VoteViewModel model,
        [FromServices] ApiDataContext context)
    {
        try
        {
            if (model == null)
                return NotFound(new ResultViewModel<string>("Modelo de Dados está vazio"));

            var restaurant = await context.Restaurants.FirstOrDefaultAsync(x => x.Id == model.RestaurantID);

            if (restaurant == null)
                return NotFound(new ResultViewModel<string>("Restaurante não cadastrado"));

            var voting = await context.Votes.FirstOrDefaultAsync(x => x.Email == model.Email && x.DateVoting == model.DateVoting);

            if (voting != null)
                return Ok(new ResultViewModel<string>("Esse e-mail já votou hoje"));

            int week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(model.DateVoting, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var chooseRestaurant = await context.ChooseRestaurants.FirstOrDefaultAsync(x => x.WeekChoose == week && x.RestaurantId == model.RestaurantID);

            if (chooseRestaurant != null)
                return Ok(new ResultViewModel<string>("Esse restaurante ja foi votado essa semana, por favor escolha outro"));

            var uservoting = new Vote
            {
                Email = model.Email,
                DateVoting = model.DateVoting,
                RestaurantId = model.RestaurantID
            };

            await context.Votes.AddAsync(uservoting);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Vote>(uservoting));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor"));
        }
    }
    [HttpGet("v1/rank")]
    public async Task<IActionResult> rank(
        [FromBody] VoteCountViewModel model, 
        [FromServices]ApiDataContext context)
    {
        try
        {
            var allRestaurants = await context.Restaurants.ToListAsync();

            var ranking = await context.Votes
                .Where(x => x.DateVoting == model.DateVoting)
                .GroupBy(x => new
                {
                    x.RestaurantId,
                    x.Restaurant.Name
                })
                .Select(x => new RankViewModel
                {
                    RestaurantId = x.Key.RestaurantId,
                    Name = x.Key.Name,
                    count = x.Count()
                }).ToListAsync();

            var results = allRestaurants.Select(restaurant => new RankViewModel
            {
                RestaurantId = restaurant.Id,
                Name = restaurant.Name,
                count = ranking.FirstOrDefault(x => x.RestaurantId == restaurant.Id)?.count ?? 0
            }).OrderByDescending(x => x.count).ToList();
            return Ok(new ResultViewModel<List<RankViewModel>>(results));
                
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor"));
        }
    }
    [HttpPost("v1/choose")]
    public async Task<IActionResult> choose(
        [FromBody] ChooseRestaurantViewModel model,
        [FromServices] ApiDataContext context)
    {
        try
        {
            int week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(model.DateVoting, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var choose = new ChooseRestaurant
            {
                DateVoting = model.DateVoting,
                RestaurantId = model.RestaurantID,
                WeekChoose = week
            };
            await context.ChooseRestaurants.AddAsync(choose);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<ChooseRestaurant>(choose));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor"));
        }
    }
}
