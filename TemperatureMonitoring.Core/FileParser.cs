using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureMonitoring.Core
{
    public class FileParser : IFishDataParser
    {
        public static FishData ParseFishData(string? path)
        {
            FishData fishData = new FishData();
            List<int> temps = new List<int>();
            DateTime date;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    date = DateTime.Parse(sr.ReadLine());
                    fishData.date = date;
                    foreach (string temp in sr.ReadLine().Split())
                    {
                        temps.Add(int.Parse(temp));
                    }
                    fishData.temps = temps;
                }
            }
            catch (Exception ex)
            {

            }
            return fishData;
        }
    }
}
