using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorStrap
{
    public interface IBSForm
    {
        string Class { get; set; }
        bool IsInline { get; set; }
        EventCallback<EditContext> OnInvalidSubmit { get; set; }
        EventCallback<EditContext> OnSubmit { get; set; }
        EventCallback<EditContext> OnValidSubmit { get; set; }
        bool UserValidation { get; set; }
        bool ValidateOnInit { get; set; }

        void ForceValidate();
        void FormIsReady(EditContext e);
    }
}
