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
}