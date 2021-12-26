Bootstrap 5 Components for Blazor Framework

### Install
##### Blazor WebAssembly
1. Download the V5.0 package from nuget: [![nuget](https://img.shields.io/badge/nuget-Download%205.x-blue)](https://www.nuget.org/packages/BlazorStrap)
2. Modify your index.html with the following.
   1. Inside the ```<head>``` add 
      1. ```<link href="BlazorStrap.WASM.styles.css" rel="stylesheet">``` )
   2. At the end of the ```<body>``` add 
      1. ```<script src="_content/BlazorStrap/popper.min.js"></script>```
      2. ```<script src="_content/BlazorStrap/blazorstrap.js"></script>```
3. In ```Program.cs``` add 
   1. ```builder.Services.AddBlazorStrap();```
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap```
-----
##### Blazor WebAssembly
1. Download the V5.0 package from nuget:[![nuget](https://img.shields.io/badge/nuget-Download%205.x-blue)](https://www.nuget.org/packages/BlazorStrap)
2. Modify your _host.cshtml with the following.
   1. Inside the ```<head>``` add
      1. ```<link href="BlazorStrap.WASM.styles.css" rel="stylesheet">```
   2. At the end of the ```<body>``` add
      1. ```<script src="_content/BlazorStrap/popper.min.js"></script>```
      2. ```<script src="_content/BlazorStrap/blazorstrap.js"></script>```
3. In ```Program.cs``` or ```Startup.cs``` add
   1. ```Services.AddBlazorStrap();``` to your build pipeline
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap```
   