# BlazorStrap

Bootstrap 4 Components for Blazor Framework

## Install

[![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.svg)](https://www.nuget.org/packages/BlazorStrap/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorStrap?color=orange)](https://www.nuget.org/packages/BlazorStrap/)
![Nuget](https://img.shields.io/nuget/dt/BlazorStrap)
![GitHub stars](https://img.shields.io/github/stars/chanan/BlazorStrap?color=orange)
[![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/BlazorStrap/community)

## Bootstrap 5
> Important: Target V5 for bootstrap 5
``` html
<PackageReference Include="BlazorStrap" Version="5.*.*" />
```

This version is still under development. Package is not released yet. Check out our progress. https://blazorstrap.io
### Blazor WebAssembly (Client-side Blazor)

1. Inside the `<head>` element of your `wwwroot/index.html`, add `<script src="_content/BlazorStrap/blazorstrap.js"></script><script src="_content/BlazorStrap/popper.min.js"></script>`.
2. In `Program`, add `builder.Services.AddBlazorStrap();`.

### Blazor Server (Server-side Blazor)

1. Inside the `<head>` element of your `Pages/_Host.cshtml`, add `<script src="_content/BlazorStrap/blazorstrap.js"></script><script src="_content/BlazorStrap/popper.min.js"></script>`.
2. In `Startup`, add `builder.Services.AddBlazorStrap();`.

---------
## Bootstrap 4
> Important:
Target V1 for bootstrap 4
``` html 
<PackageReference Include="BlazorStrap" Version="1.*.*" />
```

### Blazor WebAssembly (Client-side Blazor)

1. Inside the `<head>` element of your `wwwroot/index.html`, add `<script src="_content/BlazorStrap/blazorStrap.js"></script><script src="_content/BlazorStrap/popper.min.js"></script>`.
2. In `Program`, add `builder.Services.AddBootstrapCss();`.

### Blazor Server (Server-side Blazor)

1. Inside the `<head>` element of your `Pages/_Host.cshtml`, add `<script src="_content/BlazorStrap/blazorStrap.js"></script><script src="_content/BlazorStrap/popper.min.js"></script>`.
2. In `Startup`, add `Services.AddBootstrapCss();`.

## Docs
https://blazorstrap.io

## BlazorStyled

If you want to manage your styles in code and use dynamic styles you can check out my other project: [BlazorStyled](https://chanan.github.io/BlazorStyled)

## Change Log
https://github.com/chanan/BlazorStrap/releases

## Components:
* Alerts
* Badges
* Breadcrumbs
* Buttons
* ButtonGroups
* Cards
* Carousals
* Collapse
* Dropdowns
* OffCanvas V5
* Images
* Jumbotrons
* Figures
* Forms
* Layout (Container, Row, Col)
* Listgroups
* Navs
* Navbars
* Media
* Modals
* Pagination
* Popover
* Progress
* Tables
* Tabs
* Tooltip

## Extensions:
* [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.Extensions.BSDataTable.svg)](https://www.nuget.org/packages/BlazorStrap.Extensions.BSDataTable) BlazorStrap.Extensions.BSDataTable
* [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.Extensions.FluentValidation.svg)](https://www.nuget.org/packages/BlazorStrap.Extensions.FluentValidation/) BlazorStrap.Extensions.FluentValidation
* [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.Extensions.TreeView.svg)](https://www.nuget.org/packages/BlazorStrap.Extensions.TreeView/) BlazorStrap.Extensions.TreeView
