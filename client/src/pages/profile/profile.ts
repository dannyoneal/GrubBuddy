import {Component} from '@angular/core';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'profile-page',
  templateUrl: 'profile.html'
})
export class ProfilePage {

  // We need to inject AuthService so that we can
  // use it in the view
  constructor(public auth: AuthService) {}
}