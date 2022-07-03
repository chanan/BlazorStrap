using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    internal static class ComponentToRenderFragment
    {
        internal static RenderFragment BuildRenderFragment<CT>(this CT source, string component, int version, Action<object> reference)  where CT: class
        {
            var type = Type.GetType($"BlazorStrap.Bootstrap.V{version}.{component}");
            if (type == null) throw new ArgumentNullException(nameof(type));
            int s = 0;
            var result = new RenderFragment(builder =>
            {
                builder.OpenComponent(s, type);
                foreach (var property in source.GetType().GetProperties().OrderBy(p => p.Name))
                {
                    if (property.Name != "Attributes")
                    {
                        if (property.CustomAttributes.Any(q => q.AttributeType == typeof(ParameterAttribute)))
                        {
                            builder.AddAttribute(s++, property.Name, property.GetValue(source, null));
                        }
                    }
                }
                builder.AddAttribute(s++, "Version", version);
                var attributes = source?.GetType()?.GetProperty("Attributes")?.GetValue(source);
                if (attributes != null)
                {
                    builder.AddMultipleAttributes(s++, (IEnumerable<KeyValuePair<string, object>>?)attributes);
                }
                builder.AddComponentReferenceCapture(s++, reference);
                builder.CloseComponent();
            });
            return result;
        }
    }
}
