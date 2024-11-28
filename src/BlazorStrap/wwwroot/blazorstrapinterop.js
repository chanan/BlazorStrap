// { id = [{ creating element, eventtype, func}] }
let eventCallbacks = [];
let documentEventsSet = false;
let docuemntEventId = [];
let link;
// Common
export async function checkBackdrops(dotnet) {
    var backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
        var openModals = document.querySelectorAll('.modal.show');
        //Checks to see if any other modal is open if so do not remove the backdrop
        if (openModals.length == 0) {

            await waitForTransitionEnd(backdrop, function () {
                backdrop.classList.remove("show");
            });
            await dotnet.invokeMethodAsync('RemoveBackdropAsync');
        }
    }
    backdrop = document.querySelector('.offcanvas-backdrop');
    if (backdrop) {
        var openOffcanvas = document.querySelectorAll('.offcanvas.show');
        //Checks to see if any other offcanvas is open if so do not remove the backdrop
        if (openOffcanvas.length == 0) {
            await waitForTransitionEnd(backdrop, function () {
                backdrop.classList.remove("show");
            });
            await dotnet.invokeMethodAsync('RemoveOffCanvasBackdropAsync');
        }
    }
}
export async function removeRougeEvents() {
    // remove any event and event handler that no longer exists you will have to loop because we need to unregister the event
    var documentCallbacks = eventCallbacks.find(x => x.id == "document");
    if (documentCallbacks) {
        docuemntEventId.forEach(function (event) {
            if (!document.querySelector('[data-blazorstrap="' + event.creator + '"]')) {
                docuemntEventId = docuemntEventId.filter(x => x.creator !== event.creator);
            }
        });
        documentCallbacks.events = documentCallbacks.events.filter(x => x.creator !== event.creator);
    }
    //remove any event that id no longer exists
    eventCallbacks = eventCallbacks.filter(x => document.querySelector('[data-blazorstrap="' + x.id + '"]'));
}
export async function addDocumentEvent(eventName, creator, dotnet, ignoreChildren) {
    if (!documentEventsSet) setupDocumentEvents(dotnet);
    if (eventName == "" || eventName == "sync" || eventName == "hide" || eventName == "show") return;
    docuemntEventId.push({ eventtype: eventName, creator: creator });
}
export async function removeDocumentEvent(eventName, creator) {
    docuemntEventId = docuemntEventId.filter(x => x.creator !== creator && x.eventtype !== eventName);
}
export async function addEvent(targetId, creator, eventName, dotnet, ignoreChildren) {
    if (eventName == "" || eventName == "sync" || eventName == "hide" || eventName == "show") return;
    var target = document.querySelector('[data-blazorstrap="' + targetId + '"]');
    if (target) {
        let eventFunc = debounce(function (e) {
            if (ignoreChildren && e.target.getAttribute("data-blazorstrap") != targetId) return;
            dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", targetId, eventName, null);
        }, 150);
        //add the eventfunc to eventcallbacks so we can remove it later
        let callback = eventCallbacks.find(x => x.id == targetId);
        if (callback)
            callback.events.push({ creator: creator, eventtype: eventName, func: eventFunc });
        else
            eventCallbacks.push({ id: targetId, events: [{ creator: creator, eventtype: eventName, func: eventFunc }] });

        //observe the element so we can remove the event if it is removed
        onElementRemoved(target, function () {
            target.removeEventListener(eventName, eventFunc);
            eventCallbacks = eventCallbacks.filter(x => x.id !== targetId);
        });

        target.addEventListener(eventName, eventFunc);
    }
    //remove any event that id no longer exists
    eventCallbacks = eventCallbacks.filter(x => document.querySelector('[data-blazorstrap="' + x.id + '"]'));
}
export async function removeEvent(targetId, creator, eventName) {
    var target = document.querySelector('[data-blazorstrap="' + targetId + '"]');
    //get the eventfunc from eventcallbacks
    let callback = eventCallbacks.find(x => x.id == targetId);
    if (callback) {
        let eventFunc = callback.events.find(x => x.creator == creator && x.eventtype == eventName);
        callback.events = callback.events.filter(x => x.creator !== creator && x.eventtype !== eventName);
        if (target) {
            target.removeEventListener(eventName, eventFunc);
        }
    }
}

