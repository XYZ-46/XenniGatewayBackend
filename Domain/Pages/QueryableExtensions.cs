using System.Linq.Expressions;

namespace Domain.Pages
{

    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, List<SearchFilter>? filters)
        {
            if (filters == null || filters.Count == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            foreach (var filter in filters)
            {
                var property = typeof(T).GetProperty(filter.Field,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (property == null) continue;

                var propertyAccess = Expression.Property(parameter, property);

                Expression? expr = BuildExpression(propertyAccess, property.PropertyType, filter);
                if (expr == null) continue;

                combined = combined == null ? expr : Expression.AndAlso(combined, expr);
            }

            if (combined == null) return query;

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            return query.Where(lambda);
        }

        private static Expression? BuildExpression(MemberExpression propertyAccess, Type propertyType, SearchFilter filter)
        {
            object? convertedValue;
            try
            {
                convertedValue = Convert.ChangeType(filter.Value, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
            }
            catch
            {
                return null;
            }

            var constant = Expression.Constant(convertedValue, propertyType);

            switch (filter.Operator)
            {
                case FilterOperator.Equal:
                    return Expression.Equal(propertyAccess, constant);
                case FilterOperator.NotEqual:
                    return Expression.NotEqual(propertyAccess, constant);

                case FilterOperator.Contain:
                    return Expression.Call(propertyAccess, typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!, constant);
                case FilterOperator.NotContain:
                    return Expression.Not(Expression.Call(propertyAccess, typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!, constant));

                case FilterOperator.StartWith:
                    return Expression.Call(propertyAccess, typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!, constant);
                case FilterOperator.NotStartWith:
                    return Expression.Not(Expression.Call(propertyAccess, typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!, constant));

                case FilterOperator.EndWith:
                    return Expression.Call(propertyAccess, typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) })!, constant);
                case FilterOperator.NotEndWith:
                    return Expression.Not(Expression.Call(propertyAccess, typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) })!, constant));

                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(propertyAccess, constant);
                case FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(propertyAccess, constant);
                case FilterOperator.LessThan:
                    return Expression.LessThan(propertyAccess, constant);
                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(propertyAccess, constant);

                case FilterOperator.Between:
                    if (string.IsNullOrWhiteSpace(filter.Value2)) return null;
                    object? convertedValue2;
                    try
                    {
                        convertedValue2 = Convert.ChangeType(filter.Value2, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    }
                    catch
                    {
                        return null;
                    }
                    var constant2 = Expression.Constant(convertedValue2, propertyType);
                    var lowerBound = Expression.GreaterThanOrEqual(propertyAccess, constant);
                    var upperBound = Expression.LessThanOrEqual(propertyAccess, constant2);
                    return Expression.AndAlso(lowerBound, upperBound);

                default:
                    return null;
            }
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, List<SortField<T>>? sorts)
        {
            if (sorts == null || sorts.Count == 0)
                return query;

            IOrderedQueryable<T>? orderedQuery = null;

            for (int i = 0; i < sorts.Count; i++)
            {
                var sort = sorts[i];

                if (i == 0)
                {
                    orderedQuery = sort.Descending
                        ? query.OrderByDescending(sort.Field)
                        : query.OrderBy(sort.Field);
                }
                else
                {
                    orderedQuery = sort.Descending
                        ? orderedQuery.ThenByDescending(sort.Field)
                        : orderedQuery.ThenBy(sort.Field);
                }
            }

            return orderedQuery ?? query;
        }


        public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
            this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await Task.Run(() => query.Count(), cancellationToken);
            var items = await Task.Run(() => query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList(), cancellationToken);

            return new PaginatedResult<T>(items, totalCount, pageIndex, pageSize);
        }
    }

}
