namespace VendorMachine
{
    public class Water : Product
    {
        private string name = "Water";
        private decimal price = 1.00m;

        public Water(){
            
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