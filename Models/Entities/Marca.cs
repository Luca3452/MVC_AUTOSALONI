using System.ComponentModel.DataAnnotations;

namespace MVC_AUTOSALONI.Models.Entities
{
    public class Marca
    {
        [Key]
        public Guid Id_Marca { get; set; } 
        public string marca { get; set; }
    }
}
