import {Component, NgZone, ViewChild, ElementRef, OnInit} from '@angular/core';
import {ViewController} from 'ionic-angular';
import { Geolocation } from 'ionic-native';
declare var google;

@Component({
  templateUrl: 'map.html',
})

export class AutocompletePage{
  @ViewChild('map') mapElement: ElementRef;
  autocompleteItems;
  autocomplete;
  autoCompleteService = new google.maps.places.AutocompleteService(); //Todo: inject google services?
  placeService: any;
  map : any;
  currentMarker: any;

  constructor (public viewCtrl: ViewController, private zone: NgZone) {
    this.autocompleteItems = [];
    this.autocomplete = {
      query: ''
    };
    
  }

  ngAfterViewInit(){
    this.loadMap();
  }

  dismiss() {
    this.viewCtrl.dismiss();
  }

  select(item: any) {
    let me = this;
    this.placeService.getDetails({placeId: item.place_id}, function(place, status){
      if(me.currentMarker) 
        me.currentMarker.setMap(null); //clear previous marker.

      me.currentMarker = new google.maps.Marker({
        map: me.map,
        position: place.geometry.location
      });

      var position = me.currentMarker.getPosition();
      me.map.setCenter(position);
    });
  }
  
  updateSearch() {
    if (this.autocomplete.query == '') {
      this.autocompleteItems = [];
      return;
    }
    let me = this;
    this.autoCompleteService.getPlacePredictions({ input: this.autocomplete.query, componentRestrictions: {country: 'US'} }, function (predictions, status) {
      me.autocompleteItems = []; 
      me.zone.run(function () {
        predictions.forEach(function (prediction) {
          me.autocompleteItems.push(prediction);
        });
      });
    });
  }

  loadMap(){
    Geolocation.getCurrentPosition().then((position) => {
      let latLng = new google.maps.LatLng(-34.9290, 138.6010);
  
      let mapOptions = {
        center: latLng,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
      }
  
      this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
      this.placeService = new google.maps.places.PlacesService(this.map);
    }, (err) => {
      console.log(err);
    });
  }
}