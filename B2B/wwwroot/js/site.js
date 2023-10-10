window.Ulku = {
    mask: function (id, mask, pattern, characterPattern) {
        var el = document.getElementById(id);
        if (el) {
            var format = function (value, mask, pattern, characterPattern) {
                var chars = !characterPattern ? value.replace(new RegExp(pattern, "g"), "").split('') : value.match(new RegExp(characterPattern, "g"));
                var count = 0;

                var formatted = '';
                for (var i = 0; i < mask.length; i++) {
                    const c = mask[i];
                    if (chars && chars[count]) {
                        if (/\*/.test(c)) {
                            formatted += chars[count];
                            count++;
                        } else {
                            formatted += c;
                        }
                    }
                }
                return formatted;
            }
            el.value = format(el.value, mask, pattern, characterPattern);
        }
    }
    
   
}
