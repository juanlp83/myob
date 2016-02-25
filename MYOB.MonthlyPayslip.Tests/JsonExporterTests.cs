using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYOB.MonthlyPayslip.Calculator;
using MYOB.MonthlyPayslip.FileIO;
using Newtonsoft.Json;

namespace MYOB.MonthlyPayslip.Tests
{
    [TestClass]
    public class JsonExporterTests
    {
        [TestMethod]
        public void Can_Export_Calculator_Results_To_Json_File()
        {
            var exporter = new JsonExporter();
            const string outputFile = "test.json";
            var results = new List<MonthlyPayslipResult>
            {
                new MonthlyPayslipResult
                {

                    EmployeeFullName = "Test Test",
                    EmployeePayPeriod = "PayPeriod",
                    GrossIncome = 20,
                    IncomeTax = 10,
                    NetIncome = 449,
                    Super = 9
                },
                new MonthlyPayslipResult
                {

                    EmployeeFullName = "Test1 Test1",
                    EmployeePayPeriod = "PayPeriod1",
                    GrossIncome = 20,
                    IncomeTax = 10,
                    NetIncome = 449,
                    Super = 9
                }
            };

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            exporter.Export(outputFile, results);

            var result = JsonConvert.DeserializeObject<IEnumerable<MonthlyPayslipResult>>(File.ReadAllText(outputFile)).ToList();

            Assert.AreEqual(2, result.Count());
            var firstItem = result.First();
            Assert.AreEqual("Test Test", firstItem.EmployeeFullName);
            Assert.AreEqual("PayPeriod", firstItem.EmployeePayPeriod);

            Assert.AreEqual(20, firstItem.GrossIncome);
            Assert.AreEqual(10, firstItem.IncomeTax);
            Assert.AreEqual(449, firstItem.NetIncome);
            Assert.AreEqual(9, firstItem.Super);
        }
    }
}
