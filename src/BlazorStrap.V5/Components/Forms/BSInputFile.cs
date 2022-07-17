using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V5
{
    public class BSInputFile<TValue> : BSInputFileBase<TValue>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("form-control", !RemoveDefaultClass)
                .AddClass(ValidClass, IsValid)
                .AddClass(InvalidClass, IsInvalid)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .AddClass(ValidClass, IsValid)
                .AddClass(InvalidClass, IsInvalid)
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<InputFile>(0);
            builder.AddAttribute(1, "OnChange", EventCallback.Factory.Create<InputFileChangeEventArgs>(this, OnFileChange));
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "onclick", OnFileClick);
            if (Helper?.Id != null)
            {
                builder.AddAttribute(4, "id", Helper.Id);
            }
            builder.AddMultipleAttributes(5, Attributes);
            builder.CloseComponent();
        }
    }
}
