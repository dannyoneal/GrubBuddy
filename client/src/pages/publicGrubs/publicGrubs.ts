import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service'

@Component({
  selector: 'page-public-grubs',
  templateUrl: 'publicGrubs.html'
})
export class PublicGrubsPage {

  constructor(public auth: AuthService) {
  }

}
