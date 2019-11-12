using System;

namespace VendorMachine
{
    public abstract class Product
    {
        public virtual string Name {get; set;}
        public virtual decimal Price {get; set;}

        public bool Equals(Product other)
        {
            if(Name == other.Name && Price == other.Price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj){
            return Equals(obj as Product);
        }

        public override int GetHashCode(){
            var hashCode = Name + Price.ToString();
            return hashCode.GetHashCode();
        }
    }
}