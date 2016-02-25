using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYOB.MonthlyPayslip.Calculator;
using MYOB.MonthlyPayslip.Entities;
using MYOB.MonthlyPayslip.TaxTable;

namespace MYOB.MonthlyPayslip.Tests
{
    [TestClass]
    public class MonthlyPayslipCalculatorTests
    {
        [TestMethod]
        public void Can_Calculate_Monthly_Payslip_For_David_Rudd()
        {
            var incomeTaxTable = GetTaxTable();
            var calculator = new MonthlyPayslipCalculator(incomeTaxTable);

            var employee = new Employee
            {
                FirstName = "David",
                LastName = "Rudd",
                PayPeriod = "01 March – 31 March",
                Salary = 60050,
                SuperRate = 9
            };


            var result = calculator.Calculate(employee);

            Assert.AreEqual("David Rudd", result.EmployeeFullName);
            Assert.AreEqual("01 March – 31 March", result.EmployeePayPeriod);
            Assert.AreEqual(5004, result.GrossIncome);
            Assert.AreEqual(922, result.IncomeTax);
            Assert.AreEqual(4082, result.NetIncome);
            Assert.AreEqual(450, result.Super);
        }

        [TestMethod]
        public void Can_Calculate_Monthly_Payslip_For_Ryan_Chen()
        {
            var incomeTaxTable = GetTaxTable();
            var calculator = new MonthlyPayslipCalculator(incomeTaxTable);

            var employee = new Employee
            {
                FirstName = "Ryan",
                LastName = "Chen",
                PayPeriod = "01 March – 31 March",
                Salary = 120000,
                SuperRate = 10
            };


            var result = calculator.Calculate(employee);

            Assert.AreEqual("Ryan Chen", result.EmployeeFullName);
            Assert.AreEqual("01 March – 31 March", result.EmployeePayPeriod);
            Assert.AreEqual(10000, result.GrossIncome);
            Assert.AreEqual(2696, result.IncomeTax);
            Assert.AreEqual(7304, result.NetIncome);
            Assert.AreEqual(1000, result.Super);
        }

        private static IncomeTaxTable GetTaxTable()
        {
            var table = new IncomeTaxTable();

            table.AddNewEntry(0, 18200, 0);
            
            table.AddNewEntry(37001, 80000, 3572, new PlusInfo { PlusValue = 0.325, PlusOver = 37000 });

            table.AddNewEntry(80001, 180000, 17547, new PlusInfo { PlusValue = 0.37, PlusOver = 80000 });

            return table;
        }
    }
}
