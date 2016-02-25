using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYOB.MonthlyPayslip.FileIO;

namespace MYOB.MonthlyPayslip.Tests
{
    [TestClass]
    public class JsonInputReaderTests
    {
        [TestMethod]
        public void Can_Read_Input_File_With_Valid_Data()
        {
            var reader = new JsonInputReader();
            var filename = (Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\TestFiles\\InputFile.json"));

            var employees = reader.ReadEmployeeData(filename).ToList();

            Assert.AreEqual(2, employees.Count());

            var firstEmployee = employees.First();

            Assert.AreEqual("David", firstEmployee.FirstName);
            Assert.AreEqual("Rudd", firstEmployee.LastName);
            Assert.AreEqual(60050, firstEmployee.Salary);
            Assert.AreEqual(9, firstEmployee.SuperRate);
            Assert.AreEqual("01 March to 31 March", firstEmployee.PayPeriod);

            var lastEmployee = employees.Last();

            Assert.AreEqual("Ryan", lastEmployee.FirstName);
            Assert.AreEqual("Chen", lastEmployee.LastName);
            Assert.AreEqual(120000, lastEmployee.Salary);
            Assert.AreEqual(10, lastEmployee.SuperRate);
            Assert.AreEqual("01 March to 31 March", lastEmployee.PayPeriod);
        }
    }
}
