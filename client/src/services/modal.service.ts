 import { Injectable }     from '@angular/core';
 import { ModalController} from 'ionic-angular';

@Injectable()
export class ModalService {
    constructor(private modalCtrl: ModalController) {}

  openModal(Modal) {
    let modal = this.modalCtrl.create(Modal);
    modal.present();
  }
}