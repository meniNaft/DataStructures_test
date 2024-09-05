using DataStructures_test.Constants;
using DataStructures_test.DataStructures;
using DataStructures_test.Helpers;
using DataStructures_test.Model;
using DataStructures_test.Services;
using System.Text.Json;

namespace DataStructures_test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<Defense>? defenses = JsonIO.LoadJsonFile<List<Defense>>(ConstantsClass.DEFENSE_URL);
            List<Threat>? Threats = JsonIO.LoadJsonFile<List<Threat>>(ConstantsClass.THREAT_URL);
            if (defenses == null || Threats == null)
            {
                Console.WriteLine("json files not found");
                return;
            }

            BSTree<Defense> DefenseTree = GetFullDefenseTree(defenses);
            Console.WriteLine("--------print no balance tree--------");
            DefenseTree.PrintPreOrder();

            DefenseTree.BalanceTree();
            Console.WriteLine("\n--------print balance tree--------");
            DefenseTree.PrintPreOrder();

            Console.WriteLine("\n--------print in order--------");
            DefenseTree.PrintInOrder();

            JsonIO.RightToJsonFile("newJsonFile.json", DefenseTree.PreOrder.ToArray());

            await StartAttack(Threats, DefenseTree);
           Console.WriteLine();
        }

        static BSTree<Defense> GetFullDefenseTree(List<Defense> defenses)
        {
            BSTree<Defense> DefenseTree = new();
            foreach (var d in defenses)
            {
                DefenseTree.Insert(d);
            }
            return DefenseTree;
        }
        static async Task StartAttack(List<Threat> Threats, BSTree<Defense> DefenseTree)
        {
            int? MinSeverity = DefenseTree.GetMinValue()?.MinSeverity;
            Console.WriteLine("\n------------start attack------------");
            foreach (var t in Threats)
            {
                Console.WriteLine(t);
                await Task.Delay(1000);
                int severity = HelperClass.GetThreatSeverity(t);
                
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
        
        static Task FlowManager()
        {

        }
    }
}
