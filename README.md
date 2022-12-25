# BlazorStrap
### Bootstrap 4/5 components for Blazor

## Install

[![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.svg)](https://www.nuget.org/packages/BlazorStrap/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorStrap?color=orange)](https://www.nuget.org/packages/BlazorStrap/)
![Nuget](https://img.shields.io/nuget/dt/BlazorStrap)
![GitHub stars](https://img.shields.io/github/stars/chanan/BlazorStrap?color=orange)
[![Gitter chat](https://img.shields.io/badge/gitter-join-na?logo=gitter)](https://gitter.im/BlazorStrap/community)
[![Gitter chat](https://img.shields.io/badge/discord-join-na?logo=discord)](https://discord.gg/V7Y8s7WprR)

## Bootstrap 5 [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.V5.svg)](https://www.nuget.org/packages/BlazorStrap.V5/)
`We do not include bootstrap.min.css in the package. This already exists in the blazor wasm and blazor server side templates.`

### Install
##### Blazor WebAssembly
1. - Download BlazorStrap package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap)
   - Download BlazorStrap.V5 package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap.V5)
      - Both packages should be the same version.
2. Modify your index.html with the following.
   1. Inside the ```<head>``` add
      1. ```<link href="YourAssemblyName.styles.css" rel="stylesheet">``` )
      2. ```<link href="path_to_bootstrap.min.css" rel="stylesheet" integrity="if_needed" />"```
   2. At the end of the ```<body>``` add
      1. ```<script src="_content/BlazorStrap/popper.min.js"></script>```
      2. ```<script src="_content/BlazorStrap/blazorstrap.js"></script>```
3. In ```Program.cs``` add
   1. ```builder.Services.AddBlazorStrap();```
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap.V5```
-----
##### Blazor Server Side
1.
   - Download BlazorStrap package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap)
   - Download BlazorStrap.V5 package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap.V5)
      - Both packages should be the same version.
2. Modify your _host.cshtml with the following.
   1. Inside the ```<head>``` add
      1. ```<link href="YourAssemblyName.styles.css" rel="stylesheet">```
      2. ```<link href="path_to_bootstrap.min.css" rel="stylesheet" integrity="if_needed" />"```
   2. At the end of the ```<body>``` add
      1. ```<script src="_content/BlazorStrap/popper.min.js"></script>```
      2. ```<script src="_content/BlazorStrap/blazorstrap.js"></script>```
3. In ```Program.cs``` or ```Startup.cs``` add
   1. ```Services.AddBlazorStrap();``` to your build pipeline
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap.V5```

## Bootstrap 4 [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.V4.svg)](https://www.nuget.org/packages/BlazorStrap.V4/)
`We do not include bootstrap.min.css in the package. This already exists in the blazor wasm and blazor server side templates.`

### Install
##### Blazor WebAssembly
1. - Download BlazorStrap package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap)
   - Download BlazorStrap.V4 package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap.V4)
      - Both packages should be the same version.
2. Modify your index.html with the following.
   1. Inside the ```<head>``` add
      1. ```<link href="YourAssemblyName.styles.css" rel="stylesheet">``` )
      2. ```<link href="path_to_bootstrap.min.css" rel="stylesheet" integrity="if_needed" />"```
   2. At the end of the ```<body>``` add
      1. ```<script src="_content/BlazorStrap/popper.min.js"></script>```
      2. ```<script src="_content/BlazorStrap/blazorstrap.js"></script>```
3. In ```Program.cs``` add
   1. ```builder.Services.AddBlazorStrap();```
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap.V4```
-----
##### Blazor Server Side
1. - Download BlazorStrap package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap)
- Download BlazorStrap.V4 package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.1.x-blue)](https://www.nuget.org/packages/BlazorStrap.V4)
   - Both packages should be the same version.-
2. Modify your _host.cshtml with the following.
   1. Inside the ```<head>``` add
      1. ```<link href="YourAssemblyName.styles.css" rel="stylesheet">```
      2. ```<link href="path_to_bootstrap.min.css" rel="stylesheet" integrity="if_needed" />"```
   2. At the end of the ```<body>``` add
      1. ```<script src="_content/BlazorStrap/popper.min.js"></script>```
      2. ```<script src="_content/BlazorStrap/blazorstrap.js"></script>```
3. In ```Program.cs``` or ```Startup.cs``` add
   1. ```Services.AddBlazorStrap();``` to your build pipeline
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap.V4```


## Extensions:
* [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.Extensions.FluentValidation.svg)](https://www.nuget.org/packages/BlazorStrap.Extensions.FluentValidation/) BlazorStrap.Extensions.FluentValidation
* [![NuGet Pre Release](https://img.shields.io/nuget/v/BlazorStrap.Extensions.TreeView.svg)](https://www.nuget.org/packages/BlazorStrap.Extensions.TreeView/) BlazorStrap.Extensions.TreeView
