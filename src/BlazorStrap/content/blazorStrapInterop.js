Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Log', function (message) {
    console.log('message: ', message);
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.ChangeBody', function (classname) {
    document.body.className = classname;
    return true;
});


Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Popper', function (target, popper, arrow, placement) {
    var reference = document.getElementById(target);
    var thePopper = new Popper(reference, popper,
        {
            placement,
            modifiers: {
                offset: {
                    offset: 0
                },
                flip: {
                    behavior: 'flip'
                },
                arrow: {
                    element: arrow,
                    enabled: true
                },
                preventOverflow: {
                    boundary: 'scrollParent'
                }
                
            }
        }
    );
    return true;
});
