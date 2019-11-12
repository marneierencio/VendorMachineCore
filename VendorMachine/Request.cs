using System.Collections.Generic;

namespace VendorMachine
{
    public class Request
    {
        public Request(){
            Products = new List<Product>();
            Coins = new List<Coin>();
        }
        public List<Product> Products {get; set;}

        public List<Coin> Coins {get; set;}

        public bool ChangeRequested {get; set;}

        public decimal ValueInserted
        {
            get
            {
                var value = 0.00m;
                
                foreach(var coin in Coins){
                    value += coin.Value;
                }

                return value;
            }
        }
    }
}