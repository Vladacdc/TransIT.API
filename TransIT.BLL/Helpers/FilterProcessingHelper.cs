using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TransIT.BLL.Helpers
{
    public static class FilterProcessingHelper
    {
        public static IQueryable<TEntity> Where<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue,
            string binaryOperator)
        {
            switch (binaryOperator)
            {
                case "==":
                    return source.WhereEqual(leftProperty, constantValue);
                case "!=":
                    return source.WhereNotEqual(leftProperty, constantValue);
                case ">":
                    return source.WhereGreater(leftProperty, constantValue);
                case "<":
                    return source.WhereLess(leftProperty, constantValue);
                case ">=":
                    return source.WhereGreaterOrEqual(leftProperty, constantValue);
                case "<=":
                    return source.WhereLessOrEqual(leftProperty, constantValue);
                default:
                    return source;
            }
        }

        public static object DetectStringType(string stringValue, string entityType)
        {
            object result;
            if (stringValue == "null")
            {
                result = null;
            }
            else if (DateTime.TryParse(stringValue, out var date))
            {
                result = date;
            }
            else if (int.TryParse(stringValue, out var num1))
            {
                result = num1;
            }
            else if (bool.TryParse(stringValue, out var boolean))
            {
                result = boolean;
            }
            else
            {
                result = stringValue;
            }
            return result;
        }

        public static IQueryable<TEntity> WhereGreater<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue) =>
            ApplyOperatorInLambda(source, leftProperty, constantValue, Expression.GreaterThan);

        public static IQueryable<TEntity> WhereLess<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue) =>
            ApplyOperatorInLambda(source, leftProperty, constantValue, Expression.LessThan);

        public static IQueryable<TEntity> WhereGreaterOrEqual<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue) =>
            ApplyOperatorInLambda(source, leftProperty, constantValue, Expression.GreaterThanOrEqual);

        public static IQueryable<TEntity> WhereLessOrEqual<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue) =>
            ApplyOperatorInLambda(source, leftProperty, constantValue, Expression.LessThanOrEqual);

        public static IQueryable<TEntity> WhereEqual<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue) =>
            ApplyOperatorInLambda(source, leftProperty, constantValue, Expression.Equal);

        public static IQueryable<TEntity> WhereNotEqual<TEntity>(
            this IQueryable<TEntity> source,
            string leftProperty,
            object constantValue) =>
            ApplyOperatorInLambda(source, leftProperty, constantValue, Expression.NotEqual);

        public static IQueryable<TEntity> ApplyOperatorInLambda<TEntity>(
            IQueryable<TEntity> source,
            string leftProperty,
            object constantValue,
            Func<Expression, Expression, Expression> action)
        {
            var parameter = Expression.Parameter(source.ElementType, "p");
            var firstPropertyAccess = GetAccessProperty(
                parameter,
                CapitalizeSentence(leftProperty)
            );
            var secondPropertyAccess = (Expression)
                Expression.Constant(
                    constantValue,
                    constantValue.GetType()
                    );
            secondPropertyAccess = Expression.Convert(secondPropertyAccess, firstPropertyAccess.Type);

            return source.Provider.CreateQuery<TEntity>(
                BuildWhereExpression(
                    source,
                    Expression.Lambda(
                        action(firstPropertyAccess, secondPropertyAccess),
                        parameter
                        )
                    )
                );
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc) =>
            source.Provider.CreateQuery<TEntity>(
                BuildOrderByExpression(
                    source,
                    CapitalizeSentence(orderByProperty),
                    desc ? "OrderByDescending" : "OrderBy"
                    )
                );

        public static IQueryable<TEntity> ThenBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc) =>
            source.Provider.CreateQuery<TEntity>(
                BuildOrderByExpression(
                    source,
                    CapitalizeSentence(orderByProperty),
                    desc ? "ThenByDescending" : "ThenBy"
                    )
                );

        private static MethodCallExpression BuildWhereExpression<TEntity>(IQueryable<TEntity> source, Expression lambda) =>
            Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { source.ElementType },
                source.Expression,
                Expression.Quote(lambda)
                );

        private static MethodCallExpression BuildOrderByExpression<TEntity>(
            IQueryable<TEntity> source,
            string[] propertyPath,
            string method)
        {
            var parameter = Expression.Parameter(source.ElementType, "p");
            var propertyAccess = GetAccessProperty(parameter, propertyPath);
            var propertyType = GetPropertyByPath(
                source.ElementType.GetProperty(propertyPath.First()),
                propertyPath.Skip(1)
                ).PropertyType;

            return Expression.Call(
                typeof(Queryable),
                method,
                new[] { source.ElementType, propertyType },
                source.Expression,
                Expression.Quote(
                    Expression.Lambda(propertyAccess, parameter)
                )
            );
        }

        public static Expression GetAccessProperty(Expression propertyAccess, IEnumerable<string> propertyPath) =>
            ChangeAndReturn(
                propertyAccess,
                propertyPath,
                (name, prop) => Expression.PropertyOrField(prop, name)
                );

        public static PropertyInfo GetPropertyByPath(PropertyInfo property, IEnumerable<string> propertyPath) =>
            ChangeAndReturn(
                property,
                propertyPath,
                (name, prop) => prop.PropertyType.GetProperty(name)
                );

        private static T ChangeAndReturn<T>(T property, IEnumerable<string> propertyPath, Func<string, T, T> changer)
        {
            propertyPath.ToList()
                .ForEach(name =>
                    property = changer(name, property)
                    );
            return property;
        }

        public static string[] CapitalizeSentence(string str) =>
            str.Split('.')
                .Select(Capitalize)
                .ToArray();

        private static string Capitalize(string str) =>
            $"{str.First().ToString().ToUpper()}{str.Substring(1)}";
    }
}
