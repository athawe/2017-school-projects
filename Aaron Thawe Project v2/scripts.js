//Declaring Global data object and persistent user index variable
var jsData = {};
var index = 0;
//Setting up games array in data object with default settings
jsData.games = [];
jsData.games[{
    home_team_name: "",
    away_team_name: "",
    winning_pitcher_first: "",
    winning_pitcher_last: "",
    losing_pitcher_first: "",
    losing_pitcher_last: "",
    venue:""
}];

// This is the async callback function
function myCallBack(baseballJson) {
    // convert the returned data to a JavaScript object
    var jsObject = JSON.parse(baseballJson);
    //pass returned data to OutputValues function to strip out exactly the data we need
    OutputValues(jsObject);
}

// AJAX asynchronous XMLHttpRequest to get the JSON from the site defined by url and using the callback function callback (alias for myCallBack)
function getJSONAsync(url, callback) {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            callback(request.responseText);
        }
    };
    request.open('GET', url);
    request.send();
}

//onclick function for Retrieve button
function RetrieveData() {
    //specify date for request
    var year = document.getElementById("selYear").value;
    var month = document.getElementById("selMonth").value;
    var day = document.getElementById("selDay").value;
    //Make URL for request
    var tempURL = "http://gd2.mlb.com/components/game/mlb/year_" + year + "/month_" + month + "/day_" + day + "/master_scoreboard.json";
    // blank out the textboxes
    document.getElementById("txtHome").Text = ""; 
    document.getElementById("txtAway").Text = "";
    document.getElementById("txtWin").Text = "";
    document.getElementById("txtLose").Text = "";
    document.getElementById("txtVen").Text = "";
    // get the data for the specified date with an asynchronous call
    getJSONAsync(tempURL, myCallBack);
}

//Test if request was successful, strip needed data from result, and print out first entry to user
function OutputValues(jsObject) {
    //Reset index @ 0 
    index = 0;
    //tempObj for reading data
    var tempObj = {};
    //Test if games weren't played that day, stop user if no data
    if (!(jsObject.data.games.game)) {
        alert("No games found! Try another date!");
        return;
    }
    //clearing input array
    jsData.games = [{
        home_team_name: "",
        away_team_name: "",
        winning_pitcher_first: "",
        winning_pitcher_last: "",
        losing_pitcher_first: "",
        losing_pitcher_last: "",
        venue: ""
    }];
    //Read data into global data object using temp object (unnecessary, but more clear for me)
    for (var b = 0; b < jsObject.data.games.game.length; b++) {
        tempObj = {
            home_team_name: jsObject.data.games.game[b].home_team_name,
            away_team_name: jsObject.data.games.game[b].away_team_name,
            winning_pitcher_first: jsObject.data.games.game[b].winning_pitcher.first,
            winning_pitcher_last: jsObject.data.games.game[b].winning_pitcher.last,
            losing_pitcher_first: jsObject.data.games.game[b].losing_pitcher.first,
            losing_pitcher_last: jsObject.data.games.game[b].losing_pitcher.last,
            venue: jsObject.data.games.game[b].venue
        }
        jsData.games[b] = tempObj;
    }
    //data object and index variable are global, use printData function to output data to textboxes
    printData();
}

//Print Data from global object dependant on index variable
function printData() {
    document.getElementById("txtHome").value = jsData.games[index].home_team_name;
    document.getElementById("txtAway").value = jsData.games[index].away_team_name;
    document.getElementById("txtWin").value = jsData.games[index].winning_pitcher_first + " " + jsData.games[index].winning_pitcher_last;
    document.getElementById("txtLose").value = jsData.games[index].losing_pitcher_first + " " + jsData.games[index].losing_pitcher_last;
    document.getElementById("txtVen").value = jsData.games[index].venue;
}

//onclick for next button
function btnNextClick() {
    //Test if user is on the last index before moving further
    if ((index + 1) === jsData.games.length) {
        alert("That's the last game recorded for that day.");
        return;
    }//increment global index variable
    index++;
    //output data from next entry to user
    printData();
}

//onclick for previous button
function btnPrevClick() {
    //Test if user is on first index before moving back
    if (index === 0) {
        alert("No prior games recorded that day.");
        return;
    }//decrement global index variable
    index--;
    //output data from next entry to user
    printData();
}

//Test if user has entered a valid character on each onkeyup event.
function ValidateText(input) {
    var regexhyphen = /^[a-zA-Z0-9\'-]+$/i;
    var OK = regexhyphen.exec(input.value);
    if (!OK && (input.value.length != 0)) {
        alert("Improper value!");
        var temp = input.value;
        input.value = "";
    }
}