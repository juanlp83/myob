using System.IO;
using Newtonsoft.Json;

namespace MYOB.MonthlyPayslip.FileIO
{
    public class JsonExporter
    {
        public void Export(string outputFilename, object objectToSerialize)
        {
            File.WriteAllText(outputFilename, JsonConvert.SerializeObject(objectToSerialize, Formatting.Indented));
        }
    }
}
