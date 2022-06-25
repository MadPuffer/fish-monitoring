using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureMonitoring.Core
{
    public class FileParser : IFishDataParser
    {
        public static void ParseFishData(string? path)
        {
            List<int> temps = new List<int>();
            DateTime date;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    date = DateTime.Parse(sr.ReadLine()); 
                    foreach (string temp in sr.ReadLine().Split())
                    {
                        temps.Add(int.Parse(temp));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
