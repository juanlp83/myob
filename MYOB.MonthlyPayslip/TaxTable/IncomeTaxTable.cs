using System;
using System.Collections.Generic;
using System.Linq;

namespace MYOB.MonthlyPayslip.TaxTable
{
    public class IncomeTaxTable
    {
        private readonly List<TaxEntry> _entries;

        public int Entries
        {
            get { return _entries.Count; }
        }

        public IncomeTaxTable()
        {
            _entries = new List<TaxEntry>();
        }

        public void AddNewEntry(int min, int max, int value)
        {
            var entry = new TaxEntry(min, max, value);
            _entries.Add(entry);
        }

        public void AddNewEntry(int min, int max, int value, PlusInfo plusInfo)
        {
            var entry = new TaxEntry(min, max, value, plusInfo);
            _entries.Add(entry);
        }

        public TaxEntry GetEntry(int annualSalary)
        {
            var entry = _entries.FirstOrDefault(o => o.Min <= annualSalary && annualSalary <= o.Max);

            if (entry != null)
                return entry;

            throw new InvalidOperationException(string.Format("No entry found for annual salary ${0}", annualSalary));
        }

        public TaxEntry GetEntry(int min, int max)
        {
            var entry = _entries.FirstOrDefault(o => o.Min == min && o.Max == max);

            if (entry != null)
                return entry;
            
            throw new InvalidOperationException(string.Format("No entry found for ${0}-${1}", min, max));
        }

        public int GetIncomeTax(int annualSalary)
        {
            var taxEntry = GetEntry(annualSalary);
            double plus = 0;

            if (taxEntry.PlusInfo != null)
            {
                plus = (annualSalary - taxEntry.PlusInfo.PlusOver)*taxEntry.PlusInfo.PlusValue;
            }

            return (int)Math.Round((taxEntry.Value + plus)/12);
        }
    }
}
