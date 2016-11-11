// TODO: 

window.onload = function setBackground() {
    // If background-color is saved
    if (localStorage.getItem("BackgroundColor") != null) {
        document.body.style.backgroundColor =
        localStorage.getItem("BackgroundColor");

        //localStorage.removeItem("BackgroundColor");
    }
        // Set default value here
    else {
        document.body.style.backgroundColor = "#FFEFD4";
    }

    // Hide total string
    document.getElementById("outputString").style.visibility = "hidden";
}

function reloadPage() {
    // Reset = page reload
    location.reload();
}


function toggleCheckbox() {
    if (document.getElementById("inscription").checked)
        document.getElementById("inscriptionString").disabled = false;
    else
        document.getElementById("inscriptionString").disabled = true;
}

function submitForm() {
    try {

        // Value of selected cake
        var cake = document.querySelector('input[name="size"]:checked').value;
        // Object for select/combobox
        var fillingCombo = document.getElementById("filling");
        // Selected combobox value
        var filling = fillingCombo.value;
        // Candies (Ternary OP)
        var candies = document.getElementById('candies').checked ? 5 : 0;
        // Inscription value & string
        var inscription = document.getElementById('inscription').checked ? 20 : 0;
        var inscriptionString = document.getElementById('inscriptionString').value;
        // Contact
        var name = document.getElementById("nameSpace").value;
        var addr = document.getElementById("addr").value;
        var phone = document.getElementById("phone").value;

        // Throw if name is null
        if (name == "")
            throw "ERROR! Invalid Input.\nPlease input a name."
        // Throw if addr is null
        if (addr == "")
            throw "ERROR! Invalid Input.\nPlease input an address."
        // Throw if phone is null
        if (phone == "")
            throw "ERROR! Invalid Input.\nPlease input a telephone number."
        // If inscription, check input
        if (inscription != 0 && inscriptionString == "") {
            throw "ERROR! Invalid Input.\nPlease input an Inscription."
        }

        // bc vars
        var total = parseInt(cake) + parseInt(filling) +
            parseInt(candies) + parseInt(inscription);

        document.getElementById("outputString").innerHTML = ("Total: $" + (total).formatMoney(2, '.', ','));

        // Show total string
        document.getElementById("outputString").style.visibility = "visible";
    }
    catch(error){
        window.alert(error);
    }
}

// http://stackoverflow.com/questions/149055/how-can-i-format-numbers-as-money-in-javascript
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};