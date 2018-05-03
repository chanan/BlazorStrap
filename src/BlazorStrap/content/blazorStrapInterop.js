Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Log', function (message) {
    console.log('message: ', message);
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.ChangeBody', function (classname) {
    document.body.className = classname;
    return true;
});


Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Popper', function (targetId, popoverId, placement) {
    var reference = document.getElementById(targetId);
    var popper = document.getElementById(popoverId);
    var anotherPopper = new Popper(reference, popper,
        {
            placement
        }
    );
    return true;
});