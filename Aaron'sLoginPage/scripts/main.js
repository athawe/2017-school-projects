//Change display and visibility settings, onclick for register now header.
function FormAppear() {

    document.getElementById("regform").style.visibility = "visible";
    document.getElementById("register").style.display = "none";
    document.getElementById("darkbuttons").style.display = "block";
    document.getElementById("colouredbuttons").style.display = "none";
}
//Resets display and visibility settings
function Disappear() {
    document.getElementById("regform").style.visibility = "hidden";
    document.getElementById("register").style.display = "block";
    document.getElementById("darkbuttons").style.display = "none";
    document.getElementById("colouredbuttons").style.display = "block";
}
//Save button function
function Output() {
    var string1 = document.getElementById("Text1").value;
    var string2 = document.getElementById("Pass1").value;
    var outstring = string1 + ", " + string2;
    alert(outstring);
    Disappear();
}
//onchange function for password fields
function checkpass() {
    var string1 = document.getElementById("Pass1").value;
    var string2 = document.getElementById("Pass2").value;
    if (string1 == string2 && string1 != "") {
        document.getElementById("Save").disabled = false;
    } else {
        document.getElementById("Save").disabled = true;
    }
}
//initialization function
function init() {
    document.getElementById("Save").disabled = true;
}