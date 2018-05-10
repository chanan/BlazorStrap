Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Log', function (message) {
    console.log('message: ', message);
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.ChangeBody', function (classname) {
    document.body.className = classname;
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Popper', function (target, popperId, arrow, placement) {
    var reference = document.getElementById(target);
    var popper = document.getElementById(popperId);
    showPopper(reference, popper, arrow, placement);
    return true;
});

Blazor.registerFunction('BlazorStrap.BlazorStrapInterop.Tooltip', function (target, tooltip, arrow, placement) {
    var reference = document.getElementById(target);
    reference.addEventListener('mouseover', function () {
        tooltip.className = "tooltip fade show bs-popover-" + placement;
        showPopper(reference, tooltip, arrow, placement);
    });
    reference.addEventListener('mouseout', function () {
        tooltip.className = "tooltip hide";
    });
    return true;
});

function showPopper(reference, popper, arrow, placement) {
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
}
