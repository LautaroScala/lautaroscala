using System.ComponentModel.DataAnnotations;
using ThingsLoan.WebAPI.Entities;

namespace ThingsLoan.API.Models
{
    public class ThingsViewModel : Things
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="La descripción es obligatoria.")]
        [MinLength(5, ErrorMessage="La descripcion debe tener al menos 5 caracteres")]
        public string Desc { get; set; }
    }
}
