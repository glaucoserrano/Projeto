using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels.Choose;

public class ChooseRestaurantViewModel
{
    [Required(ErrorMessage = "Data de Votação obrigatória")]
    public DateTime DateVoting { get; set; }

    [Required(ErrorMessage = "Identificação do restaurante obrigatório")]
    public int RestaurantID { get; set; }
    public int WeekChoose { get; set; }
}
