using System.Text;
using System.Text.Json;

namespace DataTransferObject.GlobalObject
{
    public static class JsonDuplicateKeyValidator
    {
        public static List<string> GetDuplicateKeys(string json)
        {
            var duplicates = new List<string>();

            try
            {
                var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
                var pathStack = new Stack<string>(); // Tracks current path
                var keyStack = new Stack<HashSet<string>>(); // Tracks keys per object
                keyStack.Push(new HashSet<string>(StringComparer.OrdinalIgnoreCase));

                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.StartObject:
                            keyStack.Push(new HashSet<string>(StringComparer.OrdinalIgnoreCase));
                            break;

                        case JsonTokenType.EndObject:
                            keyStack.Pop();
                            if (pathStack.Count > 0) pathStack.Pop();
                            break;

                        case JsonTokenType.StartArray:
                            pathStack.Push("[]"); // placeholder for array
                            break;

                        case JsonTokenType.EndArray:
                            pathStack.Pop();
                            break;

                        case JsonTokenType.PropertyName:
                            string? key = reader.GetString();

                            // Compute full JSON path
                            string fullPath = pathStack.Count > 0
                                ? string.Join(".", pathStack) + "." + key
                                : key!;

                            var currentKeys = keyStack.Peek();
                            if (!currentKeys.Add(key!)) duplicates.Add(fullPath);

                            pathStack.Push(key!); // push current property for nested objects/arrays
                            break;

                        case JsonTokenType.String:
                        case JsonTokenType.Number:
                        case JsonTokenType.True:
                        case JsonTokenType.False:
                        case JsonTokenType.Null:
                            if (pathStack.Count > 0) pathStack.Pop(); // value consumed, pop property
                            break;
                    }
                }
            }
            catch (JsonException)
            {
                // Let upper filter handle invalid JSON
            }

            return duplicates;
        }

    }
}
