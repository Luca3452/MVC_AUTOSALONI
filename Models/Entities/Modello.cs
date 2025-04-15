using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_AUTOSALONI.Models.Entities
{
    public class Modello
    {
        [Key]
        public Guid ID_modello { get; set; }
        public string modello { get; set; }
        public Guid ID_marca { get; set; }
        [ForeignKey("ID_marca")]
        [ValidateNever]
        public Marca Marca { get; set; }
    }
}
