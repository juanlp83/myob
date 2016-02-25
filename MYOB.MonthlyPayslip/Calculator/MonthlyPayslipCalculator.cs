using System;
using MYOB.MonthlyPayslip.Entities;
using MYOB.MonthlyPayslip.TaxTable;

namespace MYOB.MonthlyPayslip.Calculator
{
    public class MonthlyPayslipCalculator
    {
        private readonly IncomeTaxTable _taxTable;

        public MonthlyPayslipCalculator(IncomeTaxTable taxTable)
        {
            _taxTable = taxTable;
        }

        public MonthlyPayslipResult Calculate(Employee employee)
        {
            var result = new MonthlyPayslipResult
            {
                EmployeeFullName = employee.FirstName + " " + employee.LastName,
                EmployeePayPeriod = employee.PayPeriod,
                GrossIncome = (int) Math.Floor((double) employee.Salary/12),
                IncomeTax = _taxTable.GetIncomeTax(employee.Salary)
            };

            result.NetIncome = result.GrossIncome - result.IncomeTax;
            result.Super = (int)Math.Floor(result.GrossIncome*(employee.SuperRate*0.01));

            return result;
        }
    }
}
