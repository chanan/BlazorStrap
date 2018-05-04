Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Log', function (message) {
    console.log('message: ', message);
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.ChangeBody', function (classname) {
    document.body.className = classname;
    return true;
});


Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Popper', function (targetId, popper, arrow, placement) {
    console.log(arrow);
    var reference = document.getElementById(targetId);
    var anotherPopper = new Popper(reference, popper,
        {
            placement,
            //arrowElement: arrow,
            modifiers: {
                offset: {
                    offset: 0
                },
                flip: {
                    behavior: 'flip'
                },
                arrow: {
                    enabled: true,
                    element: 'x-arrow'
                },
                preventOverflow: {
                    boundary: 'scrollParent'
                }
                
            }
        }
    );
    return true;
});