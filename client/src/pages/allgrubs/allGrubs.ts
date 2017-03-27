import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs/Rx';
import { NavController } from 'ionic-angular';
import { GrubsService } from "../../services/grubs.service";
import { Grub } from '../../types/Grub';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'page-all-grubs',
  templateUrl: 'allGrubs.html'
})
export class AllGrubsPage implements OnInit{
  searchText: string = '';
  allGrubs:Grub[];
  searchBarControl: FormControl;
  constructor(public navCtrl: NavController, public grubsService: GrubsService) {
    this.allGrubs = [];
    this.searchBarControl = new FormControl();
  }

  ngOnInit(){
    this.getAllGrubs();
    this.searchBarControl.valueChanges.debounceTime(700).subscribe(search => {
      this.searchGrubs();
    });
  }

  searchGrubs() {
    if(!this.searchText) {
      this.getAllGrubs();
      return;
    }
    this.grubsService.searchGrubs(this.searchText).subscribe(
      grubs => this.allGrubs = grubs);
  }

  getAllGrubs() {
    this.grubsService.getGrubs().subscribe(
      grubs => this.allGrubs = grubs);
  }

}
