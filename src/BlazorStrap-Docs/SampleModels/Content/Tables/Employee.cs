using BlazorStrap;

namespace BlazorStrap_Docs.SamplesHelpers.Content.Tables
{
    public class Employee
    {
            public string Id { get; set; } = null!;
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public BSColor RowColor { get; set; }
    }
}