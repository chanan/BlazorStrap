using BlazorStrap.V5;
using BlazorStrap.V5.DataGrid;

namespace BlazorStrap_Docs.Samples.V5.Components.DataGrid;

public partial class DataGrid5
{
    private PaginationState _pagination = new PaginationState() { ItemsPerPage = 10 };
    private BSDataGrid<Product> _dataGrid;
    private ICollection<Product> _products;

    protected override void OnInitialized()
    {
        _products = GenerateProductData();
    }

    private static ICollection<Product> GenerateProductData()
    {
        var categories = Enum.GetValues<ProductCategory>();
        var random = new Random(42); // Fixed seed for consistent data
        var products = new List<Product>();

        var productNames = new[]
        {
            "Laptop Pro", "Wireless Mouse", "Gaming Keyboard", "Monitor 4K", "Tablet", 
            "Smartphone", "Headphones", "Webcam HD", "Printer", "Router", 
            "Smart Watch", "Power Bank", "USB Drive", "Hard Drive", "SSD Drive",
            "Graphics Card", "Memory RAM", "Processor", "Motherboard", "Case",
            "Speakers", "Microphone", "Desk Lamp", "Office Chair", "Standing Desk"
        };

        for (int i = 1; i <= 50; i++)
        {
            var createdDate = DateTime.Now.AddDays(-random.Next(1, 365));
            var lastModifiedOffset = DateTimeOffset.Now.AddDays(-random.Next(0, 30));

            products.Add(new Product
            {
                Id = i,
                Name = productNames[random.Next(productNames.Length)] + $" {i}",
                SerialNumber = 1000000000L + i * 1000 + random.Next(100, 999),
                Price = Math.Round((decimal)(random.NextDouble() * 2000 + 50), 2),
                IsActive = random.Next(100) > 20, // ~80% active
                Category = categories[random.Next(categories.Length)],
                CreatedDate = createdDate,
                LastModified = lastModifiedOffset,
                Rating = Math.Round(random.NextDouble() * 4 + 1, 1) // 1.0 to 5.0
            });
        }

        return products;
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long SerialNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public double Rating { get; set; }
}

public enum ProductCategory
{
    Electronics,
    Computers,
    Accessories,
    Gaming,
    Office,
    Mobile,
    Audio,
    Storage,
    Networking
}