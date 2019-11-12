using System;
using System.Collections.Generic;
using System.Linq;

namespace VendorMachine
{
    public class Logic
    {
        private VendorMachine vendorMachine;

        public Logic()
        {
            vendorMachine = new VendorMachine();
        }

        ///<sumary>This method returns a auxiliar string with additional information to debug</summary>
        public string GetStatus()
        {
            return vendorMachine.Status();
        }

        ///<sumary>This method process a input string like [[Value coin] ...] [[Product name] ...] [CHANGE][</summary>
        ///<param name="input">String that contains the coins, products and options</param>
        public string Input(string input)
        {
            var output = "";
            var request = ConvertInput(input);
            
            // Check if request is valid
            if(request == null)
            {
                return "INVALID_INPUT =" + TotalValue(vendorMachine.CustomerCoins).ToString().Replace(",",".");
            }

            //Join the request coins to the amount already entered by the user
            foreach(var coin in request.Coins)
            {
                vendorMachine.CustomerCoins.Add(coin);
            }
            
            // Check if have products enough
            var productsTemp = new List<Product>(vendorMachine.Products);
            foreach(var product in request.Products)
            {
                if(!(productsTemp.Remove(product)))
                {
                    return "NO_PRODUCT =" + TotalValue(vendorMachine.CustomerCoins).ToString().Replace(",",".");
                }
            }

            //Check if customer inserted coins enough
            if(!(TotalValue(vendorMachine.CustomerCoins) >= TotalValue(request.Products)))
            {
                return "NO_COINS_ENOUGH =" + TotalValue(vendorMachine.CustomerCoins).ToString().Replace(",",".");
            }

            //Check if have change enough
            if(!HaveCoinsToChange(TotalValue(vendorMachine.CustomerCoins) - TotalValue(request.Products)))
            {
                return "NO_COINS =" + TotalValue(vendorMachine.CustomerCoins).ToString().Replace(",",".");
            }
            else
            {
                //Proceed to sale
                
                var customerBalance = TotalValue(vendorMachine.CustomerCoins);
                
                //Move all coins to from customercoins to machinecoins
                MoveCoins(ref vendorMachine.CustomerCoins, ref vendorMachine.MachineCoins, customerBalance);
                //move change(diff between customercoins and value of requested products) coins to customercoins
                MoveCoins(ref vendorMachine.MachineCoins, ref vendorMachine.CustomerCoins, customerBalance - TotalValue(request.Products));

                var newCustomerBalance = TotalValue(vendorMachine.CustomerCoins);

                foreach(var product in request.Products)
                {
                    //create output PRODUCT and BALANCE
                    vendorMachine.Products.Remove(product);
                    customerBalance -= product.Price;
                    output += product.Name + " =" + customerBalance.ToString().Replace(",",".") + " "; 
                }

                if(request.ChangeRequested)
                {
                    if(customerBalance == 0.00m)
                    {
                        output += "NO_CHANGE";
                    }
                    else
                    {
                        foreach(var coin in vendorMachine.CustomerCoins)
                        {
                            //Add customer coins to output
                            output += coin.Name + " ";
                        }
                        vendorMachine.CustomerCoins = new List<Coin>();
                    }
                }
            }
            return output.TrimEnd();
        }

        ///<sumary>Transfer the value in coins from origin list to destination list</summary>
        ///<param name="origin">List of coins (passing by reference) from which the amount entered will be debited</param>
        ///<param name="destination">List of coins (passing by reference) where the entered amount will be credited</param>
        ///<param name="value">Amount that will be transfered</param>
        private void MoveCoins(ref List<Coin> origin, ref List<Coin> destination, decimal value)
        {
            foreach(var acceptableCoin in vendorMachine.AcceptableCoins.OrderByDescending(c => c.Value))
            {
                while(value >= acceptableCoin.Value && origin.Contains(acceptableCoin))
                {
                    value -= acceptableCoin.Value;
                    origin.Remove(acceptableCoin);
                    destination.Add(acceptableCoin);
                }
            }
        }

        ///<sumary>Verify if Machine have coins enough to change the entered amount</summary>
        ///<param name="value">Amount to verify if have coins enough to change</param>
        private bool HaveCoinsToChange(decimal value)
        {
            var result = false;
            var machineCoinsTemp = new List<Coin>(vendorMachine.MachineCoins);
            foreach(var acceptableCoin in vendorMachine.AcceptableCoins.OrderByDescending(c => c.Value))
            {
                while(value >= acceptableCoin.Value && machineCoinsTemp.Contains(acceptableCoin))
                {
                    value -= acceptableCoin.Value;
                    machineCoinsTemp.Remove(acceptableCoin);
                }
            }
            if(value == 0.00m)
            {
                result = true;
            }
            return result;
        }

        ///<sumary>This method converts a input string in a object Request. [[Value coin] ...] [[Product name] ...] [CHANGE][</summary>
        ///<param name="input">String that contains the coins, products and options</param>
        private Request ConvertInput(string input)
        {
            var request = new Request();
            var splittedInput = input.Split(" ");
            
            foreach(var arg in splittedInput){
                switch(arg)
                {
                    case "1.00":
                        request.Coins.Add(new Coin("1.00"));
                        break;

                    case "0.50":
                        request.Coins.Add(new Coin("0.50"));
                        break;
                    
                    case "0.25":
                        request.Coins.Add(new Coin("0.25"));
                        break;
                    
                    case "0.10":
                        request.Coins.Add(new Coin("0.10"));
                        break;
                    
                    case "0.05":
                        request.Coins.Add(new Coin("0.05"));
                        break;
                    
                    case "0.01":
                        request.Coins.Add(new Coin("0.01"));
                        break;

                    case "Coke":
                        request.Products.Add(new Coke());
                        break;

                    case "Water":
                        request.Products.Add(new Water());
                        break;

                    case "Pastelina":
                        request.Products.Add(new Pastelina());
                        break;

                    case "CHANGE":
                        request.ChangeRequested = true;
                        break;
                }
            }
            request.Coins = request.Coins.OrderByDescending(c => c.Value).ToList();
            return request;
        }


        ///<sumary>Return a sum of prices of all products from the list</summary>
        ///<param name="list">List of products</param>
        private decimal TotalValue(ICollection<Product> list)
        {
            var result = 0.00m;

            if (list is null)
            {
                return result;
            }

            foreach(var item in list)
            {
                result += item.Price;
            }
            return result;
        }

        ///<sumary>Return a sum of values of all coins from the list</summary>
        ///<param name="list">List of coins</param>
        private decimal TotalValue(List<Coin> list)
        {
            var result = 0.00m;

            if (list is null)
            {
                return result;
            }

            foreach(var item in list)
            {
                result += item.Value;
            }
            return result;
        }
    }
}