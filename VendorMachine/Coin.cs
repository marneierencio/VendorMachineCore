namespace VendorMachine
{
    public class Coin
    {
        public Coin(string name){
            switch(name)
            {
                case "1.00":
                    Name = name;
                    Value = 1.00m;
                    break;
                case "0.50":
                    Name = name;
                    Value = 0.50m;
                    break;
                case "0.25":
                    Name = name;
                    Value = 0.25m;
                    break;
                case "0.10":
                    Name = name;
                    Value = 0.10m;
                    break;
                case "0.05":
                    Name = name;
                    Value = 0.05m;
                    break;
                case "0.01":
                    Name = name;
                    Value = 0.01m;
                    break;
                default:
                    Name = "Invalid";
                    Value = 0.00m;
                    break;
            }
        }

        public Coin(decimal value){
            switch(value)
            {
                case 1.00m:
                    Name = "1.00";
                    Value = value;
                    break;
                case 0.50m:
                    Name = "0.50";
                    Value = value;
                    break;
                case 0.25m:
                    Name = "0.25";
                    Value = value;
                    break;
                case 0.10m:
                    Name = "0.10";
                    Value = value;
                    break;
                case 0.05m:
                    Name = "0.05";
                    Value = value;
                    break;
                case 0.01m:
                    Name = "0.01";
                    Value = value;
                    break;
                default:
                    Name = "Invalid";
                    Value = 0.00m;
                    break;
            }
        }
        public string Name {get; set;}
        public decimal Value {get; set;}

        public bool Equals(Coin other)
        {
            if(Name == other.Name && Value == other.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj){
            return Equals(obj as Coin);
        }

        public override int GetHashCode(){
            var hashCode = Name + Value.ToString();
            return hashCode.GetHashCode();
        }
    }
}