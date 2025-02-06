window.BlazorDownloadFile = (fileName, contentType, byteArray) => {
    const blob = new Blob([new Uint8Array(byteArray)], { type: contentType });
    const url = URL.createObjectURL(blob);

    const anchor = document.createElement("a");
    anchor.href = url;
    anchor.download = fileName;
    anchor.click();

    URL.revokeObjectURL(url);
};





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
    currency: function (element, decimalPlaces) {

        element.oninput = function (i, d) {
            return function (e) {
                var firstchar = i.value.charAt(0)
                if (firstchar === '0')
                    i.value = i.value.replace('0', '');
                i.value = i.value.replace(/[^0-9,]/g, '').replace(/(\..*?)\..*/g, '$1').replace(/^0[^.]/, '0');

                if (d === 0)
                    i.value = i.value.replace('.', '');
            }
        }(element, decimalPlaces);
    },

    newMask: function (element, mask, pattern, characterPattern) {
        element.oninput = function (element, mask, pattern, characterPattern) {
            return function (e) {

                var chars = !characterPattern ? element.value.replace(new RegExp(pattern, "g"), "").split('') : element.value.match(new RegExp(characterPattern, "g"));
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
                element.value = formatted;
            }
        }(element, mask, pattern, characterPattern);
    }
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