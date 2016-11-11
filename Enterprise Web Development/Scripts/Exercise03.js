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

    // set Labels
    var date = new Date();
    document.getElementById("todayDate").innerHTML = date.toDateString();
    document.getElementById("dueDate").innerHTML = "(Enter a Term for Payment)";
}

function reloadPage() {
    // Reset = page reload
    location.reload();
}

function calculate() {

    var paymentTerm = document.getElementById("dateInput").value;

    try {
        // Throw if NotANumb
        if (isNaN(paymentTerm))
            throw "Terms of Payment must be a number!"
        // Throw if Null
        if (paymentTerm == "")
            throw "Terms of Payment is empty!"

        // Curent Date
        var date = new Date();
        // Current Date + Input
        date.setDate(date.getDate() + parseInt(paymentTerm)); // ParseInt otherwise it will try to concat.
        // Check for Saturday
        if (date.getDay() == 6) // Sat: -1
            date.setDate(date.getDate() - parseInt(1));
        // Check for Sunday
        if (date.getDay() == 0) // Sun: +1
            date.setDate(date.getDate() + parseInt(1));

        // Update String
        document.getElementById("dueDate").innerHTML = date.toDateString();
    }
    catch(error){
        window.alert("Error! Invalid Input.\n" + error);
    }

    
}