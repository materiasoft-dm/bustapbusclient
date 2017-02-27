var uniqueId = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
});

$(function () {

    var markers = [];
    

    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14
    });
    var marker = new google.maps.Marker({
        title: "Hello World!"
    });


    $.support.cors = true;
    var connection = $.hubConnection('http://darkmaster-001-site1.btempurl.com/signalr');
    var locationH = connection.createHubProxy('locationHub');

    locationH.on('broadcastLocation', function (name, longitude, latitude) {
        var myLocation = { lat: latitude, lng: longitude };

        map.setCenter(myLocation);
        debugger;
        marker.setMap(null);
        marker = new google.maps.Marker({
            title: "Hello World!",
            map: map,
            position: myLocation
        });
    });



    connection.start().done(function () {
        var geo_options = {
            enableHighAccuracy: true,
            maximumAge: 30000,
            timeout: 27000
        };

        var wpid = navigator.geolocation.watchPosition(geo_success, geo_error, geo_options);
    });

    function geo_success(position) {
        locationH.invoke('send', 'dm', position.coords.longitude, position.coords.latitude);
    }

    function geo_error() {
        alert("Sorry, no position available.");
    }





});