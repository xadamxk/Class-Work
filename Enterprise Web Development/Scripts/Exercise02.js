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
}

function reloadPage() {
    // Reset = page reload
    location.reload();
}

function unlockBox() {
    if (document.querySelector('input[name = "intRate"]:checked') != null) {
        radioVal = document.querySelector('input[name = "intRate"]:checked').value;
        if (radioVal == "4") {
            document.getElementById("custVal").removeAttribute('disabled');
        }
        else {
            document.getElementById("custVal").setAttribute("disabled", true);
        }
    }
}

function calcResults() {
    // Variables for inputs
    var desiredAmt = document.getElementById("desiredAmt").value;
    var initInvest = document.getElementById("initInvest").value;
    var annContr = document.getElementById("annContr").value;
    var radioVal; // decimal

    // Debug Text Boxs
    //window.alert(desiredAmt + "\n" + initInvest + "\n" + annContr);

    // Check Text inputs for errors
    if(isNaN(desiredAmt) || desiredAmt == ""){
        window.alert("Error! Invalid input.\nDesired Amount is not a number or was left empty.");
        return;
    }

    if (isNaN(initInvest) || initInvest == "") {
        window.alert("Error! Invalid input.\nInitial Investment is not a number or was left empty.");
        return;
    }

    if (isNaN(annContr) || annContr == "") {
        window.alert("Error! Invalid input.\nAnnual Contribution is not a number or was left empty.");
        return;
    }

    // Check Radio Inputs for errors
    if (document.querySelector('input[name = "intRate"]:checked') != null) {
        radioVal = document.querySelector('input[name = "intRate"]:checked').value;
    }
    else {
        window.alert("Error! Invalid input.\nPlease select an interest rate.");
        return;
    }

    // If radio = custom
    if (radioVal == 4) {
        // set radioVal = textbox
        radioVal = (document.getElementById("custVal").value / 100);
        // Check Radio Inputs for errors
        if (isNaN(radioVal) || radioVal == "") {
            window.alert("Error! Invalid input.\nCustom interest rate is not a number or was left empty.")
            return;
        }
    }

    // Debug selected Radio Button
    //window.alert(radioVal);

    // If it got this far, there are no errors and it's time to do math.
    // GO MATH!

    var temp = 0;
    var counter = 0;

    do {
        // Have to parse the input vars as a decimal otherwise it will concatenate them as strings, derp.
        temp = (parseFloat(initInvest) + parseFloat(annContr)) * (1 + radioVal);
        // Debug Incrementer
        //window.alert(counter + "\n" + temp + "\n" + radioVal);
        initInvest = temp;
        counter++;
    }
    while (temp < desiredAmt);

    // Round to 2 decimals
    temp = (Math.round(initInvest * 100) / 100);

    document.getElementById("yearTag").innerHTML = "After " + counter + " years,";
    document.getElementById("totalTag").innerHTML = "you will have $" + numberWithCommas(temp);
}

// Thank you REGEX :)
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}