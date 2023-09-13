using BackEnd.Data;
using BackEnd.Models;
using BackEnd.ViewModels;
using BackEnd.ViewModels.Resturants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers;

[ApiController]
public class RestaurantController :ControllerBase
{
    [HttpGet("v1/restaurants")]
    public async Task<IActionResult> GetAsync([FromServices] ApiDataContext context)
    {
        try
        {
            var restaurants = await context.Restaurants.ToListAsync();
            return Ok(new ResultViewModel<List<Restaurant>>(restaurants));
        }
        catch
        {
            return StatusCode(500,new ResultViewModel<string> ("Falha interna no servidor!"));
        }

    }
    [HttpGet("v1/restaurants/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id, [FromServices] ApiDataContext context)
    {
        try
        {
            var restaurant = await context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
                return NotFound(new ResultViewModel<string>("Restaurante não encontrado"));

            return Ok(new ResultViewModel<Restaurant>(restaurant));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor!"));
        }

    }
    [HttpPost("v1/restaurants")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorRestaurantsViewModel model, 
        [FromServices] ApiDataContext context)
    {
        try
        {
            var restaurant = new Restaurant
            {
                Name = model.Name
            };
            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();

            return Created($"v1/restaurants/{restaurant.Id}", new ResultViewModel<Restaurant>(restaurant));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor!"));
        }
    }
}
