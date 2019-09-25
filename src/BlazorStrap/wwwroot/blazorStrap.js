var link;

window.blazorStrap = {
    animationEvent: function (event) {
        if (event.target.hasAttributes()) {
            var name = "";
            var attrs = event.target.attributes;
            for (var i = 0; i < attrs.length; ++i) {
                 name = attrs[i].name;
                if (name.includes("_bl_")) {
                    name = name.replace("_bl_", "");
                    break;
                }
            }
            DotNet.invokeMethodAsync("BlazorStrap", "OnAnimationEnd", name);
        }
        else {
            DotNet.invokeMethodAsync("BlazorStrap", "OnAnimationEnd", event.target.id);
        }
    },
    log: function (message) {
        console.log("message: ", message);
        return true;
    },
    addEventAnimationEnd(el) {
        el.addEventListener('animationend', window.blazorStrap.animationEvent);
        el.addEventListener('transitionend', window.blazorStrap.animationEvent);
        return true;
    },
    removeEventAnimationEnd(el) {
        el.removeEventListener('animationend', window.blazorStrap.animationEvent);
        el.removeEventListener('transitionend', window.blazorStrap.animationEvent);
        return true;
    },
    addClass2Elements: function (el,el2, Classname) {
        el.classList.add(Classname);
        el2.classList.add(Classname);
        return true;
    },
    removeClass2Elements: function (el, el2, Classname) {
        el.classList.remove(Classname);
        el2.classList.remove(Classname);
        return true;
    },
    addClass: function (el, Classname) {
        var r = Classname.split(" ");
        for (var i = 0; i < r.length; ++i) {
            el.classList.add(r[i]);
        }
        DotNet.invokeMethodAsync("BlazorStrap", "OnAddClass", el.id, Classname);
        return true;
    },
    getScrollHeight: function (el) {
        return el.scrollHeight;
        
    },
    setStyle: function (el, key, value) {
        el.style[key] = value;
        return true;
    },
    removeClass: function (el, Classname) {
        var r = Classname.split(" ");
        for (var i = 0; i < r.length; ++i) {
            el.classList.remove(r[i]);
        }
        return true;
    },
    addBodyClass: function (Classname) {
        if (Classname == "modal-open") {
            this.changeBodyPaddingRight("17px");
        }
        document.body.classList.add(Classname);
        return true;
    },
    removeBodyClass: function (Classname) {
        if (Classname == "modal-open") {
            this.changeBodyPaddingRight("");
        }
        document.body.classList.remove(Classname);
        return true;
    },
    changeBodyClass: function (Classname) {
        document.body.className = Classname;
        return true;
    },
    changeBodyPaddingRight: function (padding) {
        var dpi = window.devicePixelRatio;
        if (dpi == 1 || !padding) {
            document.body.style.paddingRight = padding;
        }
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
        }
        else if (theme === 'custom') {
            link.href = version;
        }
        else {
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

