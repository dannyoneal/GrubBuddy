var firebase = require("firebase");
var cert = require('./firebaseAdminKey.json');
var db = null;
module.exports = {
    initDataStore: function() {
        try {
             var config = {
                apiKey: "AIzaSyDBcnWHAoAuC0cZ9kp3MnDPiH-48UcoWEM",
                authDomain: "lunchmate-f005d.firebaseapp.com",
                databaseURL: "https://lunchmate-f005d.firebaseio.com",
                storageBucket: "lunchmate-f005d.appspot.com",
                messagingSenderId: "51408714003"
            };
            firebase.initializeApp(config);
            db = firebase.database();

            if(!db) {
                console.log("Datastore object is null!")
                throw "error";
            }

            console.log("Datastore succesfully initialized");
        }
        catch(err) {
            console.log("An error occured connecting to firebase!");
            console.log(err);
        }
    }
}

