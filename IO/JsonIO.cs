using DataStructures_test.Constants;
using System.Text.Json;

namespace DataStructures_test.Services
{
    internal static class JsonIO
    {
        public static T? LoadJsonFile<T>(string path)
        {
            string text = File.ReadAllText(ConstantsClass.BASE_JSON_FOLDER_URL + path);
            return JsonSerializer.Deserialize<T>(text);
        }

        public static void RightToJsonFile<T>(string path, T[] arr) 
        {
            string json = JsonSerializer.Serialize(arr);
            File.WriteAllText(ConstantsClass.BASE_JSON_FOLDER_URL + path, json);
        }
    }
}
