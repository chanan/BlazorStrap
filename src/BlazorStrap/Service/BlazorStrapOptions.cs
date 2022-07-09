namespace BlazorStrap.Service
{
    public class BlazorStrapOptions
    {
        public bool ShowDebugMessages { get; set; } 
        public BootstrapVersion BootstrapVersion { get; set; } = BootstrapVersion.Bootstrap5;
        /// <summary>
        /// Planned feature. Not currently used
        /// </summary>
        public string BasePath { get; set; } = string.Empty;
    }
}
