using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

public class ExpressionBuilder
{
    // Define some of our default filtering options
    private static MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
    private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
    private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

    public static Expression<Func<T, bool>> GetExpression<T>(string propiedad, ConstantExpression filtro, string operador)//(List<GridHelper.Filter> filters)
    {
        // No filters passed in #KickIT
        if (filtro == null)//   if (filters.Count == 0)
            return null;

        // Create the parameter for the ObjectType (typically the 'x' in your expression (x => 'x')
        // The "parm" string is used strictly for debugging purposes
        ParameterExpression param = Expression.Parameter(typeof(T), "parm");

        // Store the result of a calculated Expression
        Expression exp = null;

        //if (filters.Count == 1)
        exp = GetExpression<T>(param, propiedad, filtro, operador); // Create expression from a single instance
        //else if (filters.Count == 2)
        //    exp = GetExpression<T>(param, filters[0], filters[1]); // Create expression that utilizes AndAlso mentality
        //else
        //{
        //    // Loop through filters until we have created an expression for each
        //    while (filters.Count > 0)
        //    {
        //        // Grab initial filters remaining in our List
        //        var f1 = filters[0];
        //        var f2 = filters[1];

        //        // Check if we have already set our Expression
        //        if (exp == null)
        //            exp = GetExpression<T>(param, filters[0], filters[1]); // First iteration through our filters
        //        else
        //            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1])); // Add to our existing expression

        //        filters.Remove(f1);
        //        filters.Remove(f2);

        //        // Odd number, handle this seperately
        //        if (filters.Count == 1)
        //        {
        //            // Pass in our existing expression and our newly created expression from our last remaining filter
        //            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
        //            // Remove filter to break out of while loop
        //            filters.RemoveAt(0);
        //        }
        //    }
        //}

        return Expression.Lambda<Func<T, bool>>(exp, param);
    }

    private static Expression GetExpression<T>(ParameterExpression param, string propiedad, ConstantExpression filtro, string operador)// GridHelper.Filter filter)
    {
        // The member you want to evaluate (x => x.FirstName)
        MemberExpression member = Expression.Property(param, propiedad);// filter.PropertyName);

        // The value you want to evaluate
        ConstantExpression constant = filtro;//filter.Value);


        // Determine how we want to apply the expression
        switch (operador)//filter.Operator)
        {
            case "="://GridHelper.Operator.Equals:
                return Expression.Equal(member, constant);
            case "LIKE"://GridHelper.Operator.Contains:
                return Expression.Call(member, containsMethod, constant);
            case ">"://GridHelper.Operator.GreaterThan:
                return Expression.GreaterThan(member, constant);
            case ">="://GridHelper.Operator.GreaterThanOrEqual:
                return Expression.GreaterThanOrEqual(member, constant);
            case "<"://GridHelper.Operator.LessThan:
                return Expression.LessThan(member, constant);

            case "<="://GridHelper.Operator.LessThanOrEqualTo:
                return Expression.LessThanOrEqual(member, constant);

            case "<>"://GridHelper.Operator.LessThanOrEqualTo:
                return Expression.NotEqual(member, constant);

            //case GridHelper.Operator.StartsWith:
            //    return Expression.Call(member, startsWithMethod, constant);

            //case GridHelper.Operator.EndsWith:
            //    return Expression.Call(member, endsWithMethod, constant);
        }

        return null;
    }

    //private static BinaryExpression GetExpression<T>(ParameterExpression param, GridHelper.Filter filter1, GridHelper.Filter filter2)
    //{
    //    Expression result1 = GetExpression<T>(param, filter1);
    //    Expression result2 = GetExpression<T>(param, filter2);
    //    return Expression.AndAlso(result1, result2);
    //}

}