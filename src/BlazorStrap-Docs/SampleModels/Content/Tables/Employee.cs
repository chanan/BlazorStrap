using BlazorStrap;
using System.ComponentModel.DataAnnotations;

namespace BlazorStrap_Docs.SamplesHelpers.Content.Tables
{
    public class Employee
    {
            public string Id { get; set; } = null!;
        [Required]
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public BSColor RowColor { get; set; }
    }
}