using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stampanti.Models
{
    public partial class Stampante
    {
       
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Il nome è richiesto")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Un IP è richiesto"), RegularExpression(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b", ErrorMessage = "Formto IP non valido")]
        public string IP { get; set; }
        [Required(ErrorMessage ="Un tipo di Port è richiesto")]
        public int? Port { get; set; }
        
        
    }
}