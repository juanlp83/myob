using System;

namespace MYOB.MonthlyPayslip.Entities
{
    [Serializable]
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public int SuperRate { get; set; }
        public string PayPeriod { get; set; }
    }
}
