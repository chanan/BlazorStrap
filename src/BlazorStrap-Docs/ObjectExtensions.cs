namespace BlazorStrap_Docs
{
    public static class ObjectExtensions
    {
        public static T? Clone<T>(this T source)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(System.Text.Json.JsonSerializer.Serialize(source));
        }
    }
}