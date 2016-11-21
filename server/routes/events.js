
var ds = require('../datastore.js');
var express = require('express');

module.exports = function(app) {
  var router = express.Router();

  /* GET users listing. */
  router.get('/events', function(req, res, next) {

    var Event = ds.getEventModel();

    console.log("fetching events");
  
    // use mongoose to get all reviews in the database
    Event.find(function(err, lunchEvents) {

        // if there is an error retrieving, send the error. nothing after res.send(err) will execute
        if (err)
            res.send(err)

        res.json(lunchEvents); // return all reviews in JSON format
    });

  });

  // create review and send back all reviews after creation
  router.post('/events', function(req, res) {

      var Event = ds.getEventModel();
      console.log("creating event");

      // create a review, information comes from request from Ionic
      Event.create({
          id: req.body.id,
          title: req.body.title,
          description: req.body.description,
          date: req.body.date,
          done : false
      }, function(err, review) {
          if (err)
              res.send(err);

          // get and return all the reviews after you create another
          Event.find(function(err, lunchEvents) {
              if (err)
                  res.send(err)
              res.json(lunchEvents);
          });
      });

  });

  app.use('/api', router)

};

