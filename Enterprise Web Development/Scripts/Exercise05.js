// TODO: 

window.onload = function setBackground() {
    // If background-color is saved
    if (localStorage.getItem("BackgroundColor") != null) {
        document.body.style.backgroundColor =
        localStorage.getItem("BackgroundColor");

        //localStorage.removeItem("BackgroundColor");
    }
        // Set default value here
    else
        document.body.style.backgroundColor = "#FFEFD4";

    // Set default instructions
    document.getElementById("output").innerHTML = "Player Number (1 - 4)";
}

function reloadPage() {
    // Reset = page reload
    location.reload();
}

function dealHand() {
    var str = document.getElementById("numPlayers").value;
    // No Input
    if (str == "")
        document.getElementById("output").innerHTML = "Enter number of players (1 - 4).";
    // Invalid Input
    else if(parseInt(str) < 1 || (parseInt(str) > 4))
        document.getElementById("output").innerHTML = "Number of players (must be between 1 - 4).";
    // Valid Input
    else {
        // AJAX request (no reload!)
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById("output").innerHTML = this.responseText;
            }
        }
        xmlhttp.open("GET", "dealCards.php?q=" + str, true);
        xmlhttp.send();
        return;
    }
}

