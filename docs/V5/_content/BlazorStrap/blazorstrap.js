// Release 5.0
// noinspection JSUnusedGlobalSymbols
let link;
let navbarShown = false;

if (!Element.prototype.matches) {
    Element.prototype.matches =
        Element.prototype.msMatchesSelector ||
        Element.prototype.webkitMatchesSelector;
}

if (!Element.prototype.closest) {
    Element.prototype.closest = function (s) {
        var el = this;

        do {
            if (Element.prototype.matches.call(el, s)) return el;
            el = el.parentElement || el.parentNode;
        } while (el !== null && el.nodeType === 1);
        return null;
    };
}

window.blazorStrap = {
    CleanUpEvents: function () {
        Object.keys(blazorStrap.EventHandlers).forEach(key => {
            if (document.querySelector("[data-blazorstrap='" + key + "']") === null) {
                Object.keys(blazorStrap.EventHandlers[key]).forEach(name => {
                    Object.keys(blazorStrap.EventHandlers[key][name]).forEach(type => {
                        try {
                            window.removeEventListener(type, blazorStrap.EventHandlers[key][name][type].Callback, false);
                        } catch {}
                    });
                });
                delete blazorStrap.EventHandlers[key];
            }
        });
    },
    BlurAll: function () {
        var tmp = document.createElement("input");
        tmp.position = "absolute";
        tmp.top = -500;
        document.body.appendChild(tmp);
        tmp.focus();
        document.body.removeChild(tmp);
    },
    AddAttribute: async function (element, name, value) {
        if (element === null || element === undefined) return;
        return new Promise(function (resolve) {
            element.setAttribute(name, value);
            resolve();
        });
    },
    AddBodyClass: async function (className) {
        return new Promise(function (resolve) {
            document.body.classList.add(className);
            resolve();
        });
    },
    AddClass: async function (element, className, delay = 0) {
        if (element === null || element === undefined) return;
        element.classList.add(className);
        return new Promise(function (resolve) {
            if (delay !== 0) setTimeout(() => resolve(), delay); else resolve();
        });
    },
    AddDocumentEvent: async function (objRef, id, name, type, ignoreChildren = false, filter = "") {
        return new Promise(function (resolve) {
            blazorStrap.AddEventInternal(objRef, document, id, name, type, ignoreChildren, filter);
            resolve();
        });
    },
    AddEventInternal: function (objRef, element, id, name, type, ignoreChildren = false, filter = "") {
        blazorStrap.CleanUpEvents();
        
        let expandedWidth = 0;
        if (type === "resize" && element == document) {
            let navbar = document.querySelector("[class*=navbar-expand]");
            // Gets the expanded size
            if (navbar !== null) {

                if (navbar.classList.contains("navbar-expand") || navbar.classList.contains("navbar-expand-ex")) expandedWidth = 576;
                else if (navbar.classList.contains("navbar-expand-md")) expandedWidth = 768;
                else if (navbar.classList.contains("navbar-expand-lg")) expandedWidth = 992;
                else if (navbar.classList.contains("navbar-expand-xl")) expandedWidth = 1200;
                else if (navbar.classList.contains("navbar-expand-xxl")) expandedWidth = 1400;
            }
            if (expandedWidth > window.innerWidth)
                navbarShown = true;
        }
        if (blazorStrap.EventHandlers[id] === undefined) {
            blazorStrap.EventHandlers[id] = {};
        }

        if (blazorStrap.EventHandlers[id][name] === undefined) {
            blazorStrap.EventHandlers[id][name] = {};
        }

        blazorStrap.EventHandlers[id][name][type] = {
            resizeFunc: function () { }
        }
        blazorStrap.EventHandlers[id][name][type] = {
            Callback: async function (event) {
                try {
                    let resizeFunc;
                    if (type === "resize" && element === document) {
                        clearTimeout(resizeFunc);
                        resizeFunc = setTimeout(function () {
                            if (window.innerWidth < expandedWidth)
                                navbarShown = false;
                            if (window.innerWidth > expandedWidth && navbarShown === false) {
                                navbarShown = true;
                                // noinspection JSUnresolvedVariable,JSUnresolvedFunction
                                try {
                                    var test = objRef.invokeMethodAsync("InteropEventCallback", id, name, type, element.classList, blazorStrap.GetEvents(event));
                                } catch {
                                    // ObjectRef may be gone by time this fires   
                                }
                            }
                        }, 100);
                        return;
                    }
                    if (type === "resize") {
                        clearTimeout(blazorStrap.EventHandlers[id][name][type][resizeFunc]);
                        blazorStrap.EventHandlers[id][name][type][resizeFunc] = setTimeout(function () {
                            objRef.invokeMethodAsync("InteropEventCallback", id, name, type, element.classList, blazorStrap.GetEvents(event));
                        }, 250);
                        return;
                    }
                    if (type === "transitionend" && id !== event.target.getAttribute("data-blazorstrap")) return;

                    if (name === "bsdropdown") {
                        let parent = document.querySelector("[data-blazorstrap='" + id + "']");

                        if (parent !== null) {
                            if (parent.contains(event.target)) {
                                if (event.target.closest(".dropdown-toggle") !== null) {
                                    return;
                                }
                            }
                        }
                    }
                    if (ignoreChildren) {
                        let parent = document.querySelector("[data-blazorstrap='" + id + "']");

                        if (parent !== null) {
                            if (parent.contains(event.target)) return;
                        }
                    }
                    if (filter === "") {
                        objRef.invokeMethodAsync("InteropEventCallback", id, name, type, element.classList, blazorStrap.GetEvents(event));
                    } else if (element.getElementsByClassName(filter)) {
                        // noinspection JSUnresolvedVariable,JSUnresolvedFunction
                        objRef.invokeMethodAsync("InteropEventCallback", id, name, type, element.classList, blazorStrap.GetEvents(event));
                    }
                } catch
                {
                    await blazorStrap.RemoveEventInternal(element, id, name, type);
                }
            }
        };
        if (type === "resize")
            window.addEventListener(type, blazorStrap.EventHandlers[id][name][type].Callback, false);
        else
            element.addEventListener(type, blazorStrap.EventHandlers[id][name][type].Callback, false);
    },
    AddEvent: async function (objRef, id, name, type, isDocument = false, ignoreChildren = false, filter = "") {
        return new Promise(function (resolve) {
            blazorStrap.AddEventInternal(objRef, document.querySelector("[data-blazorstrap='" + id + "']"), id, name, type, ignoreChildren, filter);
            resolve();
        });
    },
    AddPopover: async function (element, position, target, offset = "none") {
        function ToPixels(value) {
            if (value.indexOf("rem") !== -1) {
                value = value.substring(0, value.indexOf("rem"));
                return value * parseFloat(getComputedStyle(document.documentElement).fontSize);
            } else if (value.indexOf("em") !== -1) {
                value = value.substring(0, value.indexOf("em"));
                return value * parseFloat(getComputedStyle(element).fontSize);
            }

            return value;
        }

        return new Promise(function (resolve) {
            const id = element.getAttribute("data-blazorstrap");
            target = document.querySelector("[data-blazorstrap='" + target + "']");

            if (blazorStrap.EventHandlers[id] === undefined) {
                blazorStrap.EventHandlers[id] = {};
            }

            if (offset !== "none") {
                offset = offset.split(",");
                blazorStrap.EventHandlers[id]["Popover"] = Popper.createPopper(target, element, {
                    placement: position,
                    modifiers: [{
                        name: 'offset',
                        options: {
                            offset: [parseInt(ToPixels(offset[0])), parseInt(ToPixels(offset[1]))]
                        }
                    }]
                });
            } else {
                blazorStrap.EventHandlers[id]["Popover"] = Popper.createPopper(target, element, {
                    placement: position
                });
            }

            resolve(true);
        });
    },
    AnimateCarousel: async function (objref, id, showEl, hideEl, back) {
        await blazorStrap.CleanupCarousel(showEl, hideEl);

        let callback = function () {
            objref.invokeMethodAsync("InteropEventCallback", id, "bscarousel", "transitionend");
        };

        return new Promise(function (resolve) {
            if (back) {
                showEl.classList.add("carousel-item-prev");
                setTimeout(async function () {
                    showEl.classList.add("carousel-item-end");
                    hideEl.classList.add("carousel-item-end");
                    hideEl.addEventListener("transitionend", callback, {
                        once: true
                    });
                    resolve((await blazorStrap.TransitionDidNotStart(showEl)));
                }, 10);
            } else {
                showEl.classList.add("carousel-item-next");
                setTimeout(async function () {
                    showEl.classList.add("carousel-item-start");
                    hideEl.classList.add("carousel-item-start");
                    hideEl.addEventListener("transitionend", callback, {
                        once: true
                    });
                    resolve((await blazorStrap.TransitionDidNotStart(showEl)));
                }, 10);
            }
        });
    },
    AnimateCollapse: async function (objRef, element, id, shown, name) {
        if (shown) {
            let cleanup = function () {
                element.style["height"] = "";
                element.classList.remove("collapsing");
                element.classList.add("collapse");
                element.classList.add("show");
                objRef.invokeMethodAsync("InteropEventCallback", id, name, "transitionend", null, null);
            };

            let height = await blazorStrap.PeakHeight(element);
            element.classList.remove("collapse");
            element.classList.add("collapsing");
            element.addEventListener("transitionend", cleanup, {
                once: true
            });
            setTimeout(async function () {
                element.style["height"] = height + "px";

                if (await blazorStrap.TransitionDidNotStart(element, 50)) {
                    cleanup();
                    element.removeEventListener("transitionend", cleanup, {
                        once: true
                    });
                    objRef.invokeMethodAsync("InteropEventCallback", id, name, "transitionend", null, null);
                }
            }, 10);
        } else {
            let cleanup = function () {
                element.classList.remove("collapsing");
                element.classList.add("collapse");
                objRef.invokeMethodAsync("InteropEventCallback", id, name, "transitionend", null, null);
            };

            let height = await blazorStrap.GetHeight(element);
            element.style["height"] = height + "px";
            element.classList.remove("collapse");
            element.classList.remove("show");
            element.classList.add("collapsing");
            element.addEventListener("transitionend", cleanup, {
                once: true
            });
            setTimeout(async function () {
                element.style["height"] = "";

                if (await blazorStrap.TransitionDidNotStart(element, 50)) {
                    cleanup();
                    element.removeEventListener("transitionend", cleanup, { once: true });
                    objRef.invokeMethodAsync("InteropEventCallback", id, name, "transitionend", null, null);
                }
            }, 10);
        }
    },
    CleanupCarousel: async function (showEl, hideEl) {
        //Cleans up any rogue calls
        return new Promise(function (resolve) {
            hideEl.classList.remove("carousel-item-end");
            hideEl.classList.remove("carousel-item-prev");
            hideEl.classList.remove("carousel-item-next");
            hideEl.classList.remove("carousel-item-start");
            showEl.classList.remove("carousel-item-end");
            showEl.classList.remove("carousel-item-prev");
            showEl.classList.remove("carousel-item-next");
            showEl.classList.remove("carousel-item-start");
            resolve();
        });
    },
    EventHandlers: [],
    GetChildrenIds: function (target) { // Likely can be removed
        const children = target.querySelectorAll("[data-blazorstrap]");
        let result = [];

        for (i = 0; i < children; ++i) {
            result.push(children[i].getAttribute("data-blazorstrap"));
        }
        return result;
    },
    GetEvents: function (event) {   // Likely can be removed
        try {
            return {
                key: event.key,
                clientWidth: document.documentElement.clientWidth,
                target: {
                    classList: event.target.classList,
                    targetId: event.target.getAttribute("data-blazorstrap-target"),
                    childrenId: blazorStrap.GetChildrenIds(event.target),
                    dataId: event.target.getAttribute("data-blazorstrap")
                }
            };
        } catch {
            return {
                key: event.key,
                clientWidth: document.documentElement.clientWidth,
            }
        }
    },
    GetHeight: async function (element) {
        if (element === null || element === undefined) return;
        return new Promise(function (resolve) {
            resolve(element.offsetHeight);
        });
    },
    GetWidth: async function (element) {
        if (element === null || element === undefined) return 0;
        return new Promise(function (resolve) {
            resolve(element.offsetWidth);
        });
    },
    GetWindowInnerHeight: async function () {
        return new Promise(function (resolve) {
            resolve(window.innerHeight);
        });
    },
    GetScrollBarWidth: async function () {
        return new Promise(function (resolve) {
            resolve(window.innerWidth - document.documentElement.clientWidth);
        });
    },
    GetStyle: async function (element) {
        if (element === null || element === undefined) return;
        return element.style.cssText;
    },
    PeakHeight: async function (element) {
        if (element === null || element === undefined) return;
        var oldStyle = element.style;
        return new Promise(function (resolve) {
            element.style.visibility = "hidden";
            element.style.position = "absolute";
            element.style.display = "block";
            setTimeout(function () {
                var height = element.offsetHeight;
                element.style = oldStyle;
                resolve(height);
            }, 1);
        });
    },
    RemoveAttribute: async function (element, name) { //Likely can be removed
        if (element === null || element === undefined) return;
        return new Promise(function (resolve) {
            element.removeAttribute(name);
            resolve();
        });
    },
    RemoveBodyClass: async function (className) {
        return new Promise(function (resolve) {
            document.body.classList.remove(className);
            resolve();
        });
    },
    RemoveClass: async function (element, className, delay = 0) {
        if (element === null || element === undefined) return;
        element.classList.remove(className);
        return new Promise(function (resolve) {
            if (delay !== 0) setTimeout(() => resolve(), delay); else resolve();
        });
    },
    RemoveDocumentEvent: async function (id, name, type) {
        return new Promise(function (resolve) {
            blazorStrap.RemoveEventInternal(document, id, name, type);
            resolve();
        });
    },
    RemoveEvent: async function (id, name, type) { //Split Document out to its own function
        return new Promise(function (resolve) {
            blazorStrap.RemoveEventInternal(document.querySelector("[data-blazorstrap='" + id + "']"), id, name, type);
            resolve();
        });
    },
    RemoveEventInternal: function (element, id, name, type) {
        blazorStrap.CleanUpEvents();
        if (blazorStrap.EventHandlers[id] === undefined) return;
        
        if (name !== "null" && type !== "null") {
            if (blazorStrap.EventHandlers[id][name] === undefined) return;
            if (blazorStrap.EventHandlers[id][name][type] === undefined) return;

            if (blazorStrap.EventHandlers[id][name][type] !== undefined && blazorStrap.EventHandlers[id][name][type] !== null) {
                if (element !== undefined && element !== null) {
                    if (type === "resize")
                        window.removeEventListener(type, blazorStrap.EventHandlers[id][name][type].Callback, false);
                    else
                        element.removeEventListener(type, blazorStrap.EventHandlers[id][name][type].Callback, false);
                }
                delete blazorStrap.EventHandlers[id][name][type];
            }
            if (Object.keys(blazorStrap.EventHandlers[id][name]).length === 0) {
                delete blazorStrap.EventHandlers[id][name];
            }
        }
        if (Object.keys(blazorStrap.EventHandlers[id]).length === 0) {
            delete blazorStrap.EventHandlers[id];
        }
    },
    RemovePopover: async function (element, id) {
        return new Promise(function (resolve) {
            if (blazorStrap.EventHandlers[id] === undefined) return resolve();

            if (blazorStrap.EventHandlers[id]["Popover"] !== undefined) {
                blazorStrap.EventHandlers[id].Popover.destroy();
                delete blazorStrap.EventHandlers[id].Popover;
                resolve();
            }

            if (Object.keys(blazorStrap.EventHandlers[id]).length === 0) {
                delete blazorStrap.EventHandlers[id];
            }
        });
    },
    SetBodyStyle: async function (style, value) {
        return new Promise(function (resolve) {
            document.body.style[style] = value;
            resolve();
        });
    },
    SetBootstrapCss: function (theme, version) {
        if (link === undefined) {
            let existing = document.querySelectorAll('link[href$="bootstrap.min.css"]')[0];

            if (existing === undefined) {
                link = document.createElement('link');
                document.head.insertBefore(link, document.head.firstChild);
                link.type = 'text/css';
                link.rel = 'stylesheet';
            } else link = existing;
        }

        if (theme === 'bootstrap') {
            link.href = "https://cdn.jsdelivr.net/npm/bootstrap@" + version + "/dist/css/bootstrap.min.css";
        } else {
            link.href = "https://cdn.jsdelivr.net/npm/bootswatch@" + version + "/dist/" + theme + "/bootstrap.min.css";
        }

        return true;
    },
    SetStyle: async function (element, style, value, delay = 0) {
        if (element === null || element === undefined) return;
        element.style[style] = value;
        return new Promise(function (resolve) {
            if (delay !== 0) setTimeout(() => resolve(), delay); else resolve();
        });
    },
    ToastTimer: function (element, time, timeRemaining, rendered) {
        if (rendered === false) {
            element.classList.add("showing");
        }

        if (time === 0) {
            setTimeout(function () {
                element.classList.remove("showing");
            }, 100);
        }

        if (time !== 0) {
            const dflex = element.querySelector(".d-flex");
            const wrapper = document.createElement("div");
            wrapper.className = "w-100 p-0 m-0 position-relative border-bottom-1 border-dark";
            wrapper.style.top = "-1px";
            const timeEl = document.createElement("div");
            wrapper.appendChild(timeEl);
            element.insertBefore(wrapper, dflex);
            timeEl.classList.add("bg-dark");
            timeEl.style.height = "4px";
            timeEl.style.opacity = ".4";

            if (timeRemaining === 0) {
                timeEl.style.width = "0";
                timeEl.style["transition"] = "linear " + (time - timeRemaining) / 1000 + "s";
                timeEl.style["-webkit-transition"] = "linear " + (time - timeRemaining) / 1000 + "s";
            } else {
                timeRemaining = time - timeRemaining;
                timeEl.style.width = timeRemaining / time * 100 + "%";
                timeEl.style["transition"] = "linear" + (time - timeRemaining) / 1000 + "s";
                timeEl.style["-webkit-transition"] = "linear " + (time - timeRemaining) / 1000 + "s";
            }

            setTimeout(function () {
                element.classList.remove("showing");
                timeEl.style.width = "100%";
            }, 100);
        }
    },
    TransitionDidNotStart: async function (element, delay = 200) {
        return new Promise(function (resolve) {
            let handler = function () {
                resolve(false);
                clearTimeout(timeout);
            };

            const timeout = setTimeout(function () {
                resolve(true);
                element.removeEventListener("transitionstart", handler, {
                    once: true
                });
            }, delay);
            element.addEventListener("transitionstart", handler, {
                once: true
            });
        }).then(data => data);
    },
    UpdatePopover: async function (element) {
        const id = element.getAttribute("data-blazorstrap");
        return new Promise(function (resolve) {
            setTimeout(async function () {
                await blazorStrap.EventHandlers[id].Popover.update();
                return resolve(element.style.cssText);
            }, 10);
        });
    },
    UpdatePopoverArrow: async function (element, position, tooltip) {
        const id = element.getAttribute("data-blazorstrap");
        let arrow;
        return new Promise(function (resolve) {
            if (tooltip === false) arrow = element.querySelector('.popover-arrow'); else arrow = element.querySelector('.tooltip-arrow');
            position = position.replace("start", "");
            position = position.replace("end", "");

            switch (position) {
                case "top":
                    arrow.style.transform = "translate(" + (element.offsetWidth / 2 - arrow.offsetWidth / 2) + "px, 0px)";
                    if (!tooltip) blazorStrap.EventHandlers[id].Popover.setOptions({
                        modifiers: [{
                            name: 'offset',
                            options: {
                                offset: [0, arrow.offsetHeight]
                            }
                        }]
                    });
                    break;

                case "bottom":
                    arrow.style.transform = "translate(" + (element.offsetWidth / 2 - arrow.offsetWidth / 2) + "px, 0px)";
                    if (!tooltip) blazorStrap.EventHandlers[id].Popover.setOptions({
                        modifiers: [{
                            name: 'offset',
                            options: {
                                offset: [0, arrow.offsetHeight]
                            }
                        }]
                    });
                    break;

                case "left":
                    arrow.style.transform = "translate(0px, " + (element.offsetHeight / 2 - arrow.offsetHeight / 2) + "px)";
                    if (!tooltip) blazorStrap.EventHandlers[id].Popover.setOptions({
                        modifiers: [{
                            name: 'offset',
                            options: {
                                offset: [0, arrow.offsetWidth]
                            }
                        }]
                    });
                    break;

                case "right":
                    arrow.style.transform = "translate(0px, " + (element.offsetHeight / 2 - arrow.offsetHeight / 2) + "px)";
                    if (!tooltip) blazorStrap.EventHandlers[id].Popover.setOptions({
                        modifiers: [{
                            name: 'offset',
                            options: {
                                offset: [0, arrow.offsetWidth]
                            }
                        }]
                    });
                    break;
            }

            resolve();
        });
    }
}