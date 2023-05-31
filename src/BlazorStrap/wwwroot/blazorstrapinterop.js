//export async function showModal(modal) {
//    modal.classList.add("show");
//    modal.style.display = "block";
//    modal.setAttribute("aria-modal", "true");
//    modal.setAttribute("aria-hidden", "false");
//    document.body.classList.add("modal-open");

//    await waitForTransitionEnd(modal);
//    return {
//        classList: modal.classList,
//        styles: modal.style,
//        ariaModal: modal.getAttribute("aria-modal"),
//        ariaHidden: modal.getAttribute("aria-hidden")
//    };
//}
//export async function hideModal(modal) {
//    modal.classList.remove("show");
//    modal.setAttribute("aria-modal", "false");
//    modal.setAttribute("aria-hidden", "true");
//    document.body.classList.remove("modal-open");

//    await waitForTransitionEnd(modal);
//    modal.style.display = "none";
//    return {
//        classList: modal.classList,
//        styles: modal.style,
//        ariaModal: modal.getAttribute("aria-modal"),
//        ariaHidden: modal.getAttribute("aria-hidden")
//    };
//}

// Show the collapse content with animation
export async function showCollapse(collapse, horizontal) {
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
    await waitForNextFrame(); // Wait for the next frame to ensure the DOM updates
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
        AriaExpanded: true
    };
}

// Hide the collapse content with animation
export async function hideCollapse(collapse, horizontal) {
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
        AriaExpanded: false
    };
}


// Helper function to wait for transition end or timeout
function waitForTransitionEnd(element) {
    return new Promise((resolve) => {
        const duration = getTransitionDuration(element);
        const timeout = Math.max(duration, 300); // Minimum transition duration of 300ms

        const transitionEndHandler = () => {
            resolve();
            element.removeEventListener("transitionend", transitionEndHandler);
        };

        element.addEventListener("transitionend", transitionEndHandler);
        setTimeout(resolve, timeout);
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
    return new Promise(resolve => requestAnimationFrame(resolve));
    //return new Promise(resolve => {
    //    setTimeout(resolve, 100);
    //});
}