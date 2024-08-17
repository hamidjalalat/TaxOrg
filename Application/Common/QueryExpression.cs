using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{

    public class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        private static MethodInfo doesNotContainMethod = typeof(string).GetMethod("DoesNotContain", new Type[] { typeof(string) });
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        
        public static Expression<Func<T, bool>> GetExpression<T>(List<GridHelper.Filter> filters)
        {
            // No filters passed in #KickIT
            if (filters.Count == 0)
                return null;

            // Create the parameter for the ObjectType (typically the 'x' in your expression (x => 'x')
            // The "parm" string is used strictly for debugging purposes
            ParameterExpression param = Expression.Parameter(typeof(T), "parm");

            // Store the result of a calculated Expression
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]); // Create expression from a single instance
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]); // Create expression that utilizes AndAlso mentality
            else
            {
                // Loop through filters until we have created an expression for each
                while (filters.Count > 0)
                {
                    // Grab initial filters remaining in our List
                    var f1 = filters[0];
                    var f2 = filters[1];

                    // Check if we have already set our Expression
                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]); // First iteration through our filters
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1])); // Add to our existing expression

                    filters.Remove(f1);
                    filters.Remove(f2);

                    // Odd number, handle this seperately
                    if (filters.Count == 1)
                    {
                        // Pass in our existing expression and our newly created expression from our last remaining filter
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));

                        // Remove filter to break out of while loop
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, GridHelper.Filter filter)
        {
            //  MemberExpression member = Expression.Property(param, filter.PropertyName);

            //   ConstantExpression constant = Expression.Constant(filter.Value);

            var member = Expression.Property(param, filter.PropertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();

            //var propertyValue = converter.ConvertFromInvariantString(filter.Value);
            var propertyValue = converter.ConvertFrom(filter.Value);
            var constant = Expression.Constant(propertyValue);
            var valueExpression = Expression.Convert(constant, propertyType);

            switch (filter.Operator)
            {
                case GridHelper.Operator.Equals:
                    return Expression.Equal(member, valueExpression);

                case GridHelper.Operator.NotEqual:
                    return Expression.NotEqual(member, valueExpression);

                case GridHelper.Operator.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case GridHelper.Operator.DoesNotContain:
                    return Expression.Call(member, doesNotContainMethod, constant);

                case GridHelper.Operator.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case GridHelper.Operator.GreaterThanOrEquals:
                    return Expression.GreaterThanOrEqual(member, constant);

                case GridHelper.Operator.LessThan:
                    return Expression.LessThan(member, constant);

                case GridHelper.Operator.LessThanOrEquals:
                    return Expression.LessThanOrEqual(member, constant);

                case GridHelper.Operator.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case GridHelper.Operator.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);

                case GridHelper.Operator.IsNull:
                    return Expression.Equal(member, Expression.Constant(null, member.Type));

                case GridHelper.Operator.IsNotNull:
                    return Expression.NotEqual(member, Expression.Constant(null, member.Type));
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, GridHelper.Filter filter1, GridHelper.Filter filter2)
        {
            Expression result1 = GetExpression<T>(param, filter1);
            Expression result2 = GetExpression<T>(param, filter2);
            return Expression.AndAlso(result1, result2);
        }


    }

    public class GridHelper
    {
        public enum Operator
        {
            Equals,
            NotEqual,
            Contains,
            DoesNotContain,
            GreaterThan,
            GreaterThanOrEquals,
            LessThan,
            LessThanOrEquals,
            StartsWith,
            EndsWith,
            IsNull,
            IsNotNull,
        }

        public class Filter
        {
            public string PropertyName { get; set; }
            //public string Value { get; set; }
            public object Value { get; set; }
            private Operator _op = Operator.Contains;
            public Operator Operator
            {
                get
                {
                    return _op;
                }
                set
                {
                    _op = value;
                }
            }
        }

    }
}