//Modals
export async function showModal(modal, dotnet) {
    if (!modal) return null;
    var backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
        await waitForNextFrame();
        backdrop.classList.add("show");
        dotnet.invokeMethodAsync('BackdropShownAsync');
    }


    //bind the escape key
    let keydown = function (e) {
        if (e.key === "Escape") {
            hideModalEvents();
        }
    }
    // observe the modal for removal
    onElementRemoved(modal, function () {
        document.removeEventListener('keydown', keydown);
    });

    // add the escape key listener
    if (modal.getAttribute("data-bs-keyboard") == "true") {
        document.addEventListener('keydown', keydown);
    }

    modal.addEventListener('click', async function (e) {
        if (e.target !== e.currentTarget) return;
        hideModalEvents();
    });

    // get the scroll bar width and set the padding on the body
    if (modal.getAttribute("data-bs-allowscroll") != "true") {
        var scrollBarWidth = window.innerWidth - document.documentElement.clientWidth;
        if (scrollBarWidth > 0) {
            document.body.style.overflow = "hidden";
            document.body.style.paddingRight = scrollBarWidth + "px";
        }
    }

    modal.style.display = "block";

    modal.setAttribute("aria-modal", "true");
    modal.setAttribute("aria-hidden", "false");
    document.body.classList.add("modal-open");
    await waitForNextFrame();
    var openModals = document.querySelectorAll('.modal.show');
    if (openModals.length > 0) {
        await openModals.forEach(async openModal => {
            //if data-bs-manual is true then do not hide the modal 
            if (openModal.getAttribute("data-bs-manual") != "true") {
                if (openModal.getAttribute("data-blazorstrap") != modal.getAttribute("data-blazorstrap")) {
                    dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", openModal.getAttribute("data-blazorstrap"), "hide", "");
                    openModal.classList.remove("show");
                }
            }
        });
        await new Promise(resolve => setTimeout(resolve, 75));
    }

    await waitForTransitionEnd(modal, function () {
        modal.classList.add("show");
    });
    modal.focus();

    return {
        ClassList: modal.classList.value,
        Styles: modal.style.cssText,
        Aria: getAriaAttributes(modal),
    };

    async function hideModalEvents() {
        if (modal.getAttribute("data-bs-backdrop") == "static") {
            await waitForTransitionEnd(modal, function () {
                modal.classList.add("modal-static");
            });
            modal.classList.remove("modal-static");
        }
        else {
            dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", modal.getAttribute("data-blazorstrap"), "hide", "");
        }
    }
}
export async function hideModal(modal, dotnet) {
    if (!modal) return null;

    await waitForTransitionEnd(modal, function () {
        modal.classList.remove("show");
        modal.setAttribute("aria-modal", "false");
        modal.setAttribute("aria-hidden", "true");
        document.body.classList.remove("modal-open");

    }, 150);

    modal.style.display = "none";

    var openModals = document.querySelectorAll('.modal.show');
    if (openModals.length == 0) {
        document.body.style.overflow = "";
        document.body.style.paddingRight = "";
    }
    var backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
        var openModals = document.querySelectorAll('.modal.show:not([data-bs-backdrop="false"])');
        //Checks to see if any other modal is open if so do not remove the backdrop
        if (openModals.length == 0) {
            await waitForTransitionEnd(backdrop, function () {
                backdrop.classList.remove("show");
            }, 150);

            await dotnet.invokeMethodAsync('RemoveBackdropAsync');
        }
    }
    return {
        ClassList: modal.classList.value,
        Styles: modal.style.cssText,
        Aria: getAriaAttributes(modal),
    };
}

