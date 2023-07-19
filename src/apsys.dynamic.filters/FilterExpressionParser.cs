using apsys.dynamic.filters.extenders;
using System.Linq.Expressions;
using System.Reflection;

namespace apsys.dynamic.filters
{
    public class FilterExpressionParser
    {
        static public Expression<Func<T, bool>> ParsePredicate<T>(IEnumerable<FilterOperator> operands)
        {
            var parameterExpression = Expression.Parameter(typeof(T), nameof(T).ToLower());

            IList<Expression> allCritera = new List<Expression>();
            foreach (FilterOperator filter in operands)
            {
                string propertyName = filter.FieldName.ToPascalCase();
                Expression propertyExpression = Expression.Property(parameterExpression, propertyName);
                IList<string> filterValues = filter.Values
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Select(v => v.Trim())
                    .ToList();

                Expression criteria = null;
                switch (filter.RelationalOperatorType)
                {
                    case RelationalOperator.Contains:
                        propertyExpression = CallToStringMethod<T>(propertyExpression, propertyName);
                        var constant = Expression.Constant(filterValues.FirstOrDefault());
                        var strContainsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                        criteria = Expression.Call(propertyExpression, strContainsMethod, new Expression[] { constant });
                        break;
                    case RelationalOperator.GreaterThan:
                        var constantExpression01 = CreateConstantExpression<T>(propertyName, filterValues.FirstOrDefault());
                        criteria = Expression.GreaterThan(propertyExpression, constantExpression01);
                        break;
                    case RelationalOperator.GreaterThanOrEqual:
                        var constantExpression02 = CreateConstantExpression<T>(propertyName, filterValues.FirstOrDefault());
                        criteria = Expression.GreaterThanOrEqual(propertyExpression, constantExpression02);
                        break;
                    case RelationalOperator.LessThan:
                        var constantExpression03 = CreateConstantExpression<T>(propertyName, filterValues.FirstOrDefault());
                        criteria = Expression.LessThan(propertyExpression, constantExpression03);
                        break;
                    case RelationalOperator.LessThanOrEqual:
                        var constantExpression04 = CreateConstantExpression<T>(propertyName, filterValues.FirstOrDefault());
                        criteria = Expression.LessThanOrEqual(propertyExpression, constantExpression04);
                        break;
                    case RelationalOperator.Between:
                        var lowerLimitExpression = CreateConstantExpression<T>(propertyName, filterValues.FirstOrDefault());
                        var upperLimitExpression = CreateConstantExpression<T>(propertyName, filterValues.LastOrDefault());
                        Expression lowerLimitCriteria = Expression.GreaterThanOrEqual(propertyExpression, lowerLimitExpression);
                        Expression upperLimitCriteria = Expression.LessThanOrEqual(propertyExpression, upperLimitExpression);
                        criteria = Expression.AndAlso(lowerLimitCriteria, upperLimitCriteria);
                        break;
                    //case RelationalOperator.Equal:
                    default:
                        propertyExpression = CallToStringMethod<T>(propertyExpression, propertyName);
                        var arrContainsMethod = filterValues.GetType().GetMethod(nameof(filterValues.Contains), new Type[] { typeof(string) });
                        criteria = Expression.Call(Expression.Constant(filterValues), arrContainsMethod, propertyExpression);
                        break;
                }
                allCritera.Add(criteria);
            }

            Expression expression = null;
            if (!allCritera.Any())
            {
                expression = Expression.Constant(true);
            }
            else
            {
                foreach (Expression criteria in allCritera)
                    expression = expression != null ? Expression.AndAlso(expression, criteria) : criteria;
            }

            var lambda = Expression.Lambda<Func<T, bool>>(expression, parameterExpression);

            return lambda;
        }

        private static Expression CallToStringMethod<T>(Expression propertyExpression, string propertyName)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            if (!propertyInfo.PropertyType.Equals(typeof(string)))
                return Expression.Call(propertyExpression, "ToString", Type.EmptyTypes);

            return propertyExpression;
        }

        private static Expression CreateConstantExpression<T>(string propertyName, string constatValue)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo.PropertyType.Equals(typeof(string)))
                return Expression.Constant(constatValue);

            object convertedValue = propertyInfo.PropertyType.IsEnum
                ? Enum.Parse(propertyInfo.PropertyType, constatValue)
                : Convert.ChangeType(constatValue, propertyInfo.PropertyType);

            return Expression.Constant(convertedValue);
        }
    }
}
