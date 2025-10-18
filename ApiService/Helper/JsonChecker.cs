using ApiService.Config;
using System.Text;
using System.Text.Json;

namespace ApiService.Helper
{
    public static class JsonChecker
    {
        public static List<string> GetDuplicateJsonKeys(string json)
        {
            var duplicates = new List<string>();

            try
            {
                var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json), JsonOpt.ReadObjOptions);
                var pathStack = new Stack<string>();
                var keyStack = new Stack<HashSet<string>>();
                keyStack.Push(new HashSet<string>(StringComparer.OrdinalIgnoreCase));

                while (reader.Read())
                {
                    ProcessToken(reader, pathStack, keyStack, duplicates);
                }
            }
            catch (JsonException)
            {
                // Ignore invalid JSON
            }

            return duplicates;
        }

        private static void ProcessToken(
            Utf8JsonReader reader,
            Stack<string> pathStack,
            Stack<HashSet<string>> keyStack,
            List<string> duplicates)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    StartObject(keyStack);
                    break;

                case JsonTokenType.EndObject:
                    EndObject(pathStack, keyStack);
                    break;

                case JsonTokenType.StartArray:
                    pathStack.Push("[]");
                    break;

                case JsonTokenType.EndArray:
                    if (pathStack.Count > 0) pathStack.Pop();
                    break;

                case JsonTokenType.PropertyName:
                    HandlePropertyName(reader, pathStack, keyStack, duplicates);
                    break;

                default:
                    HandleValue(pathStack);
                    break;
            }
        }

        private static void StartObject(Stack<HashSet<string>> keyStack)
        {
            keyStack.Push(new HashSet<string>(StringComparer.OrdinalIgnoreCase));
        }

        private static void EndObject(Stack<string> pathStack, Stack<HashSet<string>> keyStack)
        {
            keyStack.Pop();
            if (pathStack.Count > 0) pathStack.Pop();
        }

        private static void HandlePropertyName(
            Utf8JsonReader reader,
            Stack<string> pathStack,
            Stack<HashSet<string>> keyStack,
            List<string> duplicates)
        {
            string key = reader.GetString()!;
            string fullPath = pathStack.Count > 0 ? string.Join(".", pathStack) + "." + key : key;

            if (!keyStack.Peek().Add(key))
            {
                duplicates.Add(fullPath);
            }

            pathStack.Push(key);
        }

        private static void HandleValue(Stack<string> pathStack)
        {
            if (pathStack.Count > 0) pathStack.Pop();
        }

    }
}
