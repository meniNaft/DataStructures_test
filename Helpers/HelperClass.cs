using DataStructures_test.Constants;
using DataStructures_test.Enums;
using DataStructures_test.Model;

namespace DataStructures_test.Helpers
{
    internal static class HelperClass
    {
        public static int GetThreatSeverity(Threat threat)
        {
            bool success = Enum.TryParse(threat.Target,true , out TargetEnum parsedValue);
            int targetValue = success ? (int)parsedValue : ConstantsClass.DEFAULT_TARGET_VALUE;
            return (threat.Volume * threat.Sophistication) + targetValue;
        }
    }
}
