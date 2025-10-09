using DataTransferObject.GlobalObject;

namespace ApiService.DataValidator
{
    public class ValidationResultModel
    {
        private readonly Dictionary<string, List<string>> _errors = [];
        public bool IsValid => _errors.Count == 0;

        public void AddError(string field, string message)
        {
            if (!_errors.ContainsKey(field)) _errors[field] = [];
            _errors[field].Add(message);
        }

        public ApiResponseDefault<object> ToResponse()
        {
            // Convert List<string> → string[]
            var errors = _errors.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());
            return ApiResponseDefault<object>.Fail("Validation failed", errors);
        }
    }
}