using System.Linq.Expressions;
using System.Reflection;

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
    
    public static Type? GetPropertyType<TGridItem>(string propertyPath)
    {
        if (string.IsNullOrEmpty(propertyPath)) return null ;
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
    
    public static ICollection<string> GetPropertyPaths<TGridItem>()
    {
        return GetPropertyPathRecursive(typeof(TGridItem));
    }
    // Recursive method that handles property path generation
    private static ICollection<string> GetPropertyPathRecursive(Type type, string parentPath = "", bool isRoot = true)
    {
        List<string> propertyPaths = new List<string>();
        // Reflect only public instance properties
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo property in properties)
        {
            string propertyPath = isRoot ? property.Name : $"{parentPath}.{property.Name}";
            propertyPaths.Add(propertyPath);

            // Check if the property is a class but not string, or a struct (non-primitive)
            if ((property.PropertyType.IsClass && property.PropertyType != typeof(string)) ||
                (property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive))
            {
                // if array, list, or dictionary, skip the recursion
                if (property.PropertyType.IsArray || property.PropertyType.IsGenericType)
                {
                    continue;
                }
                // Use reflection to invoke the method recursively with the property type
                ICollection<string> nestedPaths = GetPropertyPathRecursive(property.PropertyType, propertyPath, false);
                propertyPaths.AddRange(nestedPaths);
            }
        }

        return propertyPaths;
    }
}