using System.Collections.Generic;

namespace VendorMachine
{
    public class VendorMachine
    {
        public VendorMachine(){
            for(int i=0; i<10; i++){
                Products.Add(new Coke());
                Products.Add(new Water());
                Products.Add(new Pastelina());

                MachineCoins.Add(new Coin("1.00"));
                MachineCoins.Add(new Coin("0.50"));
                MachineCoins.Add(new Coin("0.25"));
                MachineCoins.Add(new Coin("0.10"));
                MachineCoins.Add(new Coin("0.05"));
                MachineCoins.Add(new Coin("0.01"));
            }
        }

        public List<Product> Products = new List<Product>();
        public List<Coin> MachineCoins = new List<Coin>();
        public List<Coin> CustomerCoins = new List<Coin>();

        public List<Coin> AcceptableCoins = new List<Coin>()
        {
            new Coin("1.00"),
            new Coin("0.50"),
            new Coin("0.25"),
            new Coin("0.10"),
            new Coin("0.05"),
            new Coin("0.01")
        };

        public string Status(){
            int coke = 0, water = 0, pastelina = 0;

            foreach(var product in Products){
                if(product.Name == "Coke"){
                    coke++;
                }
                else if(product.Name == "Water"){
                    water++;
                }
                else if(product.Name == "Pastelina"){
                    pastelina++;
                }
            }

            var result = "\n\n------------------------------------\n";
            result += "Vendor Machine\n\n";
            result += "Machine coins: " + CoinsString(MachineCoins) + "\n\n";
            
            result += "Stock\n";
            result += "Coke: " + coke + "\n";
            result += "Water: " + water + "\n";
            result += "Pastelina: " + pastelina + "\n\n";

            result += "Inserted coins: " + CoinsString(CustomerCoins) + "\n";
            result += "Total amount: " + TotalValue(CustomerCoins).ToString().Replace(",",".") + "\n";

            return result;
        }

        private decimal TotalValue(List<Coin> list)
        {
            if (list is null)
            {
                return 0.00m;
            }

            var result = 0m;

            foreach(var item in list)
            {
                result += item.Value;
            }
            return result;
        }

        private string CoinsString(List<Coin> coins){
            var result = "{ ";
            
            foreach(var ac in AcceptableCoins){
                result +="[" + ac.Name + ":";
                var count = 0;
                foreach(var c in coins){
                    if(c.Name == ac.Name)
                        count++;
                }
                result += count +"] ";
            }

            return result + "}";
        }
    }
}