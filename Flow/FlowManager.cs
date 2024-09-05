using DataStructures_test.Constants;
using DataStructures_test.DataStructures;
using DataStructures_test.Helpers;
using DataStructures_test.Model;
using DataStructures_test.Services;

namespace DataStructures_test.Flow
{
    internal class FlowManager
    {
        public static BSTree<Defense> GetFullDefenseTree(List<Defense> defenses)
        {
            BSTree<Defense> DefenseTree = new();
            foreach (var d in defenses)
            {
                DefenseTree.Insert(d);
            }
            return DefenseTree;
        }
        public static async Task StartAttack(List<Threat> Threats, BSTree<Defense> DefenseTree)
        {
            int? MinSeverity = DefenseTree.GetMinValue()?.MinSeverity;
            Console.WriteLine("\n------------start attack------------");
            foreach (var t in Threats)
            {
                await Task.Delay(1000);
                int severity = HelperClass.GetThreatSeverity(t);
                Console.WriteLine($"{t} (severity: {severity})");

                if (MinSeverity != null && severity < MinSeverity)
                {
                    Console.WriteLine("Attack severity is below the threshold. Attack is ignored");
                    continue;
                }

                Defense? found = DefenseTree.SearchDefenseByThreatSeverity(severity);
                if (found == null) Console.WriteLine("No suitable defense was found. Brace for impact!");
                else
                {
                    for (int i = 0; i < found.Defenses.Count; i++)
                    {
                        Console.WriteLine($"defense {i + 1}: " + found.Defenses[i]);
                        await Task.Delay(2000);
                    }
                }
                Console.WriteLine();
            }
        }
        public static async Task Run()
        {
            Console.WriteLine("1. load defense json file and init tree\n");
            List<Defense>? defenses = JsonIO.LoadJsonFile<List<Defense>>(ConstantsClass.DEFENSE_URL);
            if (defenses == null)
            {
                Console.WriteLine("defense json file not found");
                return;
            }
            BSTree<Defense> DefenseTree = GetFullDefenseTree(defenses);
            await SetDelay();

            Console.WriteLine("2. print tree: (no balanced)\n");
            DefenseTree.PrintPreOrder();
            await SetDelay();

            Console.WriteLine("3. balance the tree and print it\n");
            DefenseTree.BalanceTree();
            DefenseTree.PrintPreOrder();
            await SetDelay();

            Console.WriteLine("4. print in order\n");
            DefenseTree.PrintInOrder();
            await SetDelay();

            Console.WriteLine("5. write the fix balanced tree to json file\n");
            JsonIO.RightToJsonFile("newJsonFile.json", DefenseTree.PreOrder.ToArray());
            await SetDelay();

            Console.WriteLine("6. load Treat json file and init tree\n");
            List<Threat>? Threats = JsonIO.LoadJsonFile<List<Threat>>(ConstantsClass.THREAT_URL);
            if (Threats == null)
            {
                Console.WriteLine("Threats json file not found");
                return;
            }
            await StartAttack(Threats, DefenseTree);
        }

        private static async Task SetDelay()
        {
            await Task.Delay(4000);
        }
    }
}
