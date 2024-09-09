namespace BlazorStrap_Docs.Service
{
    public class Core
    {
        public event Action<string>? OnVersionChanged;
        public string _version = "V5";
        public string Version { get => _version; set
            {
                _version = value;
                OnVersionChanged?.Invoke(value);
            }
        }
    }
}
