 // Initializing the map


// Place function
 var Place = function (data) {
    "use strict";
    this.name = ko.observable(data.name);
    this.lat = ko.observable(data.lat);
    this.lng = ko.observable(data.lng);
    this.id = ko.observable(data.id);
    this.marker = ko.observable();
	this.rating = ko.observable("");
    this.url = ko.observable("");
    this.canonicalUrl = ko.observable("");
    this.contentString = ko.observable("");
    this.phone = ko.observable("");
    this.description = ko.observable("");
    this.address = ko.observable("");
    
},

// array shows the locations data

locations = [
   
    {
      
        lat: 30.634732,
        lng: 31.082818
    }
   
],
map;



// initMap function
function initMap() {
    "use strict";
    map = new google.maps.Map(document.getElementById("show_Map"), {
        center: {lat: 30.634732, lng: 31.082818},
        zoom: 10,
        disableDefaultUI: true
    });
    // Start the ShowModell here so it doesn't initialize before Google Maps loads
    ko.applyBindings(new ShowModell());
}

// message to the user if google maps isn't working
function googleError() {
    "use strict";
    document.getElementById("error_data").innerHTML = "<h2>There Is Some Thing Error , Please Retray Again</h2>";
}


// ShowModell
var ShowModell = function () {
    "use strict";
    // Make this accessible
    var self = this;

    // Create an array of all places
    this.places = ko.observableArray([]);

    // Call the Place constructor
    // Create Place objects for each item in locations & store them in the above array
    locations.forEach(function (placeItem) {
        self.places.push(new Place(placeItem));
    });

    // Initialize the infowindow
    var infowindow = new google.maps.InfoWindow({
        maxWidth: 250,
    });

    var marker;// Initializing the marker

    //Set marker for all places , with Foursquare data, and set event listeners for the information window
    
    self.places().forEach(function (placeItem) {

        // All places markers 
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(placeItem.lat(), placeItem.lng()),
            map: map,
            animation: google.maps.Animation.DROP
        });
        placeItem.marker = marker;

        // Make AJAX request to Foursquare
        $.ajax({
			// Foursquare data the id and the secret
            url: "https://api.foursquare.com/v2/venues/" + placeItem.id() +
            "?client_id=FDPMRSL5V20ZQ0ZUQX2MM2D3LFURV4FW0FJK1GOBLDEU2RCQ&client_secret=K3ICKR4BAPJ0TYQBVOAD1YBM2GTQJNQUT1BKS4NITTL5WQEQ&v=20180207",
            dataType: "json",
            success: function (data) {
                // To handle result
                var result = data.response.venue;
                var connectnow = result.hasOwnProperty("connectnow") ? result.connectnow : "";
				// That to check each result properties, if the property exists it will be added  to the Place constructor
                if (connectnow.hasOwnProperty("formattedPhone")) {
                    placeItem.phone(connectnow.formattedPhone || "");
                }

                var location = result.hasOwnProperty("location") ? result.location : "";
                if (location.hasOwnProperty("address")) {
                    placeItem.address(location.address || "");
                }

               
                var description = result.hasOwnProperty("description") ? result.description : "";
                placeItem.description(description || "");

                var rating = result.hasOwnProperty("rating") ? result.rating : "";
                placeItem.rating(rating || "none");

                var url = result.hasOwnProperty("url") ? result.url : "";
                placeItem.url(url || "");

                placeItem.canonicalUrl(result.canonicalUrl);

                // The content of the Informaion window
                var contentString = "<div id='iWindow'><h4>" + placeItem.name() +
                        
                        placeItem.phone() + "</p><p>" + placeItem.address() + "</p><p>" +
                        placeItem.description() + "</p><p>Rating: " + placeItem.rating() +
                        "</p><p><a href=" + placeItem.url() + ">" + placeItem.url() +
                        "</a></p><p><a target='_blank' href=" + placeItem.canonicalUrl() +
                        ">Foursquare Page</a></p><p><a target='_blank' href=https://www.google.com/maps/dir/Current+Location/" +
                        placeItem.lat() +" "+ placeItem.lng() + ">Directions</a></p></div>";

                // Add infowindows 
                google.maps.event.addListener(placeItem.marker, "click", function () {
                    infowindow.open(map, this);
					//for animation
                    placeItem.marker.setAnimation(google.maps.Animation.BOUNCE);
                    setTimeout(function () {
                        placeItem.marker.setAnimation(null);
                    }, 700);
                    infowindow.setContent(contentString);
                    map.setCenter(placeItem.marker.getPosition());
                });
            },
            // error messages
            error: function (e) {
                infowindow.setContent("");
                document.getElementById("error_data").innerHTML = "";
            }
        });

        // This event listener makes the error message on AJAX error display in the infowindow
        google.maps.event.addListener(marker, "click", function () {
            infowindow.open(map, this);
            placeItem.marker.setAnimation(google.maps.Animation.BOUNCE);
            setTimeout(function () {
                placeItem.marker.setAnimation(null);
            }, 500);
        });
    });

    // showing the marker when the user clicks a list of places 
    self.showInfo = function (placeItem) {
        google.maps.event.trigger(placeItem.marker, "click");
        self.hideElements();
    };

    // Toggle the nav class based style
   self.NavigationTog = ko.observable(false);
    this.navStatus = ko.pureComputed (function () {
        return self.NavigationTog() === false ? "nav" : "navClosed";
        }, this);

    self.hideElements = function (NavigationTog) {
        self.NavigationTog(true);
        // the default action will be allowed
        return true;
    };

    self.showElements = function (NavigationTog) {
        self.NavigationTog(false);
        return true;
    };

    // Array containing all the markers based on search and visible
    self.visible = ko.observableArray();

    // when the document load the markers are visible by default before the user input any thing
    self.places().forEach(function (place) {
        self.visible.push(place);
    });

    // the user input
    self.userInput = ko.observable('');

    // If the user input something included in the places the marker will be visible ,Otherwise remove the marker
    self.filterMarkers = function () {
        // all markers and places will not be visible
        var SearchLocation = self.userInput().toUpperCase();
        self.visible.removeAll();
        self.places().forEach(function (place) {
            place.marker.setVisible(false);
            
            // Comparing the places names which the user input If the user input something included in the location name , show the place
            if (place.name().toUpperCase().indexOf(SearchLocation) !== -1) {
                self.visible.push(place);
            }
        });
        self.visible().forEach(function (place) {
            place.marker.setVisible(true);
        });
    };

}; 
