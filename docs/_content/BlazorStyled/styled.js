var styleEl;
var styleSheet;

window.styledJsFunctions = {
    insertRule: function (rule, development) {
        //console.log(rule);
        if (styleEl === undefined) createStylesheet();
        if (development) {
            var text = styleEl.innerText;
            text = text + rule;
            styleEl.innerText = text;
            return -1;
        } else {
            let num = -1;
            try {
                num = styleSheet.insertRule(rule);
            } catch{
                //ignored
            }
            finally {
                return num;
            }
        }
    },
    clearAllRules: function () {
        const head = document.head;
        if (styleEl !== undefined) head.removeChild(styleEl);
        createStylesheet();
        return true;
    },
    addGoogleFont: function (fontlink) {
        const head = document.head;
        var found = false;
        var links = head.getElementsByTagName("link");
        for (let tag of links) {
            if (trimHref(tag.href) === trimHref(fontlink)) found = true;
        }
        if (!found) {
            link = document.createElement('link');
            head.insertBefore(link, document.head.firstChild);
            link.type = 'text/css';
            link.rel = 'stylesheet';
            link.href = fontlink;
        }
        return true;
    }
};

function createStylesheet() {
    styleEl = document.createElement('style');
    const head = document.head;
    if (head.firstChild) {
        head.insertBefore(styleEl, head.firstChild);
    } else {
        head.appendChild(styleEl);
    }
    styleSheet = styleEl.sheet;
}

function trimHref(href) {
    return href.substring(href.indexOf("//") + 2);
}