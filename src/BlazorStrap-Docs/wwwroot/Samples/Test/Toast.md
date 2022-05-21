@inject IBlazorStrap _blazorStrap
<div style="height:500px">
<BSToaster/>
</div>
<h3>Toast</h3>
  <BSButton @onclick="Show" Color="BSColor.Primary">Show</BSButton>

@code {
    private void Show()
    {
        _blazorStrap.Toaster.Add("Live Example " , "" , o =>
        {
            o.Template = typeof(SampleModels.ToastTemplate);
            o.Data = new SampleModels.ToastSample() { Text = "This is a test", Title = "Test" };
        });
    }
}
