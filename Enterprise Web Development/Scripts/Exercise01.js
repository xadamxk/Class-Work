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

function setTemp() {
    // Show Input/Output Boxes
    toggleVisibility("Temperature");
    // Change Input/Output Labels
    labelTextBoxes("F", "C");
}

function setVol() {
    // Show Input/Output Boxes
    toggleVisibility("Volume");
    // Change Input/Output Labels
    labelTextBoxes("gal", "L");
}

function setWt() {
    // Show Input/Output Boxes
    toggleVisibility("Weight");
    // Change Input/Output Labels
    labelTextBoxes("Lbs", "Kg");
}

function setDis() {
    // Show Input/Output Boxes
    toggleVisibility("Distance");
    // Change Input/Output Labels
    labelTextBoxes("in", "cm");
}

function toggleVisibility(label) {
    // Show Input/Output Boxes
    document.getElementById("InOut").style.visibility = 'visible';
    document.getElementById("InOutButtons").style.visibility = 'visible';
    // Change Instruction Label
    document.getElementById("selectOption").innerHTML = label;
}

function labelTextBoxes(inLabel, outLabel) {
    // Get label items
    var inLabelItem = document.getElementById("inputLabel");
    var outLabelItem = document.getElementById("outputLabel");
    // Set label items
    inLabelItem.innerHTML = inLabel;
    outLabelItem.innerHTML = outLabel;

}

function isInputValid() {
    var isError = false;

    // Invalid Input (Null)
    if (document.getElementById("input").value == "") {
        window.alert("Error: Invalid input. Input is null!");
        isError = true;
    }
    // Invalid Input (Not a Number)
    if (isNaN(document.getElementById("input").value) != false && !isError) {
        window.alert("Error: Invalid input. Numbers only!");
        isError = true;
    }
    // No Error - Calculate Output
    if (!isError) {
        calcOutput();
    }
}

function calcOutput() {
    var inLabelItem = document.getElementById("inputLabel").innerHTML;
    var inputVal = document.getElementById("input").value;
    var temp;

    switch (inLabelItem) {
        // F -> C
        case "F":
            temp = Math.round((((inputVal) - 32) * 5 / 9) * 100) / 100;
            break;
        // C -> F
        case "C":
            temp = Math.round(((((inputVal) * 9) /5)+32) * 100) / 100;
            break;
        // gal -> L
        case "gal":
            temp = Math.round(((inputVal) * 3.785411) * 100) / 100;
            break;
        // L -> gal
        case "L":
            temp = Math.round(((inputVal) * 0.26417) * 100) / 100;
            break;
        // lbs -> kg
        case "Lbs":
            temp = Math.round(((inputVal) / 2.2) * 100) / 100;
            break;
        // kg -> lbs
        case "Kg":
            temp = Math.round(((inputVal) * 2.2046) * 100) / 100;
            break;
        // in -> cm
        case "in":
            temp = Math.round(((inputVal) * 2.54) * 100) / 100;
            break;
        // cm -> in
        case "cm":
            temp = Math.round(((inputVal) * .39370079) * 100) / 100;
            break;
    }
    
    document.getElementById("output").value = temp;
}

function reloadPage() {
    // Reset = page reload
    location.reload();
}

function swapInOuts() {
    // Swap Labels on swap click
    var temp = document.getElementById("inputLabel").innerHTML;
    document.getElementById("inputLabel").innerHTML = document.getElementById("outputLabel").innerHTML;
    document.getElementById("outputLabel").innerHTML = temp;

    isInputValid();
}

