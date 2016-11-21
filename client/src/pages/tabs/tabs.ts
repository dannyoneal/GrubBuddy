import { Component, OnInit } from '@angular/core';
import { MyGrubsPage } from '../myGrubs/myGrubs';
import { AllGrubsPage } from '../allGrubs/allGrubs';
import { PublicGrubsPage } from '../publicGrubs/publicGrubs';
import { AuthService } from '../../services/auth.service';
import { NavController} from 'ionic-angular';

@Component({
  templateUrl: 'tabs.html',
  selector: 'tabs'
})
export class TabsPage implements OnInit {
  tab1Root: any = AllGrubsPage;
  tab2Root: any = PublicGrubsPage;
  tab3Root: any = MyGrubsPage;
  constructor(public auth: AuthService) {} 

  ngOnInit() {
    if(!this.auth.authenticated()) {
      this.auth.login();
    }
  }
}
