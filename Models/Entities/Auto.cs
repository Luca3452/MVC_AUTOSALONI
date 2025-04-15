using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_AUTOSALONI.Models.Entities
{
    public class Auto
    {
        [Key]
        public Guid ID_auto { get; set; }

        public Guid ID_Modello { get; set; }
        [ForeignKey("ID_Modello")]
        [ValidateNever]

        public Modello Modello { get; set; }

        public DateTime data_acquisto { get; set; }

        public string targa {  get; set; }

        public decimal prezzo_offerto { get; set; }


    }
}
