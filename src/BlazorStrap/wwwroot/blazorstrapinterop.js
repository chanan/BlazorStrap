//Modals
export async function showModal(modal, dotnet) {
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
                dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", openModal.getAttribute("data-blazorstrap"), "hide", "");
                openModal.classList.remove("show");
            }
        });
        await new Promise(resolve => setTimeout(resolve, 75));
    }
    modal.classList.add("show");
    modal.focus();
    await waitForTransitionEnd(modal);
    return {
        ClassList: modal.classList.value,
        Styles: modal.style.cssText,
        Aria: getAriaAttributes(modal),
    };

    async function hideModalEvents() {
        if (modal.getAttribute("data-bs-backdrop") == "static") {
            modal.classList.add("modal-static");
            await waitForTransitionEnd(modal);
            modal.classList.remove("modal-static");
        }
        else {
            dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", modal.getAttribute("data-blazorstrap"), "hide", "");
        }
    }
}
export async function hideModal(modal, dotnet) {

    modal.classList.remove("show");
    modal.setAttribute("aria-modal", "false");
    modal.setAttribute("aria-hidden", "true");
    document.body.classList.remove("modal-open");

    await waitForTransitionEnd(modal);
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
            backdrop.classList.remove("show");
            await waitForTransitionEnd(backdrop);
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
    offcanvas.classList.add("show");
    offcanvas.focus();
    await waitForTransitionEnd(offcanvas);
    return {
        ClassList: offcanvas.classList.value,
        Styles: offcanvas.style.cssText,
        Aria: getAriaAttributes(offcanvas),
    };

    async function hideOffcanvasEvents() {
        if (offcanvas.getAttribute("data-bs-backdrop") == "static") {
            offcanvas.classList.add("offcanvas-static");
            await waitForTransitionEnd(offcanvas);
            offcanvas.classList.remove("offcanvas-static");
        }
        else {
            dotnet.invokeMethodAsync('InvokeEventAsync', "javascript", offcanvas.getAttribute("data-blazorstrap"), "hide", "");
        }
    }
}
export async function hideOffcanvas(offcanvas, dotnet) {
    offcanvas.classList.remove("show");
    offcanvas.setAttribute("aria-modal", "false");
    offcanvas.setAttribute("aria-hidden", "true");
    document.body.classList.remove("offcanvas-open");

    await waitForTransitionEnd(offcanvas);
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
            backdrop.classList.remove("show");
            await waitForTransitionEnd(backdrop);
            await dotnet.invokeMethodAsync('RemoveOffCanvasBackdropAsync');
        }
    }
    return {
        ClassList: offcanvas.classList.value,
        Styles: offcanvas.style.cssText,
        Aria: getAriaAttributes(offcanvas),
    };
}

//Popover



//Collapses
export async function showCollapse(collapse, horizontal, dotnet) {
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
    collapse.classList.add("collapsing");

    var actualSize = (horizontal ? collapse.scrollWidth : collapse.scrollHeight) + "px";

    if (horizontal) {
        collapse.style.width = actualSize;
    } else {
        collapse.style.height = actualSize;
    }

    await waitForTransitionEnd(collapse);

    collapse.classList.remove("collapsing");
    collapse.classList.add("collapse");
    collapse.classList.add("show");

    return {
        ClassList: collapse.classList.value,
        Styles: collapse.style.cssText,
        Aria: getAriaAttributes(collapse),
    };
}
export async function hideCollapse(collapse, horizontal, dotnet) {
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
    collapse.classList.add("collapsing");

    await waitForNextFrame(); // Wait for the next frame to ensure the DOM updates

    if (horizontal) {
        collapse.style.width = "0";
    } else {
        collapse.style.height = "0";
    }

    await waitForTransitionEnd(collapse);

    collapse.style.display = "";
    collapse.classList.remove("collapsing");
    collapse.classList.remove("show");
    collapse.classList.add("collapse");

    return {
        ClassList: collapse.classList.value,
        Styles: collapse.style.cssText,
        Aria: getAriaAttributes(collapse),
    };
}


// Helper function to wait for transition end or timeout
function waitForTransitionEnd(element) {
    return new Promise((resolve) => {
        const duration = getTransitionDuration(element);
        const timeout = 250; // Minimum transition duration of 350ms
        const transitionEndHandler = () => {
            element.removeEventListener("transitionend", transitionEndHandler);
            resolve();
            clearTimeout(timeoutTimer);
        };

        element.addEventListener("transitionend", transitionEndHandler);
        var timeoutTimer = setTimeout(resolve, timeout);
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
function getAriaAttributes(element) {
    const ariaAttributes = Array.from(element.attributes)
        .filter(attribute => attribute.name.startsWith('aria'))
        .map(attribute => `${attribute.name} = "${attribute.value}"`)
        .join(', ');

    return ariaAttributes;
}

function onElementRemoved(element, callback) {
    new MutationObserver(function (mutations) {
        if (!document.body.contains(element)) {
            callback();
            this.disconnect();
        }
    }).observe(element.parentElement, { childList: true });
}