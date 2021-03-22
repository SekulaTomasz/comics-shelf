function debounce(func:Function, wait:number, immediate: boolean = false) {
    var timeout:any;
    return function(this: any) {
        var context = this;
        var args = arguments;
        clearTimeout(timeout);
        timeout = setTimeout(function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        }, wait);
        if (immediate && !timeout) func.apply(context, args);
    };
}

export default debounce