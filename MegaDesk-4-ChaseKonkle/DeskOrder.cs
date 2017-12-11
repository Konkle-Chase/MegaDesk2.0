using System;
using System.IO;

namespace MegaDesk_4_ChaseKonkle
{
    public class DeskOrder
    {
        //Declarations
        public float quote { get; set; }
        public DateTime quoteDate { get; set; }
        public Desk newDesk { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string rushDays { get; set; }
        public DeskOrder newOrder;
        private int deskArea;
        private float rushCost;
        private float quotePrice;
        private float surfaceCost;
        private const float BASEPRICE = 200;
        private const float DRAWER = 50;
        private const float SMALL_DESK = 1000;
        private const float LARGE_DESK = 2000;

        //DeskOrder constructor
        public DeskOrder (Desk inDesk, string inFirstName, string inLastName, string inRushDays, DateTime inDate )
        {
            newDesk = inDesk;
            firstName = inFirstName;
            lastName = inLastName;
            rushDays = inRushDays;
            quoteDate = inDate;
            quote = CalculatePrice(newDesk, rushDays);
        }

        //Calls the CalculatePrice() function.
        #region CalculatePrice()

        //Function that calculates the price base on the values entered by the user on the addQuoteForm.
        private float CalculatePrice (Desk newDesk, string rushDays)
        {
            float extraAreaCost;

            float drawCost = newDesk.numDrawers * DRAWER;
            deskArea = newDesk.width * newDesk.depth;  
          
            if (deskArea > 1000)
            {
                extraAreaCost = deskArea - 1000;
            }
            else
            {
                extraAreaCost = 0;
            }

            rushCost = DetermineRushCost(rushDays, deskArea);
            surfaceCost = (float) newDesk.surface;
            quotePrice = BASEPRICE + drawCost + extraAreaCost + surfaceCost + rushCost;
            return quotePrice;
        }
        #endregion
        
        //Function for determining the rush price for a desk using price chart made with a 2D array.
        #region DetermineRushCost()
        private float DetermineRushCost(string rushDays, int deskArea)
        {
            string[,] rushPriceChart = GetRushOrder();
            string rushPrice;
            if (rushDays == "3 Days")
            {
                if (deskArea < SMALL_DESK)
                {
                    rushPrice = rushPriceChart[0,0];
                }
                else if (deskArea >= SMALL_DESK && deskArea <= LARGE_DESK)
                {
                    rushPrice = rushPriceChart[0, 1];
                }
                else
                {
                    rushPrice = rushPriceChart[0, 2];
                }
            }
            else if (rushDays == "5 Days")
            {
                if (deskArea < SMALL_DESK)
                {
                    rushPrice = rushPriceChart[1, 0];
                }
                else if (deskArea >= SMALL_DESK && deskArea <= LARGE_DESK)
                {
                    rushPrice = rushPriceChart[1, 1];
                }
                else
                {
                    rushPrice = rushPriceChart[1, 2];
                }
            }
            else if (rushDays == "7 Days")
            {
                if (deskArea < SMALL_DESK)
                {
                    rushPrice = rushPriceChart[2, 0];
                }
                else if (deskArea >= SMALL_DESK && deskArea <= LARGE_DESK)
                {
                    rushPrice = rushPriceChart[2, 1];
                }
                else
                {
                    rushPrice = rushPriceChart[2, 2];
                }
            }
            else
            {
                rushDays = "Standard";
                rushPrice = "0";
            }
            rushCost = float.Parse(rushPrice);
            return rushCost;
        }
        #endregion

        //Function for retrieving the rush price values from a text file and creating chart of the values using a 2D array.
        public string[,] GetRushOrder()
        {
            int c = 0;
            string[] rushPrices = new string[9];
            string[,] rushPriceChart = new string[3, 3];
        
            try
            {
                StreamReader reader = new StreamReader("rushOrderPrices.txt");
                
                for (int i = 0; reader.EndOfStream == false; i++)
                {
                    rushPrices[i] = reader.ReadLine();
                }
                
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        rushPriceChart[x, y] = rushPrices[c];
                        c++;
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return rushPriceChart;
        }
    }
}
