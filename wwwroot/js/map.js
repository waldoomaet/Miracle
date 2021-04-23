let map = null;
let markers = []
let directionsService = null;
let directionsDisplay = null;
let latlng = null;

function initialize(name, lat, lng) {
    latlng = new google.maps.LatLng(lat, lng);
    var options = {
        zoom: 14,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map"), options);
    directionsService = new google.maps.DirectionsService;
    directionsDisplay = new google.maps.DirectionsRenderer({
        map: map
    });

    new google.maps.Marker({
        position: latlng,
        title: name,
        label: "A",
        map: map
    });

    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng);
    });
}

function placeMarker(location) {

    let request = {
        origin: latlng,
        destination: location,
        travelMode: google.maps.TravelMode.DRIVING
    };

    directionsService.route(request,
        function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
}

function alert() {
    alert("¡Tu pedido ya va en camino!");
}