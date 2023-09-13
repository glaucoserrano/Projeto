using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels.Vote;

public class VoteViewModel
{
    [Required(ErrorMessage = "Email obrigatório")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage ="Data de Votação obrigatória")]
    public DateTime DateVoting { get; set; }

    [Required(ErrorMessage ="Identificação do restaurante obrigatório")]
    public int RestaurantID { get; set; }
}
