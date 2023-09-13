namespace BackEnd.Models;

public class ChooseRestaurant
{
    public int Id { get; set; }
    public DateTime DateVoting { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public int WeekChoose { get; set; }
}
