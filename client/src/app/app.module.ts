import { NgModule } from '@angular/core';
import { IonicApp, IonicModule, NavController, ModalController } from 'ionic-angular';
import { MyApp } from './app.component';
import { TabsPage } from '../pages/tabs/tabs';
import { MyGrubsPage } from '../pages/myGrubs/myGrubs';
import { PublicGrubsPage } from '../pages/publicGrubs/publicGrubs';
import { AllGrubsPage } from '../pages/allGrubs/allGrubs';
import { AutocompletePage } from '../pages/map/map';
import { AuthConfig, AuthHttp } from 'angular2-jwt';
import { AuthService } from '../services/auth.service';
import { ModalService } from '../services/modal.service';
import { Http } from '@angular/http';
import { Storage } from '@ionic/storage';
import { GrubsService } from "../services/grubs.service";
import { NewGrubModal } from "../modals/newGrub";

let storage: Storage = new Storage();

export function getAuthHttp(http) {
  return new AuthHttp(new AuthConfig({
    globalHeaders: [{'Accept': 'application/json'}],
    tokenGetter: (() => storage.get('id_token'))
  }), http);
}

@NgModule({
  declarations: [
    MyApp,
    TabsPage,
    MyGrubsPage,
    PublicGrubsPage,
    AllGrubsPage,
    NewGrubModal,
    AutocompletePage
  ],
  imports: [
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    TabsPage,
    MyGrubsPage,
    PublicGrubsPage,
    AllGrubsPage,
    NewGrubModal,
    AutocompletePage
  ],
  providers: [
    Storage,
    AuthService,
    GrubsService,
    ModalService,
    {
      provide: AuthHttp,
      useFactory: getAuthHttp,
      deps: [Http]
    },
  ]
})
export class AppModule {}