//Offcanvas
export async function showOffcanvas(offcanvas, dotnet) {
    if (!offcanvas) return null;
    var backdrop = document.querySelector('.offcanvas-backdrop');
    if (backdrop) {
        await waitForNextFrame();
        backdrop.classList.add("show");
        dotnet.invokeMethodAsync('OffCanvasBackdropShownAsync');
    }
    //bind the escape key
    let keydown = function (e) {
        if (e.key === "Escape") {
            hideOffcanvasEvents();
        }
    }
    // observe the offcanvas for removal
    onElementRemoved(offcanvas, function () {
        document.removeEventListener('keydown', keydown);
    });

    // add the escape key listener
    if (offcanvas.getAttribute("data-bs-keyboard") == "true") {
        document.addEventListener('keydown', keydown);
    }

    if (backdrop) {
        backdrop.addEventListener('click', async function (e) {
            if (e.target !== e.currentTarget) return;
            hideOffcanvasEvents();
        });
    }

    // get the scroll bar width and set the padding on the body
    if (offcanvas.getAttribute("data-bs-allowscroll") != "true") {
        var scrollBarWidth = window.innerWidth - document.documentElement.clientWidth;
        if (scrollBarWidth > 0) {
            document.body.style.overflow = "hidden";
            document.body.style.paddingRight = scrollBarWidth + "px";
        }
    }
    offcanvas.style.visibility = "visible";
    offcanvas.setAttribute("aria-modal", "true");
    offcanvas.setAttribute("aria-hidden", "false");
    document.body.classList.add("offcanvas-open");
    await waitForNextFrame();
    var openOffcanvas = document.querySelectorAll('.offcanvas.show');
    if (openOffcanvas.length > 0) {
        await openOffcanvas.forEach(async openOffcanvas => {
            //if data-bs-manual is true then do not hide the offcanvas
            if (openOffcanvas.getAttribute("data-bs-manual") != "true") {
                dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", openOffcanvas.getAttribute("data-blazorstrap"), "hide", "");
                openOffcanvas.classList.remove("show");
            }
        });
        await new Promise(resolve => setTimeout(resolve, 75));
    }

    await waitForTransitionEnd(offcanvas, function () {
        offcanvas.classList.add("show");
    });

    offcanvas.focus();
    return {
        ClassList: offcanvas.classList.value,
        Styles: offcanvas.style.cssText,
        Aria: getAriaAttributes(offcanvas),
    };

    async function hideOffcanvasEvents() {
        if (offcanvas.getAttribute("data-bs-backdrop") == "static") {
            await waitForTransitionEnd(offcanvas, function () {
                offcanvas.classList.add("offcanvas-static");
            });
            offcanvas.classList.remove("offcanvas-static");
        }
        else {
            dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", offcanvas.getAttribute("data-blazorstrap"), "hide", "");
        }
    }
}
export async function hideOffcanvas(offcanvas, dotnet) {
    if (!offcanvas) return null;
    await waitForTransitionEnd(offcanvas, function () {
        offcanvas.classList.remove("show");
        offcanvas.setAttribute("aria-modal", "false");
        offcanvas.setAttribute("aria-hidden", "true");
        document.body.classList.remove("offcanvas-open");
    });
    offcanvas.style.visibility = "hidden";

    var openOffcanvas = document.querySelectorAll('.offcanvas.show');
    if (openOffcanvas.length == 0) {
        document.body.style.overflow = "";
        document.body.style.paddingRight = "";
    }
    var backdrop = document.querySelector('.offcanvas-backdrop');
    if (backdrop) {
        var openOffcanvas = document.querySelectorAll('.offcanvas.show:not([data-bs-backdrop="false"])');
        //Checks to see if any other offcanvas is open if so do not remove the backdrop
        if (openOffcanvas.length == 0) {
            await waitForTransitionEnd(backdrop, function () {
                backdrop.classList.remove("show");
            },150);
            await dotnet.invokeMethodAsync('RemoveOffCanvasBackdropAsync');
        }
    }
    //grab add child .tooltip and .popover elements and remove them
    var childElements = offcanvas.querySelectorAll(".tooltip,.popover");
    if (childElements) {
        childElements.forEach(function (childElement) {
            var id = childElement.getAttribute("data-blazorstrap");
            dotnet.invokeMethodAsync('InvokeEventAsync', 'javascript', id, "hide", null);
        });
    }
    return {
        ClassList: offcanvas.classList.value,
        Styles: offcanvas.style.cssText,
        Aria: getAriaAttributes(offcanvas),
    };
}

