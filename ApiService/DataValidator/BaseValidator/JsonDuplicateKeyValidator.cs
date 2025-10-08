using System.Text;
using System.Text.Json;

namespace ApiService.DataValidator.BaseValidator
{
    public static class JsonDuplicateKeyValidator
    {
        public static List<string> GetDuplicateKeys(string json)
        {
            var duplicates = new List<string>();
            var bytes = Encoding.UTF8.GetBytes(json);

            var reader = new Utf8JsonReader(bytes, new JsonReaderOptions { AllowTrailingCommas = true });

            DetectDuplicates(ref reader, duplicates, "");
            return duplicates;
        }

        private static void DetectDuplicates(ref Utf8JsonReader reader, List<string> duplicates, string path)
        {
            if (!reader.Read()) return;

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject) return;

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var propName = reader.GetString()!;
                        string fullPath = string.IsNullOrEmpty(path) ? propName : $"{path}.{propName}";

                        if (!seen.Add(propName)) duplicates.Add(fullPath);

                        if (!reader.Read()) return; // move to value

                        switch (reader.TokenType)
                        {
                            case JsonTokenType.StartObject:
                                DetectDuplicates(ref reader, duplicates, fullPath);
                                break;
                            case JsonTokenType.StartArray:
                                DetectArrayDuplicates(ref reader, duplicates, fullPath);
                                break;
                            default:
                                // Skip scalar value
                                break;
                        }
                    }
                }
            }
        }

        private static void DetectArrayDuplicates(ref Utf8JsonReader reader, List<string> duplicates, string path)
        {
            int index = 0;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray) return;

                string arrayPath = $"{path}[{index}]";

                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    DetectDuplicates(ref reader, duplicates, arrayPath);
                }
                else if (reader.TokenType == JsonTokenType.StartArray)
                {
                    DetectArrayDuplicates(ref reader, duplicates, arrayPath);
                }
                else
                {
                    // Skip primitives
                }

                index++;
            }
        }

    }
}
