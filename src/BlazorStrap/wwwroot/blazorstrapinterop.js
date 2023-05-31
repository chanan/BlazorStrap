export async function showModal(modal, dotnet) {
    var start = new Date();
    var backdrop = document.querySelector('.modal-backdrop');
    if (backdrop)
    {
        await waitForNextFrame();
        backdrop.classList.add("show");
    }

    modal.style.display = "block";
    modal.setAttribute("aria-modal", "true");
    modal.setAttribute("aria-hidden", "false");
    document.body.classList.add("modal-open");
    await waitForNextFrame(); 
    modal.classList.add("show");
 
    await waitForTransitionEnd(modal);

    var end = new Date();
    var time = end - start;
    console.log('Execution time: ' + time);
    return JSON.stringify({
        ClassList: modal.classList.value,
        Styles: modal.style.cssText,
        Aria: getAriaAttributes(modal),
    });
}
export async function hideModal(modal, dotnet) {

    modal.classList.remove("show");
    modal.setAttribute("aria-modal", "false");
    modal.setAttribute("aria-hidden", "true");
    document.body.classList.remove("modal-open");
    
    await waitForTransitionEnd(modal);
    modal.style.display = "none";

    var backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
        var openModals = document.querySelectorAll('.modal.show:not([data-blazorstrap-backdrop="false"])');
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

// Show the collapse content with animation
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

// Hide the collapse content with animation
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