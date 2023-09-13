using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels.Resturants;

public class EditorRestaurantsViewModel
{
    [Required(ErrorMessage ="Nome do restaurante é obrigatório")]
    [StringLength(80, MinimumLength =3, ErrorMessage ="Este campo deve conter de 3 a 80 caracteres")]
    public string Name { get; set; }
}
