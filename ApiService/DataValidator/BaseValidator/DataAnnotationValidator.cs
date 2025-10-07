using System.ComponentModel.DataAnnotations;

namespace ApiService.DataValidator.BaseValidator
{
    public static class DataAnnotationValidator
    {
        public static ValidationResultModel Validate<T>(T? model)
        {
            var result = new ValidationResultModel();

            if (model == null)
            {
                result.AddError("Body", "Request body is empty or invalid JSON.");
                return result;
            }

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, validateAllProperties: true);

            foreach (var error in validationResults)
            {
                // if multiple member names are attached, map them all
                var field = error.MemberNames.FirstOrDefault() ?? "Model";
                result.AddError(field, error.ErrorMessage ?? "Invalid value.");
            }

            return result;
        }
    }
}
