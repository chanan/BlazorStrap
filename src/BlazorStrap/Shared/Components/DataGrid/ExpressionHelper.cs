using System.Linq.Expressions;

namespace BlazorStrap.Shared.Components.DataGrid;

public static class ExpressionHelper
{
    public static string GetPropertyPath<TGridItem, TProperty>(Expression<Func<TGridItem, TProperty>> expression)
    {
        var memberExpression = expression.Body as MemberExpression;
        if (memberExpression == null)
        {
            throw new ArgumentException("Expression must be a member expression");
        }

        var propertyPath = new Stack<string>();
        while (memberExpression != null)
        {
            propertyPath.Push(memberExpression.Member.Name);
            memberExpression = memberExpression.Expression as MemberExpression;
        }

        return string.Join(".", propertyPath);
    }
    
    public static Type GetPropertyType<TGridItem>(string propertyPath)
    {
        var parameter = Expression.Parameter(typeof(TGridItem), "x");
        Expression property = parameter;

        foreach (var member in propertyPath.Split('.'))
        {
            property = Expression.Property(property, member);
        }

        return property.Type;
    }
    
    public static Expression<Func<TGridItem, object>> GetExpression<TGridItem>(string propertyPath)
    {
        var parameter = Expression.Parameter(typeof(TGridItem), "x");
        Expression property = parameter;

        foreach (var member in propertyPath.Split('.'))
        {
            property = Expression.Property(property, member);
        }

        // Create a lambda expression without converting the property to object
        var lambda = Expression.Lambda(property, parameter);
        return Expression.Lambda<Func<TGridItem, object>>(Expression.Convert(lambda.Body, typeof(object)), lambda.Parameters);
    }
    public static string GetPropertyPathAllProperties<TGridItem, TProperty>(Expression<Func<TGridItem, TProperty>> expression)
    {
        var memberExpression = expression.Body as MemberExpression;
        if (memberExpression == null)
        {
            throw new ArgumentException("Expression must be a member expression");
        }

        var propertyPath = new Stack<string>();
        while (memberExpression != null)
        {
            propertyPath.Push(memberExpression.Member.Name);
            memberExpression = memberExpression.Expression as MemberExpression;
        }

        return string.Join(".", propertyPath);
    }
}