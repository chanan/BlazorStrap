var link;

window.blazorStrap = {
    log: function (message) {
        console.log("message: ", message);
        return true;
    },
    changeBody: function (classname) {
        document.body.className = classname;
        return true;
    },
    changeBodyModal: function (padding) {
        document.body.style.paddingRight = padding;
        return true;
    },
    popper: function (target, popperId, arrow, placement) {
        var reference = document.getElementById(target);
        var popper = document.getElementById(popperId);
        showPopper(reference, popper, arrow, placement);
        return true;
    },
    tooltip: function (target, tooltip, arrow, placement) {
        var instance;
        var reference = document.getElementById(target);
        function mouseoverHandler() {
            reference.removeEventListener("mouseover", mouseoverHandler);
            reference.addEventListener("mouseout", mouseoutHandler);
            tooltip.className = "tooltip fade show bs-tooltip-" + placement;
            instance = showPopper(reference, tooltip, arrow, placement);
        }
        function mouseoutHandler() {
            reference.removeEventListener("mouseout", mouseoutHandler);
            reference.addEventListener("mouseover", mouseoverHandler);
            tooltip.className = "tooltip d-none";
            if (instance) {
                instance.destroy && instance.destroy();
                instance = undefined;
            }
        }
        reference.addEventListener("mouseover", mouseoverHandler);
        return true;
    },
    modelEscape: function () {
        document.body.onkeydown = function (e) {
            if (e.key == "Escape") {
                document.body.onkeydown = null;
                DotNet.invokeMethodAsync("BlazorStrap", "OnEscape");
            }
        };
    },
    focusElement: function (element) {
        element.focus();
    },
    setBootstrapCSS: function (theme, version) {
        if (link === undefined) {
            link = document.createElement('link');
            document.head.insertBefore(link, document.head.firstChild);
            link.type = 'text/css';
            link.rel = 'stylesheet';
        }
        if (theme === 'bootstrap') {
            link.href = `https://stackpath.bootstrapcdn.com/bootstrap/${version}/css/bootstrap.min.css`;
        } else {
            link.href = `https://stackpath.bootstrapcdn.com/bootswatch/${version}/${theme}/bootstrap.min.css`;
        }
        return true;
    }
};

function showPopper(reference, popper, arrow, placement) {
    var thePopper = new Popper(reference, popper,
        {
            placement,
            modifiers: {
                offset: {
                    offset: 0
                },
                flip: {
                    behavior: "flip"
                },
                arrow: {
                    element: arrow,
                    enabled: true
                },
                preventOverflow: {
                    boundary: "scrollParent"
                }

            }
        }
    );
    return thePopper;
}