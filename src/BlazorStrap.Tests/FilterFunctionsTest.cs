using System;
using System.Collections.Generic;
using System.Linq;
using BlazorStrap.Shared.Components.DataGrid.BSDataGirdHelpers;
using BlazorStrap.Tests.Models;
using Xunit;

namespace BlazorStrap.Tests;

public class FilterFunctionsTests
{
    
    [Fact]
    public void Operator_Equal_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99 },
            new Product { Id = 2, Name = "Keyboard", Price = 49.99 }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.Equal, "Laptop")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Laptop", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_Equal_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.Equal, "Dell")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Dell", filtered.First().Details.Manufacturer);
    }
    
    [Fact]
    public void Operator_NotEqual_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99 },
            new Product { Id = 2, Name = "Keyboard", Price = 49.99 }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.NotEqual, "Laptop")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Keyboard", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_NotEqual_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.NotEqual, "Dell")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Samsung", filtered.First().Details.Manufacturer);
    }
    
    [Fact]
    public void Operator_StartsWith_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.StartsWith, "Lap")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Laptop", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_StartsWith_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.StartsWith, "De")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Dell", filtered.First().Details.Manufacturer);
    }
    
    [Fact]
    public void Operator_EndsWith_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.EndsWith, "top")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Laptop", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_EndsWith_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.EndsWith, "ll")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Dell", filtered.First().Details.Manufacturer);
    }
    
    [Fact]
    public void Operator_Contains_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.Contains, "pto")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Laptop", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_Contains_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.Contains, "el")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Dell", filtered.First().Details.Manufacturer);
    }

    [Fact]
    public void Operator_NotContains_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.NotContains, "pto")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Monitor", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_NotContains_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.NotContains, "el")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Samsung", filtered.First().Details.Manufacturer);
    }
    
    [Fact]
    public void Operator_IsEmpty_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.IsEmpty, "")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_IsEmpty_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.IsEmpty, "")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("", filtered.First().Details.Manufacturer);
    }
    [Fact]
    public void Operator_IsNotEmpty_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "", Details = new ProductDetails { Manufacturer = "Dell" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Name", Operator.IsNotEmpty, "")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Monitor", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_IsNotEmpty_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "" } },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" } }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.Manufacturer", Operator.IsNotEmpty, "")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Samsung", filtered.First().Details.Manufacturer);
    }
    
    [Fact]
    public void Operator_GreaterThan_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" }, Price = 1200.00},
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" }, Price = 500.00}
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Price", Operator.GreaterThan, 500.00)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Laptop", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_GreaterThan_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "", ManufactureDate = DateTime.Now.AddDays(5)} },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung", ManufactureDate = DateTime.Now} }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.ManufactureDate", Operator.GreaterThan, DateTime.Now)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Laptop", filtered.First().Name);
    }
    [Fact]
    public void Operator_GreaterThanOrEqual_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" }, Price = 1200.00},
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" }, Price = 500.00}
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Price", Operator.GreaterThanOrEqual, 500.00)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Collection(filtered,
            item => Assert.Equal("Laptop", item.Name),
            item => Assert.Equal("Monitor", item.Name));
    }
    
    [Fact]
    public void Operator_GreaterThanOrEqual_Nested_FiltersCorrectly()
    {
        var date = DateTime.Now;
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "", ManufactureDate = DateTime.Now.AddDays(5)} },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung", ManufactureDate = date} }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.ManufactureDate", Operator.GreaterThanOrEqual, date)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Collection(filtered, 
            item => Assert.Equal("Laptop", item.Name),
            item => Assert.Equal("Monitor", item.Name)
        );
    }
    // LessThan
     [Fact]
    public void Operator_LessThan_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" }, Price = 1200.00},
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" }, Price = 500.00}
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Price", Operator.LessThan, 1200.00)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Monitor", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_LessThan_Nested_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "", ManufactureDate = DateTime.Now.AddDays(5)} },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung", ManufactureDate = DateTime.Now.AddDays(-5)} }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.ManufactureDate", Operator.LessThan, DateTime.Now)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Single(filtered);
        Assert.Equal("Monitor", filtered.First().Name);
    }
    
    [Fact]
    public void Operator_LessThanOrEqual_FiltersCorrectly()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell" }, Price = 1200.00},
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung" }, Price = 500.00}
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Price", Operator.LessThanOrEqual, 1200.00)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Collection(filtered,
            item => Assert.Equal("Laptop", item.Name),
            item => Assert.Equal("Monitor", item.Name));
    }
    
    
    [Fact]
    public void Operator_LessThanOrEqual_Nested_FiltersCorrectly()
    {
        var date = DateTime.Now;
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Details = new ProductDetails { Manufacturer = "Dell", ManufactureDate = DateTime.Now.AddDays(-5)} },
            new Product { Id = 2, Name = "Monitor", Details = new ProductDetails { Manufacturer = "Samsung", ManufactureDate = date} }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Details.ManufactureDate", Operator.LessThanOrEqual, date)
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Collection(filtered, 
            item => Assert.Equal("Laptop", item.Name),
            item => Assert.Equal("Monitor", item.Name)
        );
    }
    
    [Fact]
    public void NonNumericFilterOnNumericField_IgnoresFilter()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99 },
            new Product { Id = 2, Name = "Keyboard", Price = 49.99 }
        }.AsQueryable();

        var filters = new List<ColumnFilter>
        {
            CreateColumnFilter("Price", Operator.GreaterThan, "Test")
        };

        // Act
        var filtered = UnitTest.FilterFunctions_FiltersColumns(products, filters);

        // Assert
        Assert.Equal(2, filtered.Count());
    }
    
    private ColumnFilter<T> CreateColumnFilter<T>(string property, Operator @operator, T value)
    {
        return new ColumnFilter<T>(property, @operator, value);
    }
}