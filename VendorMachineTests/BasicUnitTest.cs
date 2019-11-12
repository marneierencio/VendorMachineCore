using NUnit.Framework;

namespace VendorMachineTests
{
    
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SampleTest1()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("0.50 1.00 Coke");

            Assert.AreEqual("Coke =0.00", result);
        }

        [Test]
        public void SampleTest2()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("1.00 Pastelina CHANGE");

            Assert.AreEqual("Pastelina =0.70 0.50 0.10 0.10", result);
        }

        [Test]
        public void SampleTest3()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("0.25 0.05 Pastelina CHANGE");

            Assert.AreEqual("Pastelina =0.00 NO_CHANGE", result);
        }

        [Test]
        public void SampleTest4()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("1.00 Pastelina Pastelina Pastelina");

            Assert.AreEqual("Pastelina =0.70 Pastelina =0.40 Pastelina =0.10", result);
        }

        [Test]
        public void JustChange()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("CHANGE");

            Assert.AreEqual("=0.00 NO_CHANGE", result);
        }

        [Test]
        public void Insert1Change1SameTransaction()
        {
            var logic = new VendorMachine.Logic();
            var result = logic.Input("1.00 CHANGE");

            Assert.AreEqual("1.00", result);
        }

        [Test]
        public void Insert1TransactionChange1Transaction()
        {
            var logic = new VendorMachine.Logic();
            logic.Input("1.00");
            var result = logic.Input("CHANGE");
            
            Assert.AreEqual("=1.00 0.00", result);
        }


    }
}