using System;

namespace BlazorStrap.Tests.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public ProductDetails Details { get; set; }
}

public class ProductDetails
{
    public string Manufacturer { get; set; }
    public DateTime ManufactureDate { get; set; }
}