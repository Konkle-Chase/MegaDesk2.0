using System;

namespace MegaDesk_4_ChaseKonkle
{
    public class DeskOrderQuote
    {
        //Declarations
        public Desk newDesk;
        public DeskOrder newOrder;

        public string quoteDate { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public float width { get; set; }
        public float depth { get; set; }
        public float numDrawers { get; set; }
        public string surface { get; set; }
        public string rushDays { get; set; }
        public float quote { get; set; }

        //DeskOrderQuote constructer 
        public DeskOrderQuote(Desk newDesk, DeskOrder newOrder)
        {
            quoteDate = newOrder.quoteDate.ToString();
            lastName = newOrder.lastName;
            firstName = newOrder.firstName;
            width = newDesk.width;
            depth = newDesk.depth;
            numDrawers = newDesk.numDrawers;
            surface = newDesk.surface.ToString();
            rushDays = newOrder.rushDays;
            quote = newOrder.quote;
        }
    }
}
