using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stampanti.Models
{
    
        public class StampanteViewModel

        {
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Display(Name = "Stampante")]
            public string IP { get; set; }

            [Display(Name = "Port")]
            public string Port { get; set; }
        }
  
}