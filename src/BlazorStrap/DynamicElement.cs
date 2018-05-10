using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    /// <summary>
    /// Renders an element with the specified name and attributes. This is useful
    /// when you want to combine a set of attributes declared at compile time with
    /// another set determined at runtime.
    /// </summary>
    public class DynamicElement : IComponent, IHandleEvent
    {
        /// <summary>
        /// Gets or sets the name of the element to render.
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the attributes to render.
        /// </summary>
        public IReadOnlyDictionary<string, object> Attributes
        {
            // The property is only declared for intellisense. It's not used at runtime.
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="Microsoft.AspNetCore.Blazor.ElementRef"/>.
        /// </summary>
        public ElementRef ElementRef { get; private set; }

        private RenderHandle _renderHandle;
        private IDictionary<string, object> _attributesToRender;
        private RenderFragment _childContent;

        /// <inheritdoc />
        public void Init(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
        }

        /// <inheritdoc />
        public void SetParameters(ParameterCollection parameters)
        {
            _attributesToRender = (IDictionary<string, object>)parameters.ToDictionary();
            _childContent = GetAndRemove<RenderFragment>(_attributesToRender, RenderTreeBuilder.ChildContent);

            TagName = GetAndRemove<string>(_attributesToRender, nameof(TagName))
                ?? throw new InvalidOperationException($"No value was supplied for required parameter '{nameof(TagName)}'.");

            // Combine any explicitly-supplied attributes with the remaining parameters
            var attributesParam = GetAndRemove<IReadOnlyDictionary<string, object>>(_attributesToRender, nameof(Attributes));
           
            if (attributesParam != null)
            {
                foreach (var kvp in attributesParam)
                {
                    if (kvp.Value != null
                        && _attributesToRender.TryGetValue(kvp.Key, out var existingValue)
                        && existingValue != null)
                    {
                        _attributesToRender[kvp.Key] = existingValue.ToString()
                            + " " + kvp.Value.ToString();
                    }
                    else
                    {
                        _attributesToRender[kvp.Key] = kvp.Value;
                    }
                }
            }
            _renderHandle.Render(Render);
        }

        private static T GetAndRemove<T>(IDictionary<string, object> values, string key)
        {
            if (values.TryGetValue(key, out var value))
            {
                values.Remove(key);
                return (T)value;
            }
            else
            {
                return default;
            }
        }

        private void Render(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, TagName);
            foreach (var kvp in _attributesToRender)
            {
                builder.AddAttribute(1, kvp.Key, kvp.Value);
            }
            builder.AddElementReferenceCapture(2, capturedRef => { ElementRef = capturedRef; });
            builder.AddContent(3, _childContent);
            builder.CloseElement();
        }

        public void HandleEvent(EventHandlerInvoker handler, UIEventArgs args)
        {
            //Implementing IHandleEvent as a workaround for https://github.com/aspnet/Blazor/issues/656
            handler.Invoke(args);
        }
    }
}
