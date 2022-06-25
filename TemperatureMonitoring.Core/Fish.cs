using System;

namespace TemperatureMonitoring.Core
{
    public class Fish
    {
        public int MaxTemp { get; private protected set; }
        public int MaxTempDuration { get; private protected set; }
        public int? MinTemp { get; private protected set; }
        public int? MinTempDuration { get; private protected set; }
        public string Name { get; private protected set; }
        public List<int>? Temps { get; private protected set; }
        public DateTime TransportingStartTime { get; private protected set; }

        public Fish(int maxTemp, int maxTempDuration, int? minTemp, int? minTempDuration,
            string name, List<int> temps, DateTime transportingStartTime)
        {
            MaxTemp = maxTemp;
            MaxTempDuration = maxTempDuration;
            MinTemp = minTemp;
            MinTempDuration = minTempDuration;
            Name = name;
            Temps = temps;
            TransportingStartTime = transportingStartTime;
        }

        public Fish(int maxTemp, int maxTempDuration, int? minTemp, int? minTempDuration,
            string name)
        {
            MaxTemp = maxTemp;
            MaxTempDuration = maxTempDuration;
            MinTemp = minTemp;
            MinTempDuration = minTempDuration;
            Name = name;
        }

        public FishConditionData CheckFishStatus()
        {
            List<ViolationData> violations = new List<ViolationData>();
            int minTempViolation = 0;
            int maxTempViolation = 0;
            
            for (int i = 0; i < Temps.Count; ++i)
            {
                if (Temps[i] > MaxTemp)
                {
                    maxTempViolation += 10;
                    violations.Add(new ViolationData(TransportingStartTime.AddMinutes(10 * i), MaxTemp, Temps[i]));
                }
                else if (MinTemp != null && MinTemp > Temps[i])
                {
                    minTempViolation += 10;
                    violations.Add(new ViolationData(TransportingStartTime.AddMinutes(10 * i), (int) MinTemp, Temps[i]));
                }
            }

            return new FishConditionData(violations, minTempViolation, maxTempViolation);
        }

        public void LoadData(FishTransportingData data)
        {
            Temps = data.temps;
            TransportingStartTime = data.date;
        }
    }

    public struct FishTransportingData
    {
        public List<int> temps { get; set; }
        public DateTime date { get; set; }

    }

    public struct ViolationData
    {
        public DateTime Date { get; set; }
        public int RequiredTemp { get; set; }
        public int FixedTemp { get; set; }

        public ViolationData(DateTime date, int requiredTemp, int fixedTemp)
        {
            Date = date;
            RequiredTemp = requiredTemp;
            FixedTemp = fixedTemp;
        }
    }

    public struct FishConditionData
    {
        List<ViolationData> Violations { get; set; }
        int MinTempViolationsDuration { get; set; }
        int MaxTempViolationsDuration { get; set; }

        public FishConditionData(List<ViolationData> violations, int minTempViolations, int maxTempViolations)
        {
            Violations = violations;
            MinTempViolationsDuration = minTempViolations;
            MaxTempViolationsDuration = maxTempViolations;
        }
    }

}