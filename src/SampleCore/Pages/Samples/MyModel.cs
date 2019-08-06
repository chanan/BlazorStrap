using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleCore.Pages.Samples
{
    public class MyModel
    {
       
            [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
            [StringLength(10, ErrorMessage = "Name is too long.")]
            public string Name { get; set; }
        
    }
}
