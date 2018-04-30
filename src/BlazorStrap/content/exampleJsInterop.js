Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Log', function (message) {
    console.log('message: ', message);
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.ChangeBody', function (classname) {
    document.body.className = classname;
    return true;
});
