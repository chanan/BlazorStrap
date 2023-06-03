<div class="d-flex align-items-center">
    <img src="https://blazorstrap.io/logo5.svg" alt="BootStrap Logo" style="height: 100px"/>
    <h1>BlazorStrap</h1>
</div>

Bootstrap 5 Components for Blazor Framework
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
3. In ```Program.cs``` add 
   1. ```builder.Services.AddBlazorStrap();```
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap.V5```
   2. In your layout after @Body
    - Add ```<BSCore/>```

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
3. In ```Program.cs``` or ```Startup.cs``` add
   1. ```Services.AddBlazorStrap();``` to your build pipeline
4. In ```_Imports.razor``` add
   1. ```@using BlazorStrap.V5```
   2. In your layout after @Body
    - Add ```<BSCore/>```
   