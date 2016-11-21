var mongoose = require('mongoose'); 
var model = null;


var eventSchema = mongoose.Schema(
    {
        id: Number,
        title: String,
        description: String,
        date: Date
    }
);

module.exports = {
    initDataStore: function() {
        try {

            // Configuration
            mongoose.connect('mongodb://localhost/lunchbuddy');

            console.log("Datastore succesfully initialized");

        }
        catch(err) {
            console.log("An error occured connecting to mongodb!");
            console.log(err);
        }
    },
    getEventModel: function () {
        return mongoose.model('Event', eventSchema);
    }
}
