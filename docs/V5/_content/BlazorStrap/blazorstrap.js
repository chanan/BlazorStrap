// noinspection JSUnusedGlobalSymbols
var link;
if (!Element.prototype.matches) {
    Element.prototype.matches =
        Element.prototype.msMatchesSelector ||
        Element.prototype.webkitMatchesSelector;
}

if (!Element.prototype.closest) {
    Element.prototype.closest = function(s) {
        var el = this;

        do {
            if (Element.prototype.matches.call(el, s)) return el;
            el = el.parentElement || el.parentNode;
        } while (el !== null && el.nodeType === 1);
        return null;
    };
}
window.blazorStrap = {
    ToastTimer: function(element, time, timeRemaining, rendered)
    {
        if(rendered === false)
        {
            element.classList.add("showing");
        }
        if(time === 0)
        {
            
            setTimeout(function (){
                element.classList.remove("showing");
            },100)
        }
        if(time !== 0) {

            const dflex = element.querySelector(".d-flex");
            const wrapper = document.createElement("div");
            wrapper.className = "w-100 p-0 m-0 position-relative border-bottom-1 border-dark";
            wrapper.style.top = "-1px";
            const timeEl = document.createElement("div")
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
                timeRemaining = time-timeRemaining;
                console.log(((timeRemaining / time) * 100));
                timeEl.style.width = ((timeRemaining / time) * 100) + "%";
                timeEl.style["transition"] = "linear" + (time - timeRemaining) / 1000 + "s";
                timeEl.style["-webkit-transition"] = "linear " + (time - timeRemaining) / 1000 + "s";
            }
        
            setTimeout(function () {
                element.classList.remove("showing");
                timeEl.style.width = "100%";
            }, 100);
        }
    },
    EventHandlers: [],
    AddEvent: async function (id, name, type, isDocument = false, ignoreChildren = false, filter = "") {
        return new Promise(function (resolve) {
            let element;
            if (isDocument)
                element = document;
            else
                element = document.querySelector("[data-blazorstrap='" + id + "']");
            if (blazorStrap.EventHandlers[id] === undefined) {
                blazorStrap.EventHandlers[id] = {};
            }
            if(blazorStrap.EventHandlers[id][name] === undefined) {
                blazorStrap.EventHandlers[id][name] = {};
            }
            blazorStrap.EventHandlers[id][name][type] = {
                    Callback: function (event) {
                        if(type === "transitionend" && id !== event.target.getAttribute("data-blazorstrap"))
                            return;
                        if (name === "documentDropdown") {
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
                                if (parent.contains(event.target))
                                    return;
                            }
                        }
                        if (filter === "") {
                            // noinspection JSUnresolvedVariable,JSUnresolvedFunction
                            DotNet.invokeMethodAsync("BlazorStrap", "EventCallback", id, name, type, element.classList, blazorStrap.GetEvents(event));
                        } else if (element.getElementsByClassName(filter)) {
                            // noinspection JSUnresolvedVariable,JSUnresolvedFunction
                            DotNet.invokeMethodAsync("BlazorStrap", "EventCallback", id, name, type, element.classList, blazorStrap.GetEvents(event));
                        }
                    }
                }
            
            element.addEventListener(type, blazorStrap.EventHandlers[id][name][type].Callback, false);
            resolve();
        });
    },
    RemoveEvent: async function (id, name, type, isDocument = false) {
        return new Promise(function (resolve) {

            let element;
            if (isDocument)
                element = document;
            else
                element = document.querySelector("[data-blazorstrap='" + id + "']");
            if (blazorStrap.EventHandlers[id] === undefined) return resolve();
            if (name !== "null" && type !== "null") {

                if (blazorStrap.EventHandlers[id][name] === undefined) return resolve();
                if (blazorStrap.EventHandlers[id][name][type] === undefined) return resolve();
                if (blazorStrap.EventHandlers[id][name][type] !== undefined && blazorStrap.EventHandlers[id][name][type] !== null) {
                    if (element !== undefined && element !== null) {
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

            resolve();
        });
    },
    TransitionDidNotStart: async function (element, delay = 200) {
        return new Promise(function (resolve) {
            let handler = function (event) {
                resolve(false);
                clearTimeout(timeout);
            };
            const timeout = setTimeout(function () {
                resolve(true);
                element.removeEventListener("transitionstart",handler, {once:true});
            }, delay);
            element.addEventListener("transitionstart",handler, {once:true});
        }).then((data) => data);
    },
    RemovePopover: async function (element, id) {
        return new Promise(function (resolve, r) {
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
    AddPopover: async function (element, dotNetObjectReference, position, target, offset = "none") {
        function ToPixels(value) {
            if(value.indexOf("rem") !== -1)
            {
                value = value.substring(0,value.indexOf("rem"));
                return value * parseFloat(getComputedStyle(document.documentElement).fontSize);    
            }
            else if(value.indexOf("em") !== -1)
            {
                value = value.substring(0,value.indexOf("em"));
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
                blazorStrap.EventHandlers[id]["Popover"] =
                    Popper.createPopper(target, element, {
                        placement: position,
                        modifiers: [{
                            name: 'offset',
                            options: {
                                offset: [parseInt(ToPixels(offset[0])), parseInt(ToPixels(offset[1]))],
                            },
                        }]
                    });

            } else {
                blazorStrap.EventHandlers[id]["Popover"] =
                    Popper.createPopper(target, element, {
                        placement: position,
                    });
            }
            resolve(true);
        });
    },
    UpdatePopover: async function (element) {
        const id = element.getAttribute("data-blazorstrap");
        return new Promise(function (resolve) {
            setTimeout(async function () {
                await blazorStrap.EventHandlers[id].Popover.update();
                return resolve(element.style.cssText);
            }, 10)
        });
    },
    UpdatePopoverArrow: async function (element, dotNetObjectReference, position, tooltip) {
        const id = element.getAttribute("data-blazorstrap");
        let arrow;
        return new Promise(function (resolve) {
            if (tooltip === false)
                arrow = element.querySelector('.popover-arrow');
            else
                arrow = element.querySelector('.tooltip-arrow');
            position = position.replace("start", "");
            position = position.replace("end", "");
            switch (position) {
                case "top":
                    arrow.style.transform = "translate(" + (element.offsetWidth / 2 - (arrow.offsetWidth / 2)) + "px, 0px)";
                    if (!tooltip)
                        blazorStrap.EventHandlers[id].Popover.setOptions({
                            modifiers: [{
                                name: 'offset',
                                options: { offset: [0, arrow.offsetHeight], },
                            }]
                        });
                    break;
                case "bottom":
                    arrow.style.transform = "translate(" + (element.offsetWidth / 2 - (arrow.offsetWidth / 2)) + "px, 0px)";
                    if (!tooltip)
                        blazorStrap.EventHandlers[id].Popover.setOptions({
                            modifiers: [{
                                name: 'offset',
                                options: { offset: [0, arrow.offsetHeight], },
                            }]
                        });
                    break;
                case "left":
                    arrow.style.transform = "translate(0px, " + (element.offsetHeight / 2 - (arrow.offsetHeight / 2)) + "px)";
                    if (!tooltip)
                        blazorStrap.EventHandlers[id].Popover.setOptions({
                            modifiers: [{
                                name: 'offset',
                                options: { offset: [0, arrow.offsetWidth], },
                            }]
                        });
                    break;
                case "right":
                    arrow.style.transform = "translate(0px, " + (element.offsetHeight / 2 - (arrow.offsetHeight / 2)) + "px)";
                    if (!tooltip)
                        blazorStrap.EventHandlers[id].Popover.setOptions({
                            modifiers: [{
                                name: 'offset',
                                options: { offset: [0, arrow.offsetWidth], },
                            }]
                        });
                    break;
            }
            resolve();
        });
    },
    PeakHeight: async function (element) {
        if(element === null || element === undefined) return;
        return new Promise(function (resolve) {
            element.style.visibility = "hidden";
            element.style.position = "absolute";
            element.style.display = "block";
            setTimeout(function () {
                element.style.display = "";
                element.style.visibility = "";
                element.style.position = ""
            }, 1);
            resolve(element.offsetHeight);
        });
    },
    GetHeight: async function (element) {
        if(element === null || element === undefined) return;
        return new Promise(function (resolve) {
            resolve(element.offsetHeight);
        });
    },
    GetInnerHeight: async function () {
        return new Promise(function (resolve) {
            resolve(window.innerHeight);
        });
    },
    SetBodyStyle: async function (style, value) {
        return new Promise(function (resolve) {
            document.body.style[style] = value;
            resolve();
        });
    },
    GetScrollBarWidth: async function () {
        return new Promise(function (resolve) {
            resolve(window.innerWidth - document.documentElement.clientWidth);
        });
    },
    AddBodyClass: async function (className) {
        return new Promise(function (resolve) {
            document.body.classList.add(className);
            resolve();
        });
    },
    RemoveBodyClass: async function (className) {
        return new Promise(function (resolve) {
            document.body.classList.remove(className);
            resolve();
        });
    },
    SetStyle: async function (element, style, value, delay = 0) {
        if(element === null || element === undefined) return;
        element.style[style] = value;
        return new Promise(function (resolve) {
            if (delay !== 0)
                setTimeout(() => resolve(), delay);
            else
                resolve();
        });
    },
    GetStyle: async function (element) {
        if(element === null || element === undefined) return;
        return element.style.cssText;
    },
    AddAttribute: async function (element, name, value) {
        if(element === null || element === undefined) return;
        return new Promise(function (resolve) {
            element.setAttribute(name, value);
            resolve();
        });
    },
    RemoveAttribute: async function (element, name) {
        if(element === null || element === undefined) return;
        return new Promise(function (resolve) {
            element.removeAttribute(name);
            resolve();
        });
    },
    AddClass: async function (element, className, delay = 0) {
        if(element === null || element === undefined) return;
        element.classList.add(className);
        return new Promise(function (resolve) {
            if (delay !== 0)
                setTimeout(() => resolve(), delay);
            else
                resolve();
        });

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
    AnimateCarousel: async function (id, showEl, hideEl, back) {
        await blazorStrap.CleanupCarousel(showEl,hideEl);
        let callback = function()
        {
            DotNet.invokeMethodAsync("BlazorStrap", "EventFallback", id, "bsCarousel", "transitionend");
        }
        return new Promise(function (resolve) {
            if (back) {
                showEl.classList.add("carousel-item-prev");
                setTimeout(async function () {
                    showEl.classList.add("carousel-item-end");
                    hideEl.classList.add("carousel-item-end");
                    hideEl.addEventListener("transitionend", callback, {once:true})
                    resolve(await blazorStrap.TransitionDidNotStart(showEl));
                }, 10);
            } else {
                showEl.classList.add("carousel-item-next");
                setTimeout(async function () {
                    showEl.classList.add("carousel-item-start");
                    hideEl.classList.add("carousel-item-start");
                    hideEl.addEventListener("transitionend", callback, {once:true})
                    resolve(await blazorStrap.TransitionDidNotStart(showEl));
                }, 10);
            }
        })
    },
    AnimateCollapse: async function(element, shown, id,name,type)
    {
        if(shown)
        {
            let cleanup = function () {
                element.style["height"] = "";
                element.classList.remove("collapsing");
                element.classList.add("collapse");
                element.classList.add("show");
                DotNet.invokeMethodAsync("BlazorStrap", "EventFallback", id, name, type)
            }
            
            let height = await blazorStrap.PeakHeight(element);
            element.classList.remove("collapse");
            element.classList.add("collapsing");
            element.addEventListener("transitionend", cleanup, {once:true});
            setTimeout(async function () {
                element.style["height"] = height + "px";
                if(await blazorStrap.TransitionDidNotStart(element,50)) {
                    cleanup();
                    element.removeEventListener("transitionend", cleanup, {once:true});
                    DotNet.invokeMethodAsync("BlazorStrap", "EventFallback", id, name, type)
                }
            },10);
            
        }
        else
        {
            let cleanup = function () {
                element.classList.remove("collapsing");
                element.classList.add("collapse");
                DotNet.invokeMethodAsync("BlazorStrap", "EventFallback", id, name, type)
            }
                let height = await blazorStrap.GetHeight(element);
            element.style["height"] = height + "px";
            element.classList.remove("collapse");
            element.classList.remove("show");
            element.classList.add("collapsing");
            element.addEventListener("transitionend", cleanup, {once:true});
            
            setTimeout(async function () {
                element.style["height"] = "";
                if(await blazorStrap.TransitionDidNotStart(element,50)) {
                    cleanup();
                    element.removeEventListener("transitionend", cleanup, {once:true});
                    DotNet.invokeMethodAsync("BlazorStrap", "EventFallback", id, name, type);
                }
            },10);
            
            
        }
    },
    RemoveClass: async function (element, className, delay = 0) {
        if(element === null || element === undefined) return;
        element.classList.remove(className);
        return new Promise(function (resolve) {
            if (delay !== 0)
                setTimeout(() => resolve(), delay);
            else
                resolve();
        });
    },
    GetEvents: function (event) {
        return {
            key: event.key,
            target: {
                classList: event.target.classList,
                targetId: event.target.getAttribute("data-blazorstrap-target"),
                childrenId: blazorStrap.GetChildrenIds(event.target),
                dataId: event.target.getAttribute("data-blazorstrap")
            }
        };
    },
    GetChildrenIds: function (target) {
        const children = target.querySelectorAll("[data-blazorstrap]");
        let result = [];
        for (i = 0; i < children; ++i) {
            result.push(children[i].getAttribute("data-blazorstrap"))
        }
        return result;
    },
    setBootstrapCss: function (theme, version) {
        if (link === undefined) {
            let existing = document.querySelectorAll('link[href$="bootstrap.min.css"]')[0];
            if(existing === undefined) {
                link = document.createElement('link');
                document.head.insertBefore(link, document.head.firstChild);
                link.type = 'text/css';
                link.rel = 'stylesheet';
            }
            else
                link = existing;
        }
        if (theme === 'bootstrap') {
            link.href = "https://cdn.jsdelivr.net/npm/bootstrap@" + version + "/dist/css/bootstrap.min.css";
        } else {
            link.href = "https://cdn.jsdelivr.net/npm/bootswatch@" + version + "/dist/" + theme + "/bootstrap.min.css";
        }
        return true;
    },
}

let ResizeFunc;
window.addEventListener("resize", function () {
    clearTimeout(ResizeFunc);
    ResizeFunc = setTimeout(function () {
        // noinspection JSUnresolvedVariable,JSUnresolvedFunction
        DotNet.invokeMethodAsync("BlazorStrap", "ResizeComplete", document.documentElement.clientWidth);
    }, 500);
});
