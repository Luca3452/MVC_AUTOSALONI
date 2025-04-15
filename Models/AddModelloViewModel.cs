using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_AUTOSALONI.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_AUTOSALONI.Models
{
    public class AddModelloViewModel
    {
        public string modello { get; set; }
        public Guid ID_marca { get; set; }
    }
}
