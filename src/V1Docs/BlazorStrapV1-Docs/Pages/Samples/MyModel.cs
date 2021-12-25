using System.ComponentModel.DataAnnotations;

namespace BlazorStrapV1_Docs.Pages.Samples
{
    public class MyModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

    }
}
