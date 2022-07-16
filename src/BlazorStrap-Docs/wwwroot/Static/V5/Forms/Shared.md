## Forms Shared 
From BSInputBase not used on Files
See [shared](layout/shared) for additional parameters    
:::

| Parameter        | Type          | Valid          | Remarks/Output          | 
|------------------|---------------|----------------|-------------------------|
| DebounceInterval | int           | int            |                         | {.table-striped .p-2}
| InvalidClass     | string        | string         | default .is-invalid     |
| ValidClass       | string        | string         | default .is-valid       |
| IsDisabled       | bool          | true/false     | disabled                |
| IsInvalid        | bool          | true/false     | InvalidClass            |
| IsValid          | bool          | true/false     | ValidClass              |
| OnBlur           | EventCallback | FocusEventArgs |                         |
| OnFocus          | EventCallback | FocusEventArgs |                         |
| ValidateOnBlur   | bool          | true/false     |                         | 
| ValidateOnChange | bool          | true/false     |                         |
| ValidateOnInput  | bool          | true/false     | Debounced default 500ms |