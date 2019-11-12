namespace VendorMachine
{
    public class Pastelina : Product
    {
        private string name = "Pastelina";
        private decimal price = 0.30m;
        
        public Pastelina(){
            
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