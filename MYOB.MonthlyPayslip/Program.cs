using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MYOB.MonthlyPayslip.Calculator;
using MYOB.MonthlyPayslip.Entities;
using MYOB.MonthlyPayslip.FileIO;
using MYOB.MonthlyPayslip.TaxTable;

namespace MYOB.MonthlyPayslip
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Count() == 2)
            {
                var inputFilename = args.First();
                var outputFilename = args.Last();

                if (File.Exists(inputFilename))
                {
                    Console.WriteLine("Reading input file: " + inputFilename);
                    var employees = GetEmployeesFromFile(inputFilename);

                    Console.WriteLine("Running Calculations...");
                    var calculationResults = GetCalculationResults(employees);

                    Console.WriteLine("Exporting results to file: " + outputFilename);
                    ExportResults(outputFilename, calculationResults);

                    Console.WriteLine("Done. Press any key to exit");
                }
                else
                {
                    Console.WriteLine("The input file: " + inputFilename + " doesn't exist");
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Missing parameters. Example: MYOB.MonthlyPayslip.exe jsonInputFile.json jsonOutputFile.json");
            }
        }

        private static void ExportResults(string outputFilename, List<MonthlyPayslipResult> calculationResults)
        {
            var jsonExporter = new JsonExporter();
            jsonExporter.Export(outputFilename, calculationResults);
        }

        private static IEnumerable<Employee> GetEmployeesFromFile(string inputFilename)
        {
            var inputReader = new JsonInputReader();
            var employees = inputReader.ReadEmployeeData(inputFilename);
            return employees;
        }

        private static List<MonthlyPayslipResult> GetCalculationResults(IEnumerable<Employee> employees)
        {
            var incomeTaxTable = GetIncomeTaxTable();
            var calculator = new MonthlyPayslipCalculator(incomeTaxTable);

            var calculationResults = new List<MonthlyPayslipResult>();

            foreach (var employee in employees)
            {
                calculationResults.Add(calculator.Calculate(employee));
            }

            return calculationResults;
        }

        private static IncomeTaxTable GetIncomeTaxTable()
        {
            var table = new IncomeTaxTable();

            table.AddNewEntry(0, 18200, 0);
            table.AddNewEntry(37001, 80000, 3572, new PlusInfo { PlusValue = 0.325, PlusOver = 37000 });
            table.AddNewEntry(80001, 180000, 17547, new PlusInfo { PlusValue = 0.37, PlusOver = 80000 });

            return table;
        }
    }
}
