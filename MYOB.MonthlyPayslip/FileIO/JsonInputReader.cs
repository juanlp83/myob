using System.Collections.Generic;
using System.IO;
using MYOB.MonthlyPayslip.Entities;
using Newtonsoft.Json;

namespace MYOB.MonthlyPayslip.FileIO
{
    public class JsonInputReader
    {
        public IEnumerable<Employee> ReadEmployeeData(string filename)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Employee>>(File.ReadAllText(filename));
        }
    }
}