//Dropdowns
export async function showDropdown(dropdown, isPopper, targetId, placement, dotnet, offsetX, offsetY, options = {}) {
    if (!dropdown) return null;

    if (isPopper) {
        //using popper.js setup the tooltip
        var target = document.querySelector('[data-blazorstrap="' + targetId + '"]');
        if (target) {
            var popper = Popper.createPopper(target, dropdown, {

                placement: placement,
                modifiers: [
                    {
                        name: 'offset',
                        options: {
                            offset: [offsetX, offsetY],
                        },
                    }
                ],
                ...options
            });
        }
    }

    let documentClick = function (e) {
        if (!dropdown.contains(e.target)) {
            dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", dropdown.getAttribute("data-blazorstrap"), "click", "");
        }
    };
    onShowClassRemoved(dropdown, function () {
        document.removeEventListener('click', documentClick);
    });
    await waitForTransitionEnd(dropdown, function () {
        dropdown.classList.add("show");
    });

    document.addEventListener('click', documentClick);
    return {
        ClassList: dropdown.classList.value,
        Styles: dropdown.style.cssText,
        Aria: getAriaAttributes(dropdown),
    };
}
export async function hideDropdown(dropdown, dotnet) {
    if (!dropdown) return null;
    await waitForTransitionEnd(dropdown, function () {
        dropdown.classList.remove("show");
    });
    return {
        ClassList: dropdown.classList.value,
        Styles: dropdown.style.cssText,
        Aria: getAriaAttributes(dropdown),
    };
}

//Tooltips
export async function showTooltip(tooltip, placement, targetId, dotnet, options = {}) {
    if (!tooltip) return null;
    //using popper.js setup the tooltip
    //get the arrow element
    var arrow = tooltip.querySelector(".tooltip-arrow");
    var offset = [0, 0];
    if (tooltip.classList.contains("popover")) {
        arrow = tooltip.querySelector(".popover-arrow");
        // if placement contains top or bottom
        if (placement.indexOf("top") > -1 || placement.indexOf("bottom") > -1) {
            offset = [0, arrow.offsetHeight];
        }
        else {
            offset = [0, arrow.offsetWidth];
        }
    }
    var target = document.querySelector('[data-blazorstrap="' + targetId + '"]');
    if (target) {
        var popper = Popper.createPopper(target, tooltip, {
            ...options,
            placement: placement,
            modifiers: [
                {
                    name: 'arrow',
                    options: {
                        element: arrow,
                    },
                },
                {
                    name: 'offset',
                    options: {
                        offset: offset,
                    },
                }
            ],
        });
        await waitForTransitionEnd(tooltip, function () {
            tooltip.classList.add("show");
        });
    }
    return {
        ClassList: tooltip.classList.value,
        Styles: tooltip.style.cssText,
        Aria: getAriaAttributes(tooltip),
    };
}
export async function hideTooltip(tooltip, dotnet) {
    if (!tooltip) return null;
    await waitForTransitionEnd(tooltip, function () {
        tooltip.classList.remove("show");
    }, 150);
    return {
        ClassList: tooltip.classList.value,
        Styles: tooltip.style.cssText,
        Aria: getAriaAttributes(tooltip),
    };
}

export async function showAccordion(accordion, accordionToHide, dotnet) {
    let hideResult = null;
    if (!accordion) return null;
    let showResult = showCollapse(accordion, false, dotnet);
    if (accordionToHide) {
        hideResult = hideCollapse(accordionToHide, false, dotnet);
    }
    await Promise.all([showResult, hideResult]);
    return [showResult, hideResult];
}

