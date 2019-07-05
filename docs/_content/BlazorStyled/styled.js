var styleEl = document.createElement('style');
document.head.appendChild(styleEl);
var styleSheet = styleEl.sheet;

window.styledJsFunctions = {
    insertRule: function (rule, development) {
        //console.log(rule)
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
    }
};