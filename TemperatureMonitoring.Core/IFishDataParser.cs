﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureMonitoring.Core
{
    public interface IFishDataParser
    {
        public static FishData ParseFishData(string? path)
        {
            return new FishData();
        }
    }
}
