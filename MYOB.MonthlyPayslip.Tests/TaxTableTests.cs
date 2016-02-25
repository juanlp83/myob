using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYOB.MonthlyPayslip.TaxTable;

namespace MYOB.MonthlyPayslip.Tests
{
    [TestClass]
    public class TaxTableTests
    {
        [TestMethod]
        public void New_TaxTable_Should_Be_Empty()
        {
            var table = new IncomeTaxTable();

            Assert.AreEqual(0, table.Entries);
        }
        
        [TestMethod]
        public void Can_Add_New_Entry()
        {
            var table = new IncomeTaxTable();

            table.AddNewEntry(0, 18200, 0);

            Assert.AreEqual(1, table.Entries);
        }

        [TestMethod]
        public void Can_Get_Entry()
        {
            var table = new IncomeTaxTable();

            table.AddNewEntry(0, 18200, 0);

            var entry = table.GetEntry(0, 18200);

            Assert.AreEqual(0, entry.Min);
            Assert.AreEqual(18200, entry.Max);
            Assert.AreEqual(0, entry.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No entry found for $0-$18200")]
        public void Getting_Not_Existing_Entry_Should_Throw()
        {
            var table = new IncomeTaxTable();

            table.GetEntry(0, 18200);
        }

        [TestMethod]
        public void Can_Add_New_Entry_With_Plus()
        {
            var table = new IncomeTaxTable();

            var plusInfo = new PlusInfo {PlusValue = 0.325, PlusOver = 37000};
            table.AddNewEntry(37001, 80000, 3572, plusInfo);

            Assert.AreEqual(1, table.Entries);
        }

        [TestMethod]
        public void Can_Get_Entry_With_Plus()
        {
            var table = new IncomeTaxTable();

            var plusInfo = new PlusInfo { PlusValue = 0.325, PlusOver = 37000 };
            table.AddNewEntry(37001, 80000, 3572, plusInfo);

            var entry = table.GetEntry(37001, 80000);

            Assert.AreEqual(37001, entry.Min);
            Assert.AreEqual(80000, entry.Max);
            Assert.AreEqual(3572, entry.Value);
            Assert.AreEqual(0.325, entry.PlusInfo.PlusValue);
            Assert.AreEqual(37000, entry.PlusInfo.PlusOver);
        }

        [TestMethod]
        public void Can_Get_Entry_Based_On_Salary()
        {
            var table = new IncomeTaxTable();

            table.AddNewEntry(37001, 80000, 3572);

            var entry = table.GetEntry(60050);

            Assert.AreEqual(37001, entry.Min);
            Assert.AreEqual(80000, entry.Max);
        }

        [ExpectedException(typeof(InvalidOperationException), "No entry found for annual salary $60050")]
        public void Getting_Not_Existing_Entry_Based_On_Salary_Should_Throw()
        {
            var table = new IncomeTaxTable();

            table.GetEntry(60050);
        }

        [TestMethod]
        public void Can_Get_Income_Tax()
        {
            var table = new IncomeTaxTable();

            table.AddNewEntry(0, 18200, 0);

            var incomeTax = table.GetIncomeTax(18200);

            Assert.AreEqual(0, incomeTax);
        }

        [TestMethod]
        public void Can_Get_Income_Tax_With_Plus()
        {
            var table = new IncomeTaxTable();

            var plusInfo = new PlusInfo { PlusValue = 0.325, PlusOver = 37000 };
            table.AddNewEntry(37001, 80000, 3572, plusInfo);

            var incomeTax = table.GetIncomeTax(60050);

            Assert.AreEqual(922, incomeTax);
        }
    }

    
}