//Collapses
export async function showCollapse(collapse, horizontal, dotnet) {
    if (!collapse) return null;
    document.querySelectorAll('[data-blazorstrap-target="' + collapse.getAttribute('data-blazorstrap') + '"]').forEach(caller => {
        if (caller) {
            caller.setAttribute('aria-expanded', true);
            caller.classList.remove('collapsed');
        }
    });


    if (horizontal) {
        collapse.style.width = "0";
    } else {
        collapse.style.height = "0";
    }
    //  collapse.style.display = "block";
    await waitForNextFrame();
    collapse.classList.remove("collapse");
    await waitForTransitionEnd(collapse, function () {
        collapse.classList.add("collapsing");

        let actualSize = (horizontal ? collapse.scrollWidth : collapse.scrollHeight) + "px";

        if (horizontal) {
            collapse.style.width = actualSize;
        } else {
            collapse.style.height = actualSize;
        }
    });

    collapse.classList.remove("collapsing");
    collapse.classList.add("collapse");
    collapse.classList.add("show");
    if (horizontal) {
        collapse.style.width = "";
    }
    else {
        collapse.style.height = "";
    }
    return {
        ClassList: collapse.classList.value,
        Styles: collapse.style.cssText,
        Aria: getAriaAttributes(collapse),
    };
}
export async function hideCollapse(collapse, horizontal, dotnet) {
    if (!collapse) return null;
    document.querySelectorAll('[data-blazorstrap-target="' + collapse.getAttribute('data-blazorstrap') + '"]').forEach(caller => {
        if (caller) {
            caller.setAttribute('aria-expanded', false);
            caller.classList.add('collapsed');
        }
    });

    var currentSize = (horizontal ? collapse.scrollWidth : collapse.scrollHeight) + "px";

    if (horizontal) {
        collapse.style.width = currentSize;
    } else {
        collapse.style.height = currentSize;
    }

    await waitForNextFrame(); // Wait for the next frame to ensure the DOM updates
    collapse.classList.remove("collapse");

    await waitForTransitionEnd(collapse, async function () {
        collapse.classList.add("collapsing");

        await waitForNextFrame(); // Wait for the next frame to ensure the DOM updates

        if (horizontal) {
            collapse.style.width = "";
        } else {
            collapse.style.height = "";
        }
    });

    collapse.style.display = "";
    collapse.classList.remove("collapsing");
    collapse.classList.remove("show");
    collapse.classList.add("collapse");
    collapse.style.height = "";

    return {
        ClassList: collapse.classList.value,
        Styles: collapse.style.cssText,
        Aria: getAriaAttributes(collapse),
    };
}

//Toaster
export async function toastTimer(element, time, timeRemaining, rendered) {
    if (rendered === false) {
        element.classList.add("showing");
    }

    if (time === 0) {

        await new Promise(resolve => setTimeout(function () {
            element.classList.remove("showing");
            resolve();
        }, 100));
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
        await new Promise(resolve => setTimeout(function () {
            element.classList.remove("showing");
            timeEl.style.width = "100%";
            resolve();
        }, 100));
    }
}
//TODO: Update these direct ports
async function cleanupCarousel(showEl, hideEl) {
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
}
export async function animateCarousel(id, showEl, hideEl, back, v4, dotnet) {
    await cleanupCarousel(showEl, hideEl);

    let callback = function () {
        dotnet.invokeMethodAsync("InvokeEventAsync", "javascript", id, "transitionend", null);
    };

    return new Promise(function (resolve) {
        if (back) {
            showEl.classList.add("carousel-item-prev");
            setTimeout(async function () {
                resolve((await waitForTransitionEnd(showEl, function () {
                    if (v4) {
                        showEl.classList.add("carousel-item-right");
                        hideEl.classList.add("carousel-item-right");
                    }
                    else {
                        showEl.classList.add("carousel-item-end");
                        hideEl.classList.add("carousel-item-end");
                    }
                    hideEl.addEventListener("transitionend", callback, {
                        once: true
                    });
                })));
            }, 10);
        } else {
            showEl.classList.add("carousel-item-next");
            setTimeout(async function () {
                resolve((await waitForTransitionEnd(showEl, function () {
                    if (v4) {
                        showEl.classList.add("carousel-item-left");
                        hideEl.classList.add("carousel-item-left");
                    }
                    else {
                        showEl.classList.add("carousel-item-start");
                        hideEl.classList.add("carousel-item-start");
                    }
                    hideEl.addEventListener("transitionend", callback, {
                        once: true
                    });
                })));
            }, 10);
        }
    });
}
// END Direct ports

//Utility functions

//Body
export function addBodyClass(className) {
    document.body.classList.add(className);
}
export function removeBodyClass(className) {
    document.body.classList.remove(className);
}
export function setBodyStyle(style, value) {
    document.body.style[style] = value;
}

export function blurAll() {
    var tmp = document.createElement("input");
    tmp.position = "absolute";
    tmp.top = -500;
    document.body.appendChild(tmp);
    tmp.focus();
    document.body.removeChild(tmp);
}

