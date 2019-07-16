using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSTabSelectedContent : BootstrapComponentBase
    {
        [CascadingParameter] protected BSTabGroup Group { get; set; }

        protected override void OnInit()
        {
            if(Group != null)
            {

            }
        }
    }
}
