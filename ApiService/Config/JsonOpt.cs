using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiService.Config
{
    public static class JsonOpt
    {
        public static readonly JsonSerializerOptions WriteOptions = new()
        {
            // Relax string escaping (allow special chars like single quote '12')
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            PropertyNamingPolicy = null,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        public static readonly JsonSerializerOptions ReadOptions = new()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            AllowDuplicateProperties = false,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };
    }
}