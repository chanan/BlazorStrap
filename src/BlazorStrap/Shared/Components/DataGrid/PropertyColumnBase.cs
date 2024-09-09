using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid;

public abstract class PropertyColumnBase<TGridItem, TProp> : ColumnBase<TGridItem>
{
    [Parameter, EditorRequired] public Expression<Func<TGridItem, TProp>> Property { get; set; } = default!;
    private Expression<Func<TGridItem, TProp>>? _property;
    private Func<TGridItem, string?>? _cellValueFunc;
    protected override void OnParametersSet()
    {
        if (!Equals(_property,Property))
        {   
            _property = Property;
            var compiledPropertyExpression = Property.Compile();
            PropertyPath = ExpressionHelper.GetPropertyPath(_property);
            _cellValueFunc = item => compiledPropertyExpression!(item)?.ToString();
            
        }
        
        if(Title is null && Property.Body is MemberExpression memberExpression)
        {
            Title = memberExpression.Member.Name;
        }
 
    }

    public override void CellContent(RenderTreeBuilder builder, TGridItem item)
    {
        if (_cellValueFunc is null) return;
        builder.AddContent(0, _cellValueFunc(item));
    }

}