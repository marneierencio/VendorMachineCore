namespace VendorMachine
{
    public class Coke : Product
    {
        private string name = "Coke";
        private decimal price = 1.50m;

        public Coke(){
            
        }

        public override decimal Price
        {
            get 
            {
                return price;
            }
        }

        public override string Name 
        {
            get
            {
                return name;
            }
        }
    }
}