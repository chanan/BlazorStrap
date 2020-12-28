var link;
var script;

// MDN String.include polyfill
if (!String.prototype.includes) {
    String.prototype.includes = function(search, start) {
        'use strict';
        if (search instanceof RegExp) {
            throw TypeError('first argument must not be a RegExp');
        }
        if (start === undefined) { start = 0; }
        return this.indexOf(search, start) !== -1;
    };
}

window.blazorStrap = {
    modal: {
        paddingRight: function (padding) {
            var dpi = window.devicePixelRatio;
            if (dpi === 1 || !padding) {
                document.body.style.paddingRight = padding;
            }
            return true;
        },
        eventListeners: [],
        open: function (id) {
            if (!document.body.classList.contains("modal-open")) {
                document.body.classList.add("modal-open");
            }
            var body = document.body,
                html = document.documentElement;
            var height = Math.max(body.scrollHeight, body.offsetHeight,
                html.clientHeight, html.scrollHeight, html.offsetHeight);
            if (height > window.innerHeight) {
                this.paddingRight("17px");
            }
            return id;
        },
        close: function (id) {
            for (var i = 0; i < this.eventListeners.length; ++i) {
                if (this.eventListeners[i].id == id) {
                    
                    if (i + 1 != this.eventListeners.length) {
                        this.eventListeners.splice(i, 1);
                    }
                    else {
                        // Removes this event listener.
                        document.removeEventListener("keyup", window.blazorStrap.modal.eventListeners[window.blazorStrap.modal.eventListeners.length - 1].func);
                        window.blazorStrap.modal.eventListeners.pop();

                        // Adds Event listener back to modal under closing modal.
                        if (window.blazorStrap.modal.eventListeners.length >= 1)
                            document.addEventListener("keyup", window.blazorStrap.modal.eventListeners[window.blazorStrap.modal.eventListeners.length - 1].func);
                        else {
                            document.body.classList.remove("modal-open");
                            window.blazorStrap.modal.paddingRight("");
                        }
                    }
                }
            }
        },
        initOnEscape: function (id) {
            this.eventListeners.push({id: id, func: function (e) {
                if (e.key == "Escape") {
                    DotNet.invokeMethodAsync("BlazorStrap", "OnModalEscape", id);

                    // Removes this event listener.
                    document.removeEventListener("keyup", window.blazorStrap.modal.eventListeners[window.blazorStrap.modal.eventListeners.length - 1].func);
                    window.blazorStrap.modal.eventListeners.pop();

                    // Adds Event listener back to modal under closing modal.
                    if (window.blazorStrap.modal.eventListeners.length >= 1)
                        document.addEventListener("keyup", window.blazorStrap.modal.eventListeners[window.blazorStrap.modal.eventListeners.length - 1].func);
                    else {
                        document.body.classList.remove("modal-open");
                        window.blazorStrap.modal.paddingRight("");
                    }
                };
            }});
            // Removes event listener from modal under current modal.
            if(this.eventListeners.length > 1)
                document.removeEventListener("keyup", this.eventListeners[this.eventListeners.length - 2].func);
            // Adds new event listener for just opened modal.
            document.addEventListener("keyup", this.eventListeners[this.eventListeners.length - 1].func);
            return id;
        }
    },
    poppers: [],
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
    changeBodyPaddingRight: function (padding) {
        var dpi = window.devicePixelRatio;
        if (dpi === 1 || !padding) {
            document.body.style.paddingRight = padding;
        }
        return true;
    },
    popper: function (target, popperId, arrow, placement) {
        window.blazorStrap.closeOtherPoppers(popperId);
        var reference = document.getElementById(target);
        var popper = document.getElementById(popperId);
        showPopper(reference, popper, arrow, placement);
        return true;
    },
    closeOtherPoppers: function(popperId) {
        window.blazorStrap.poppers.forEach(p => {
            if (p !== popperId) {
                var el = document.getElementById(p);
                if (el) {
                    el.style.visibility = "hidden";
                    el.style.pointerEvents = "none";
                }
            }
        });
        if (window.blazorStrap.poppers.indexOf(popperId) === -1) {
            window.blazorStrap.poppers.push(popperId);
        }
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
    modelEscape: function (dotnetHelper) {
        document.body.onkeydown = function (e) {
            if (e.key == "Escape") {
                document.body.onkeydown = null;
                dotnetHelper.invokeMethodAsync("OnEscape");
            }
        };
    },
    focusElement: function (element) {
        element.focus();
    },
    collapsingElement: function (element, show) {
        element.classList.remove("collapsing");
        element.classList.remove("collapse");
        var height = element.offsetHeight;
        element.classList.add("collapsing");

        if (show) {
            setTimeout(function () { element.style.height = height + "px"; }, 100)
        }
        else {
            element.style.height = height + "px";
            setTimeout(function () { element.style.height =""; }, 100)   
        }
        return true;
    },

    collapsingElementEnd: function (element) {
        if (element) {
            element.style.height = "";
            element.classList.remove("collapsing");
            element.classList.add("collapse");
        }
        return true;
    },
    setBootstrapCss: function (theme, version) {
        if (link === undefined) {
            link = document.createElement('link');
            document.head.insertBefore(link, document.head.firstChild);
            link.type = 'text/css';
            link.rel = 'stylesheet';
        }
        if (theme === 'bootstrap') {
            link.href = "https://stackpath.bootstrapcdn.com/bootstrap/" + version + "/css/bootstrap.min.css";
        } else {
            link.href = "https://stackpath.bootstrapcdn.com/bootswatch/" + version + "/" + theme + "/bootstrap.min.css";
        }
        return true;
    },
    setPopper: function () {
        if (typeof Popper === undefined || script === undefined) {
            script = document.createElement('script');
            document.head.insertBefore(script, document.head.lastChild);
            script.src = "_content/BlazorStrap/popper.min.js";
        }
        return true;
    }
};

function showPopper(reference, popper, arrow, placement) {
    var thePopper = new Popper(reference, popper,
        {
            placement: placement,
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