export async function addClass(element, className, delay = 0) {
    if (element === null || element === undefined) return;
    element.classList.add(className);
    await new Promise(resolve => setTimeout(resolve, delay));
}
export async function removeClass(element, className, delay = 0) {
    if (element === null || element === undefined) return;
    element.classList.remove(className);
    await new Promise(resolve => setTimeout(resolve, delay));
}

export async function setStyle(element, style, value, delay = 0) {
    if (element === null || element === undefined) return;
    element.style[style] = value;
    await new Promise(resolve => setTimeout(resolve, delay));
}
export function addAttribute(element, name, value) {
    if (element === null || element === undefined) return;
    element.setAttribute(name, value);
}

export function removeAttribute(element, name) {
    if (element === null || element === undefined) return;
    element.removeAttribute(name);
}

export function getHeight(element) {
    if (element === null || element === undefined) null;
    return element.offsetHeight;
}

export function getWidth(element) {
    if (element === null || element === undefined) null;
    return element.offsetWidth;
}
export function setBootstrapCss(themeUrl) {
    if (link === undefined) {
        let existing = document.querySelectorAll('link[href$="bootstrap.min.css"]')[0];

        if (existing === undefined) {
            link = document.createElement('link');
            document.head.insertBefore(link, document.head.firstChild);
            link.type = 'text/css';
            link.rel = 'stylesheet';
        } else link = existing;
    }
    link.href = themeUrl;
}

// Helper function to wait for transition end or timeout
function waitForTransitionEnd(element, trigger, extraDelay = 1) {
    return new Promise((resolve) => {
        const duration = getTransitionDuration(element);
        const timeout = 450; // Minimum transition duration of 450ms any animations that take longer will be interrupted
        const transitionEndHandler = () => {
            setTimeout(async function () {
                element.removeEventListener("transitionend", transitionEndHandler);
                resolve(true);
                clearTimeout(timeoutTimer);
            }, extraDelay);
        };

        element.addEventListener("transitionend", transitionEndHandler);
        trigger();
        var timeoutTimer = setTimeout(function () {
            resolve(false);
        }, timeout);
    });
}

// Helper function to get the transition duration of an element
function getTransitionDuration(element) {
    const style = window.getComputedStyle(element);
    const duration = style.transitionDuration || style.webkitTransitionDuration || style.msTransitionDuration || "0s";
    return parseFloat(duration) * 1000; // Convert to milliseconds
}

// Helper function to wait for the next frame
function waitForNextFrame() {
    return new Promise(resolve => setTimeout(resolve, 5));
}
function setTimeoutAsync(func, time) {
    return new Promise(resolve => setTimeout(async function () { await func(); resolve(); }, time));
}
function getAriaAttributes(element) {
    const ariaAttributes = Array.from(element.attributes)
        .filter(attribute => attribute.name.startsWith('aria'))
        .map(attribute => `${attribute.name} = "${attribute.value}"`)
        .join(', ');

    return ariaAttributes;
}

function onShowClassRemoved(element, callback) {
    new MutationObserver(function (mutations) {
        if (!element.classList.contains("show")) {
            callback();
            this.disconnect();
        }
    }).observe(element, { attributes: true, attributeFilter: ["class"] });
}

function onElementRemoved(element, callback) {
    new MutationObserver(function (mutations) {
        if (!document.body.contains(element)) {
            callback();
            this.disconnect();
        }
    }).observe(element.parentElement, { childList: true });
}

function setupDocumentEvents(dotnet) {
    // add commonly used event listeners to the window
    var resizeFunc = debounce(function (event) {
        var related = docuemntEventId.find(x => x.eventtype == "resize");
        if (related > 0) {
            var relatedIds = events.map(event => event.creator);
            var relatedstring = relatedIds.join(',');
            dotnet.invokeMethodAsync("InvokeEventAsync", "jsdocument", relatedstring, "resize", window.innerWidth);
        }
    }, 200);

    window.addEventListener('resize', resizeFunc);
    documentEventsSet = true;
}

function debounce(func, delay) {
    let timeoutId;
    return function (...args) {
        clearTimeout(timeoutId);

        timeoutId = setTimeout(() => {
            func.apply(this, args);
            timeoutId = null; // Reset timeoutId after function execution
        }, delay);
    };
}