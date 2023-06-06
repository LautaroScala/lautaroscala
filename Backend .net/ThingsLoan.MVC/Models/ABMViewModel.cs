using DB.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThingsLoan.MVC.Models
{
    public class ABMViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Descripcion")]
        public string Desc { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Categories { get; set; }
        [Display(Name = "Disponible")]
        public bool Available { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
