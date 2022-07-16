## Input File
:::{.bd-callout .bd-callout-info}
*Note* `BSInputFile` is a wrapper of the basic ``InputFile` with helps for validation. See [ASP.NET Core Blazor file uploads](https://docs.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-6.0&pivots=server) for more details
With our <code>BS</code> helper class to style your form or use html tags and class.
:::
#### Component \<BSInput\>
See [shared](forms/shared) for additional parameters
**`*`** indicates required Parameters
:::


| Parameter    | Type          | Valid                    | Remarks/Output                    | 
|--------------|---------------|--------------------------|-----------------------------------|
| InvalidClass | string        | string                   |                                   | {.table-striped .p-2}
| ValidClass   | string        | string                   |                                   |
| IsBasic      | T             | N/A                      |                                   |
| IsDisabled   | bool          | true/false               | disabled                          |
| IsInvalid    | bool          | true/false               | InvalidClass                      |
| IsValid      | bool          | true/false               | ValidClass                        |
| OnChange     | EventCallback | InputFileChangeEventArgs |
| ValidWhen    | Expression    | Func of TValue           | Example @(() => Modal.HasFile)    |
:::
`@("value")` is not required it's a line declaration of a string to make the demo work

### Example

{{sample=V5/Forms/InputFile/InputFile1}}