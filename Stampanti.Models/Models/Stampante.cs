using Dapper.Contrib.Extensions;
using FluentValidation.Attributes;
using Stampanti.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stampanti.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Stampanti")]
    [Validator(typeof(StampantiValidator))]
    public partial class Stampante
    {
        public int Extra { get; set; }
        [Computed]
        public bool ExtraBool { get { return Extra == 1; } set { Extra = value ? 1 : 0; } }
        public string PCName { get; set; }
        public int Id { get; set; }
        //[Required(ErrorMessage ="Il nome è richiesto")]
        public string Nome { get; set; }
        //[Required(ErrorMessage = "Un IP è richiesto"), RegularExpression(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b", ErrorMessage = "Formto IP non valido")]
        public string IP { get; set; }
        //[Required(ErrorMessage ="Un tipo di Port è richiesto")]
        public int? Port { get; set; }
        
        
    }
}