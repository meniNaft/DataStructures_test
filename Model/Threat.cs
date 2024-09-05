namespace DataStructures_test.Model
{
    internal class Threat
    {
        public string ThreatType { get; set; }
        public int Volume { get; set; }
        public int Sophistication { get; set; }
        public string Target { get; set; }

        public override string ToString()
        {
            return $"threat Details: ThreatType: {ThreatType}, For Target: {Target}";
        }
    }
}
