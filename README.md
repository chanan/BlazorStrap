# BlazorStrap

Bootstrap 4 Components for Blazor Framework

## Install

[![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.svg)](https://www.nuget.org/packages/BlazorStrap/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorStrap?color=orange)](https://www.nuget.org/packages/BlazorStrap/)
![Nuget](https://img.shields.io/nuget/dt/BlazorStrap)
![GitHub stars](https://img.shields.io/github/stars/chanan/BlazorStrap?color=orange)
[![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/BlazorStrap/community)

### Blazor WebAssembly (Client-side Blazor)

1. Inside the `<head>` element of your `wwwroot/index.html`, add `<script src="_content/BlazorStrap/blazorStrap.js"></script>`.
2. In `Program`, add `builder.Services.AddBootstrapCss();`.

### Blazor Server (Server-side Blazor)

1. Inside the `<head>` element of your `Pages/_Host.cshtml`, add `<script src="_content/BlazorStrap/blazorStrap.js"></script>`.
2. In `Startup`, add `Services.AddBootstrapCss();`.

## Animations
> Animations are always on as they no longer require javascript to function.

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
* Buttons (excluding Checkboxes and Radio buttons)
* ButtonGroups
* Cards
* Carousals
* Collapse
* Dropdowns
* Images
* Jumbotrons
* Figures
* Forms - mostly done (Binding still pending)
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
