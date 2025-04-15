using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_AUTOSALONI.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_AUTOSALONI.Models
{
    public class AddAutoViewModel
    {
        public Guid ID_Modello { get; set; }

        public Guid ID_Marca { get; set; }

        public DateTime data_acquisto { get; set; }

        public string targa { get; set; }

        public double prezzo_offerto { get; set; }
    }
}
