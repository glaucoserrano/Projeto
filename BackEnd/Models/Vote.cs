namespace BackEnd.Models;

public class Vote
{
    public int Id { get; set; }
    public string Email { get; set; }
    
    public DateTime DateVoting { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}
