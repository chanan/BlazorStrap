using System.Diagnostics;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap
{
    public partial class BSBreadcrumb : BlazorStrapBase, IDisposable
    {
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Parameter] public string Divider { get; set; } = "'/'";
        [Parameter] public string? BasePath { get; set; }
        [Parameter] public Dictionary<string, string> Labels { get; set; } = new();
        private Dictionary<string, string> Tree { get; set; } = new();

        private string? ClassBuilder => new CssBuilder("breadcrumb")
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void OnInitialized()
        {
            if (string.IsNullOrEmpty(BasePath)) return;
            if (NavigationManager == null) return;
            NavigationManager.LocationChanged += OnLocationChanged;
            Tree = GetPath(NavigationManager.Uri.ToLower(), BasePath.ToLower(), Labels, NavigationManager?.BaseUri.ToLower() ?? "");
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BasePath)) return;
            if (NavigationManager == null) return;
            Tree = GetPath(NavigationManager.Uri.ToLower(), BasePath.ToLower(), Labels, NavigationManager?.BaseUri.ToLower() ?? "");
            StateHasChanged();
        }

        private Dictionary<string, string> GetPath(string path, string basePath, Dictionary<string, string> labels, string navManBase)
        {
            //Strip out protocol
            var prefix = path.Split("://")[0];
            path = path[(prefix.Length + 3)..];
            //path = path.Replace(prefix + "://", "");
            //Strip everything before base
            if (basePath != "/")
            {
                if (path.Contains(basePath))
                {
                    var baseSplit = path.Split(basePath);
                    prefix += "://" + baseSplit[0];
                    path = basePath + baseSplit[1];
                }
                else
                {
                    prefix += ":/";
                }
            }
            else
            {
                prefix += ":/";
            }

            var result = new Dictionary<string, string>();
            //var result = new List<(string path, string label)>();
            path = path.TrimStart('/');
            var steps = path.Split('/');

            var uri = prefix + "/" + steps[0];
            for (var i = 0; i < steps.Length; ++i)
            {
                if (i != 0)
                    uri += "/" + steps[i];
                result.Add(uri, LabelLookup(uri, steps[i], prefix, labels, navManBase));
            }

            return result;
        }

        private static string LabelLookup(string path, string item, string prefix, Dictionary<string, string> labels,
            string navManBase)
        {
            var query = path.Replace(prefix, "");
            if (path + "/" == navManBase)
            {
                return labels.ContainsKey("/") ? labels["/"] : "Home";
            }

            if (labels.ContainsKey(query))
            {
                return labels[query];
            }

            return item.FirstCharToUpper() ?? "";
        }

        public void Dispose()
        {
            if (NavigationManager != null) NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}