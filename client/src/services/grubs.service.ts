import { Injectable }     from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import {Observable} from 'rxjs/Rx';
import {Grub} from '../types/Grub';
import {HttpClientService} from '../services/httpClient.service';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class GrubsService {
    private baseGrubsUrl = "http://localhost:50095/grubs";
    grubs: any;
    constructor(private httpClient: HttpClientService) {
        this.grubs = [];
    }


    getGrubs() : Observable<Grub[]> {
        var action = "/get";
        return this.httpClient.get(this.baseGrubsUrl + action, new RequestOptions());
    }

    searchGrubs(searchText:string) {
        var action = "/getByName/" + searchText;
        return this.httpClient.get(this.baseGrubsUrl + action, new RequestOptions());
    }
}