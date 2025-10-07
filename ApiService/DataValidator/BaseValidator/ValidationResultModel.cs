namespace ApiService.DataValidator.BaseValidator
{
    public class ValidationResultModel
    {
        private readonly Dictionary<string, List<string>> _errors = new();

        public bool IsValid => _errors.Count == 0;

        public void AddError(string field, string message)
        {
            if (!_errors.ContainsKey(field)) _errors[field] = new List<string>();

            _errors[field].Add(message);
        }

        // Produce your final format
        public object ToResponse()
        {
            // Flatten single errors into string, keep lists for multiple
            var modelState = _errors.ToDictionary(kv => kv.Key, kv => (object)kv.Value);
            
            return new { message = "Validation failed", modelState };
        }
    }
}