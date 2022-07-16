using System.Text.Json;
using System.Text.Json.Serialization;
namespace BlazorStrap.Utilities
{

    public class JavascriptEvent
    {
        [JsonPropertyName("target")]
        public Target Target { get; set; } = new Target();
        [JsonPropertyName("key")]
        public string Key { get; set; } = "";

        [JsonPropertyName("parent")]
        public JavascriptEvent? Parent { get; set; } 
        [JsonPropertyName("clientWidth")]
        public int ClientWidth { get; set; } = 0;
        [JsonExtensionData]
        public IDictionary<string, JsonElement> Unmatched { get; set; } = new Dictionary<string, JsonElement>();
    }

    public class Target
    {
        [JsonPropertyName("nodeName")]
        public string? NodeName { get; set; }
        [JsonPropertyName("classList")]
        public Dictionary<string, string> ClassList { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("dataId")]
        public string? DataId { get; set; }
        [JsonPropertyName("targetId")]
        public string? TargetId { get; set; }
        [JsonPropertyName("childrenId")] public string[]? ChildrenId { get; set; }
        [JsonExtensionData]
        public IDictionary<string, JsonElement> Unmatched { get; set; } = new Dictionary<string, JsonElement>();
    }
}