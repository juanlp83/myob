namespace MYOB.MonthlyPayslip.Calculator
{
    public class MonthlyPayslipResult
    {
        public string EmployeeFullName { get; set; }
        public string EmployeePayPeriod { get; set; }
        public int GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public int NetIncome { get; set; }
        public int Super { get; set; }
    }
}
