namespace MYOB.MonthlyPayslip.TaxTable
{
    public class TaxEntry
    {
        public int Min { get; private set; }
        public int Max { get; private set; }
        public int Value { get; private set; }
        public PlusInfo PlusInfo { get; private set; }

        public TaxEntry(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public TaxEntry(int min, int max, int value)
        {
            Min = min;
            Max = max;
            Value = value;
        }
        
        public TaxEntry(int min, int max, int value, PlusInfo plusInfo)
        {
            Min = min;
            Max = max;
            Value = value;
            PlusInfo = plusInfo;
        }

        public override int GetHashCode()
        {
            return Max.GetHashCode() ^ 11 + Min.GetHashCode() ^ 11;
        }
    }
}