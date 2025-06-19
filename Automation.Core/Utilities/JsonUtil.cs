using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.Test.Core.Utilities
{
    public static class JsonUtil
    {

        public static T GetJsonScenarioData<T>(string fileName, string scenarioKey)
        {
            string fullPath = Path.Combine(AppContext.BaseDirectory, "TestData", fileName);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            var json = File.ReadAllText(fullPath);
            var jsonObject = JsonConvert.DeserializeObject<JObject>(json);

            if (!jsonObject.ContainsKey(scenarioKey))
                throw new ArgumentException($"Scenario '{scenarioKey}' not found in JSON file.");

            var token = jsonObject[scenarioKey];
            var result = token.ToObject<T>();

            return result!;
        }
    }
}