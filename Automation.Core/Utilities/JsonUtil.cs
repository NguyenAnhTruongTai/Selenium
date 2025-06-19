using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.Test.Core.Utilities
{
    public static class JsonUtil
    {
        public static IEnumerable<IDictionary<string, string>> GetJsonDataArray(string subFolder, string fileName)
        {
            string fullPath = Path.Combine(AppContext.BaseDirectory, "TestData", subFolder, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            var json = File.ReadAllText(fullPath);
            var jsonObject = JsonConvert.DeserializeObject<JObject>(json);

            if (jsonObject == null)
                throw new InvalidOperationException("Failed to deserialize JSON into JObject.");

            var propertyName = jsonObject.Properties().First().Name;

            var token = jsonObject[propertyName];
            if (token == null)
                throw new InvalidOperationException($"Property '{propertyName}' not found in JSON object.");

            var dataArray = token.ToObject<List<Dictionary<string, string>>>();

            return dataArray ?? new List<Dictionary<string, string>>();
        }

        public static List<Dictionary<string, string>> GetJsonData(string subFolder, string fileName, string caseKey)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectRoot, "TestData", subFolder, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("JSON file not found", fullPath);

            var jsonString = File.ReadAllText(fullPath);

            var allCases = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(jsonString);

            if (allCases != null && allCases.ContainsKey(caseKey))
                return allCases[caseKey];

            throw new KeyNotFoundException($"Cannot find key '{caseKey}' in JSON file.");
        }

    }
}