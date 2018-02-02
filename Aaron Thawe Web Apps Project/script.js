// create some basic global variables
var map = {};
var markers = [];
var markersPos = [];  // I added this to try and keep track of each marker for Distance API requests. It didnt work out, explanation below.
var place = [];
var globalPos;

// *************************************************************
// onload event handler to create the initial map and map object
function initMap() {
    // create a container to draw the map (same idea as bing)
    // inside a <div>
    var mapCanvas = (document.getElementById("maparea"));

    // define some map properties (These default to Fanshawe before the map can find the user's location, and then adjust)
    var mapOptions = {
        center: new google.maps.LatLng(43.011987, -81.200276),
        zoom: 12
    }

    // call the constructor to create a new map object
    // and then get your geo location
    map = new google.maps.Map(mapCanvas, mapOptions);
    getLocation();
}

// ***********************************************************
// Get and then set the map position based on the geo location
function getLocation() {
    if (navigator.geolocation) {
        // showPosition is a reference to a JS function (below)
        navigator.geolocation.getCurrentPosition(showPosition);
    }
}

// *********************************
// helper function for getLocation()
function showPosition(position) {
    globalPos = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
    };

    //center the map on current position
    map.setCenter(globalPos);
}

// ***************************************************************
// this event handler demonstrates Google Places service
// by requesting all hotels within a varying metre radius
// based on the current geo location (defined by globalPos object)
function hotelfinder() {
    // delete any existing markers
    deleteMarkers();
    //default radius
    var rad = '500';
    if (document.getElementById("SlcRange").value !== "5hdist") {
        if (document.getElementById("SlcRange").value == "1kdist") {
            rad = '1000';
        } else if (document.getElementById("SlcRange").value == "10kdist") {
            rad = '10000';
        } else if (document.getElementById("SlcRange").value == "25kdist") {
            rad = '25000';
        }
    }

    // create a request object
    var request = {
        location: globalPos,
        radius: rad,
        query: ['hotel']
    };

    // create the service object
    var service = new google.maps.places.PlacesService(map);

    // perform a search based on the request object and callback
    // the "callback" function below
    service.textSearch(request, callback);

    // this is an inner callback function as referenced immediately above
    function callback(results, status) {
        if (status === google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                markersPos.push(results[i].geometry.location.toString());   //this was to capture the locations of the markers for use below
                //Creating the marker
                addMarker(results[i]);
            }
        }
    }

    // display all the pins
    displayAllMarkers(map);
}

// **************************************************************
// function creates a marker object and adds the new marker (pin)
// to the marker array
function addMarker(place) {
    // create a marker (pin) object
    var marker = new google.maps.Marker({
        position: place.geometry.location,
        map: map,
        title: place.name + "\n" + place.formatted_address,
        animation: google.maps.Animation.DROP
    });

    //original click event handler being created here
    // add a "click" event handler to centre on the marker
    //marker.addListener('click', function () {
    //    map.setZoom(16);
    //    map.setCenter(marker.getPosition());
    //});


    // I tried isolating the handler in a function below, I found that the code for the event handler was actually being run as the markers were made.
    //marker.addListener('click', findDistance());

    // push the marker object onto the markers array
    markers.push(marker);
}

// *********************************************************
// display all the marker objects (pins) in the marker array
function displayAllMarkers(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// *************************************************
// delete all map markers and init the markers array
function deleteMarkers() {
    displayAllMarkers(null);
    markers = [];
    markersPos = [];
}

/**
 * This was where I hoped to grab the users location and the location data of whatever marker they clicked on, set those up as a
 * Google Distance API query, find the time and distance for the trip, and present that to the user by having a link to that information
 * appear when they click a marker. Unfortunately things kept on being wrong with this, as I spent a lot of time trying to do an AJAX request
 * until I learned that the error message about CORS headers and Same Origin Policy was actually due to me choosing the wrong method, and not
 * some particular line I needed to add in or something. The request string I made was:
 * https://maps.googleapis.com/maps/api/distancematrix/json?origins=42.98565,-81.32752&destinations=43.01198,-81.20027&key=AIzaSyCjR2vbpAnEIy29bi9YO3BLs_iTgQ96C90
 * That would get me the test data that is in json.json. The rest of my experimental files are test.js and testpage.html . I included those
 * to show some of the other things I tried, most of it is things I found online looking through stackoverflow, mozilla developer network
 * and the google developer references.
 */
function findDistance(markernum) {
    //console.log(markersPos[markernum]);

    //var inputstring = globalPos.lat + ',' + globalPos.lng + '&destinations=' + markersPos[markernum].toString().substring(1, markersPos[markernum].toString().length - 1);
    //var data = "https://maps.googleapis.com/maps/api/distancematrix/json?key=AIzaSyCjR2vbpAnEIy29bi9YO3BLs_iTgQ96C90&origins=" + inputstring;

    //trying to get the link to become visible
    document.getElementById("LnkDistance").href = data;
    document.getElementById("LnkDistance").style.visibility = "visible";
}