using System;

namespace VendorMachine.View
{
    public class CLI
    {
        private Logic logic;
        
        public CLI(){
            logic = new Logic();
        }
        
        public void Run(){
            while(true){
                Console.Write("Input: ");
                //Wait a input line and print the result of the method, the output
                Console.WriteLine("Output: " + logic.Input(Console.ReadLine()));
            }
        }

        public void RunWithStatus(){
            while(true){
                Console.Write(logic.GetStatus());
                Console.Write("Input: ");
                //Wait a input line and print the result of the method, the output
                Console.WriteLine("Output: " + logic.Input(Console.ReadLine()));
            }
        }
    }
}