function postToPage(data, url) {
    // Formu oluşturma
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", url);

    // JSON objesindeki her anahtar/değer çifti için form elemanı oluşturma
    for (var key in data) {
        if (data.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", data[key]);

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);

    // Formu gönderme
    form.submit();
}

window.Ulku = {
    getDimensions: function () {
        var wtd = window.innerWidth;
        return wtd;
    },
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
    },
}



window.iframe = {
    ac: function (data) {
        $("#payment-modal iframe").attr("src", data.gatewayUrl);
        $("#payment-modal").modal("show");
    },
    kapat: function (message) {
        addEventListener("message", e => {
            if (e) {
                if (e.data.Success) {
                    postToPage(e.data, location.protocol + '//' + window.location.host + message + e.data.OrderNumber)
                } else {
                    postToPage(e.data, location.protocol + '//' + window.location.host + message + e.data.OrderNumber)
                }
            } else {
                alert("Ödeme sırasında bir hata oluştu.");
            }
            return e.data;
            $("#payment-modal iframe").attr("src", "");
            $("#payment-modal").modal("hide");
        });
    }





}

