using NUnit.Framework;

namespace VendorMachineTests
{
    public class AdditionalUnitTest
    {
        //Test NO_CHANGE 
        [Test]
        public void JustChange()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("CHANGE");

            Assert.AreEqual("=0.00 NO_CHANGE", result);
        }

        //Test input without products
        [Test]
        public void Insert1ChangeSameTransaction()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("1.00 CHANGE");

            Assert.AreEqual("=1.00 1.00", result);
        }

        //Test 2 distinct transactions and memory state
        //Test input just with CHANGE
        [Test]
        public void Insert1ChangeDistinctTransactions()
        {
            var logic = new VendorMachine.Logic();
            logic.Input("1.00");
            var result = logic.Input("CHANGE");
            
            Assert.AreEqual("=1.00 1.00", result);
        }

        //Test NO_COINS result
        [Test]
        public void Request9Pastelinas()
        {
            var logic = new VendorMachine.Logic();
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            logic.Input("0.50 Pastelina CHANGE");
            var result = logic.Input("0.50 Pastelina CHANGE");
            
            Assert.AreEqual("NO_COINS =0.50", result);
        }

        //Test NO_PRODUCT result
        [Test]
        public void RequestElevenCokes()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("1.00 1.00 1.00 1.00 1.00 1.00 1.00 1.00 1.00 1.00 1.00 Coke Coke Coke Coke Coke Coke Coke Coke Coke Coke Coke");
            
            Assert.AreEqual("NO_PRODUCT =11.00", result);
        }

    }
}