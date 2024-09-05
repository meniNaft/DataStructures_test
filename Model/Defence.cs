namespace DataStructures_test.Model
{
    internal class Defense: IComparable
    {
        public int MinSeverity { get; set; }
        public int MaxSeverity { get; set; }
        public List<string> Defenses { get; set; }

        public int CompareTo(object? other)
        {
            if (other is int v)
            {
                if (MinSeverity <= v && MaxSeverity >= v) return 0; 
                if (MinSeverity < v) return -1;
                return 1;
            }
            else if (other is Defense second) return MinSeverity.CompareTo(second?.MinSeverity);
            return 0;
        }

        public override string ToString()
        {
            return $"[{MinSeverity}-{MaxSeverity}] Defenses: {string.Join(", ", Defenses)}";
        }
    }
}
