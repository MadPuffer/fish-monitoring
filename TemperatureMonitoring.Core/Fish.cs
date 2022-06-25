namespace TemperatureMonitoring.Core
{
    public class Fish
    {
        public int MaxTemp { get; private protected set; }
        public int MaxTempDuration { get; private protected set; }
        public int MinTemp { get; private protected set; }
        public int MinTempDuration { get; private protected set; }
        public string Name { get; private protected set; }
        public List<int> Temps { get; private protected set; }
        public DateTime TransportingStartTime { get; private protected set; }

        public Fish(int maxTemp, int maxTempDuration, int minTemp, int minTempDuration,
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

        public bool IsFishFresh()
        {
            return true;
        }
    }
}