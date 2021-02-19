using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampanti.Models.Validators
{
    public class StampantiValidator: AbstractValidator<Stampante>
    {
        public StampantiValidator()
        {
            RuleFor(x => x.PCName).NotEmpty().WithMessage("Il nome del PC è richiesto");
            RuleFor(x => x.Extra).NotNull().WithMessage("Un qualsiasi valore diverso da 0 identifica la stampante come Extra");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Il nome è richiesto");
            RuleFor(x => x.IP).NotEmpty().Matches(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b").WithMessage("L'indirizzo IP è errato");
            RuleFor(x => x.Port).NotEmpty().WithMessage("Un tipo di Port è richiesto");
        }
    }
}
